// <copyright file="SQLBookDataServices.cs" company="Transilvania University of Brasov">
// Chelmus Otilia-Elena
// </copyright>

namespace DataMapper.SqlServerDAO
{
    using System.Data.Entity;
    using System.Diagnostics.CodeAnalysis;
    using DataMapper.Interfaces;
    using DomainModel.Models;
    using log4net;

    /// <summary>The book data services.</summary>
    [ExcludeFromCodeCoverage]
    public class SQLBookDataServices : IBookDataServices
    {
        private static readonly ILog Logger = LogManager.GetLogger(System.Environment.MachineName);

        /// <inheritdoc/>
        public bool AddBook(Book book)
        {
            using (LibraryContext libraryContext = new LibraryContext())
            {
                try
                {
                    List<Author> authors = book.Authors.ToList();
                    List<Domain> domains = new List<Domain>();
                    List<Edition> editions = book.Editions.ToList();

                    foreach (var author in authors)
                    {
                        libraryContext.Authors.Attach(author);
                    }

                    foreach (var domain in book.Domains)
                    {
                        Domain? foundDomain = libraryContext.Domains.FirstOrDefault(d => d.Id == domain.Id);

                        if (foundDomain != null)
                        {
                            domains.Add(foundDomain);
                            libraryContext.Domains.Attach(domains[domains.Count() - 1]);
                        }
                    }

                    foreach (var edition in book.Editions)
                    {
                        if (edition.Id != 0)
                        {
                            libraryContext.Editions.Attach(edition);
                        }
                        else
                        {
                            // libraryContext.Editions.Add(edition);
                        }
                    }

                    book.Authors = authors;
                    book.Domains = domains;
                    book.Editions = editions;

                    libraryContext.Books.Add(book);
                    libraryContext.SaveChanges();
                }
                catch (Exception exception)
                {
                    Logger.Error("Error while adding a new book: " + exception.Message.ToString());
                    return false;
                }
            }

            Logger.Info("Book added successfully!");
            return true;
        }

        /// <inheritdoc/>
        public ICollection<Book> GetBooks()
        {
            ICollection<Book> books = new List<Book>();

            using (LibraryContext libraryContext = new LibraryContext())
            {
                books = libraryContext.Books.Include("Domains").OrderBy((book) => book.Id).ToList();
            }

            return books;
        }

        /// <inheritdoc/>
        public Book? GetBookById(int id)
        {
            Book? book = null;

            using (LibraryContext libraryContext = new LibraryContext())
            {
                book = libraryContext.Books.FirstOrDefault((book) => book.Id == id);
            }

            return book;
        }

        /// <inheritdoc/>
        public Book? GetBookByTitle(string title)
        {
            Book? book = null;

            using (LibraryContext libraryContext = new LibraryContext())
            {
                book = libraryContext.Books
                    .Include(book => book.Authors)
                    .Include(book => book.Editions)
                    .Include(book => book.Domains)
                    .FirstOrDefault((book) => book.Title == title);
            }

            return book;
        }

        /// <inheritdoc/>
        public bool UpdateBook(Book book)
        {
            using (LibraryContext libraryContext = new LibraryContext())
            {
                try
                {
                    libraryContext.Books.Attach(book);
                    libraryContext.Entry(book).State = EntityState.Modified;
                    libraryContext.SaveChanges();
                }
                catch (Exception exception)
                {
                    Logger.Error("Error while updating a book: " + exception.Message.ToString());
                    return false;
                }
            }

            Logger.Info("Book updated successfully!");
            return true;
        }

        /// <inheritdoc/>
        public bool DeleteBook(Book book)
        {
            using (LibraryContext libraryContext = new LibraryContext())
            {
                try
                {
                    libraryContext.Books.Attach(book);
                    libraryContext.Books.Remove(book);
                    libraryContext.SaveChanges();
                }
                catch (Exception exception)
                {
                    Logger.Error("Error while deleting a new book: " + exception.Message.ToString());
                    return false;
                }
            }

            Logger.Info("Book deleted successfully!");
            return true;
        }

        /// <inheritdoc/>
        // TODO: De revizuit metoda.
        public bool BookAlreadyExists(string title, List<Author> authorList)
        {
            Book? currentBook = this.GetBookByTitle(title);

            if (currentBook != null)
            {
                foreach (Author author in authorList)
                {
                    if (!currentBook.Authors.Any(a => a.FullName == author.FullName))
                    {
                        return false;
                    }
                }

                return true;
            }

            return false;
        }
    }
}
