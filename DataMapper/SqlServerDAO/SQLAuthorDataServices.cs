// <copyright file="SQLAuthorDataServices.cs" company="Transilvania University of Brasov">
// Chelmus Otilia-Elena
// </copyright>

namespace DataMapper.SqlServerDAO
{
    using System.Data.Entity;
    using System.Diagnostics.CodeAnalysis;
    using DataMapper.Interfaces;
    using DomainModel.Models;
    using log4net;

    /// <summary>The author data services.</summary>
    [ExcludeFromCodeCoverage]
    public class SQLAuthorDataServices : IAuthorDataServices
    {
        private static readonly ILog Logger = LogManager.GetLogger(System.Environment.MachineName);

        /// <inheritdoc/>
        public bool AddAuthor(Author author)
        {
            using (LibraryContext libraryContext = new LibraryContext())
            {
                try
                {
                    libraryContext.Authors.Add(author);
                    libraryContext.SaveChanges();
                }
                catch (Exception exception)
                {
                    Logger.Error("Error while adding a new author: " + exception.Message.ToString());
                    return false;
                }
            }

            Logger.Info("Author added successfully!");
            return true;
        }

        /// <inheritdoc/>
        public ICollection<Author> GetAuthors()
        {
            ICollection<Author> authors = new List<Author>();

            using (LibraryContext libraryContext = new LibraryContext())
            {
                authors = libraryContext.Authors.OrderBy((author) => author.Id).ToList();
            }

            return authors;
        }

        /// <inheritdoc/>
        public Author? GetAuthorById(int id)
        {
            Author? author = null;

            using (LibraryContext libraryContext = new LibraryContext())
            {
                author = libraryContext.Authors.FirstOrDefault((author) => author.Id == id);
            }

            return author;
        }

        /// <inheritdoc/>
        public Author? GetAuthorByName(string name)
        {
            Author? author = null;

            using (LibraryContext libraryContext = new LibraryContext())
            {
                author = libraryContext.Authors.FirstOrDefault((author) => author.FullName == name);
            }

            return author;
        }

        /// <inheritdoc/>
        public bool UpdateAuthor(Author author)
        {
            using (LibraryContext libraryContext = new LibraryContext())
            {
                try
                {
                    libraryContext.Authors.Attach(author);
                    libraryContext.Entry(author).State = EntityState.Modified;
                    libraryContext.SaveChanges();
                }
                catch (Exception exception)
                {
                    Logger.Error("Error while updating a author: " + exception.Message.ToString());
                    return false;
                }
            }

            Logger.Info("Author updated successfully!");
            return true;
        }

        /// <inheritdoc/>
        public bool DeleteAuthor(Author author)
        {
            using (LibraryContext libraryContext = new LibraryContext())
            {
                try
                {
                    libraryContext.Authors.Attach(author);
                    libraryContext.Authors.Remove(author);
                    libraryContext.SaveChanges();
                }
                catch (Exception exception)
                {
                    Logger.Error("Error while deleting a new author: " + exception.Message.ToString());
                    return false;
                }
            }

            Logger.Info("Author deleted successfully!");
            return true;
        }

        /// <inheritdoc/>
        public bool AuthorNameAlreadyExists(string name)
        {
            if (this.GetAuthorByName(name) != null)
            {
                return true;
            }

            return false;
        }
    }
}
