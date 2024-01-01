// <copyright file="SQLBorrowedBooksDataServices.cs" company="Transilvania University of Brasov">
// Chelmus Otilia-Elena
// </copyright>

namespace DataMapper.SqlServerDAO
{
    using System.ComponentModel.DataAnnotations;
    using System.Data.Entity;
    using System.Data.Entity.Validation;
    using System.Diagnostics.CodeAnalysis;
    using DataMapper.Interfaces;
    using DomainModel.Models;
    using log4net;

    /// <summary>The borrowed books data services.</summary>
    [ExcludeFromCodeCoverage]
    public class SQLBorrowedBooksDataServices : IBorrowedBooksDataServices
    {
        private static readonly ILog Logger = LogManager.GetLogger(System.Environment.MachineName);

        /// <inheritdoc/>
        public bool AddBorrowedBooks(BorrowedBooks borrowedBooks)
        {
            using (LibraryContext libraryContext = new LibraryContext())
            {
                try
                {
                    borrowedBooks.ReaderId = borrowedBooks.Reader.Id;
                    borrowedBooks.LibrarianId = borrowedBooks.Librarian.Id;

                    List<Edition> editions = new List<Edition>();

                    foreach (var edition in borrowedBooks.Editions)
                    {
                        Edition? foundEdition = libraryContext.Editions
                            .Include(e => e.Book)
                            .Include(e => e.BorrowedBooks)
                            .FirstOrDefault(e => e.Id == edition.Id);

                        if (foundEdition != null)
                        {
                            editions.Add(foundEdition);
                            libraryContext.Editions.Attach(editions[editions.Count() - 1]);
                        }
                    }

                    borrowedBooks.Editions = editions;

                    foreach (Edition edition in borrowedBooks.Editions)
                    {
                        var edContext = new ValidationContext(edition, serviceProvider: null, items: null);
                        var edResults = new List<ValidationResult>();

                        if (!Validator.TryValidateObject(edition, edContext, edResults, true))
                        {
                            Console.WriteLine("Invalid edition: " + string.Join(' ', edResults));
                            Logger.Info("Invalid edition: " + string.Join(' ', edResults));
                        }
                    }

                    var context = new ValidationContext(borrowedBooks, serviceProvider: null, items: null);
                    var results = new List<ValidationResult>();

                    if (!Validator.TryValidateObject(borrowedBooks, context, results, true))
                    {
                        Console.WriteLine("Invalid borrowedBooks: " + string.Join(' ', results));
                        Logger.Info("Invalid edition: " + string.Join(' ', results));
                    }

                    libraryContext.BorrowedBooks.Add(borrowedBooks);
                    libraryContext.SaveChanges();
                }
                catch (DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        Console.WriteLine(
                            "Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name,
                            eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Console.WriteLine(
                                "- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName,
                                ve.ErrorMessage);
                        }
                    }

                    throw;
                }
                catch (Exception exception)
                {
                    Logger.Error("Error while adding a new borrowedBooks: " + exception.Message.ToString() + " " + exception.InnerException?.ToString());
                    return false;
                }
            }

            Logger.Info("BorrowedBooks added successfully!");
            return true;
        }

        /// <inheritdoc/>
        public ICollection<BorrowedBooks> GetBorrowedBooks()
        {
            ICollection<BorrowedBooks> borrowedBookss = new List<BorrowedBooks>();

            using (LibraryContext libraryContext = new LibraryContext())
            {
                borrowedBookss = libraryContext.BorrowedBooks.OrderBy((borrowedBooks) => borrowedBooks.Id).ToList();
            }

            return borrowedBookss;
        }

        /// <inheritdoc/>
        public List<BorrowedBooks> GetBorrowedBooksByUserId(int userId)
        {
            List<BorrowedBooks> borrowedBooks = new List<BorrowedBooks>();

            using (LibraryContext libraryContext = new LibraryContext())
            {
                borrowedBooks = libraryContext.BorrowedBooks
                    .Include(bb => bb.Editions.Select(e => e.Book))
                    .Where(borrowedBooks => borrowedBooks.Reader.Id == userId)
                    .ToList();
            }

            return borrowedBooks;
        }

        /// <inheritdoc/>
        public bool UpdateBorrowedBooks(BorrowedBooks borrowedBooks)
        {
            using (LibraryContext libraryContext = new LibraryContext())
            {
                try
                {
                    libraryContext.BorrowedBooks.Attach(borrowedBooks);
                    libraryContext.Entry(borrowedBooks).State = EntityState.Modified;
                    libraryContext.SaveChanges();
                }
                catch (Exception exception)
                {
                    Logger.Error("Error while updating a borrowedBooks: " + exception.Message.ToString());
                    return false;
                }
            }

            Logger.Info("BorrowedBooks updated successfully!");
            return true;
        }
    }
}
