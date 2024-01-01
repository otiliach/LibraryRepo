// <copyright file="BookServicesImplementation.cs" company="Transilvania University of Brasov">
// Chelmus Otilia-Elena
// </copyright>

namespace ServiceLayer.Implementation
{
    using System.ComponentModel.DataAnnotations;
    using DataMapper;
    using DataMapper.Interfaces;
    using DomainModel.Models;
    using log4net;
    using ServiceLayer.Interfaces;

    /// <summary>The Book Services Implementation class.</summary>
    /// <seealso cref="ServiceLayer.Interfaces.IBookServices" />
    public class BookServicesImplementation : IBookServices
    {
        /// <summary>The logger.</summary>
        private ILog logger;

        /// <summary>The book data services.</summary>
        private IBookDataServices bookDataServices;

        /// <summary>The domain data services.</summary>
        private IDomainDataServices domainDataServices;

        /// <summary>The condition data services.</summary>
        private IConditionDataServices conditionDataServices;

        /// <summary>Initializes a new instance of the <see cref="BookServicesImplementation" /> class.</summary>
        /// <param name="bookDataServices">The book data services.</param>
        /// <param name="domainDataServices">The domain data services.</param>
        /// <param name="conditionDataServices">The condition data services.</param>
        /// <param name="logger">The logger.</param>
        public BookServicesImplementation(IBookDataServices bookDataServices, IDomainDataServices domainDataServices, IConditionDataServices conditionDataServices, ILog logger)
        {
            this.bookDataServices = bookDataServices;
            this.domainDataServices = domainDataServices;
            this.conditionDataServices = conditionDataServices;
            this.logger = logger;
        }

        /// <inheritdoc/>
        public bool AddBook(Book book)
        {
            // Check if <book> is null.
            if (book == null)
            {
                this.logger.Warn("Attempted to add a null book.");
                return false;
            }

            // Check if <book> is invalid.
            var context = new ValidationContext(book, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            if (!Validator.TryValidateObject(book, context, results, true))
            {
                this.logger.Warn("Attempted to add an invalid book. " + string.Join(' ', results));
                return false;
            }

            // Check if there are too many domains
            if (book.Domains.Count > this.conditionDataServices.GetDOMENII())
            {
                this.logger.Warn("Attempted to add a book with too many domains.");
                return false;
            }

            // Check if ancestor-descendant domains
            if (book.Domains.Count >= 2)
            {
                for (int i = 0; i < book.Domains.Count - 1; i++)
                {
                    for (int j = i + 1; j < book.Domains.Count; j++)
                    {
                        if (this.AreDomainsDirectlyRelated(
                            book.Domains.ElementAt(i), book.Domains.ElementAt(j)))
                        {
                            this.logger.Warn("Attempted to add a book with directly related domains.");
                            return false;
                        }
                    }
                }
            }

            // Check if <book> already exists.
            if (this.BookAlreadyExists(book.Title, book.Authors.ToList()))
            {
                this.logger.Warn("Attempted to add a duplicate book.");
                return false;
            }

            // If all checks pass call AddBook.
            return this.bookDataServices.AddBook(book);
        }

        /// <inheritdoc/>
        public bool BookAlreadyExists(string title, List<Author> authorList)
        {
            return this.bookDataServices.BookAlreadyExists(title, authorList);
        }

        /// <inheritdoc/>
        public ICollection<Book> GetBooks()
        {
            return this.bookDataServices.GetBooks();
        }

        /// <inheritdoc/>
        public Book? GetBookById(int id)
        {
            return this.bookDataServices.GetBookById(id);
        }

        /// <inheritdoc/>
        public Book? GetBookByTitle(string title)
        {
            return this.bookDataServices.GetBookByTitle(title);
        }

        /// <inheritdoc/>
        public ICollection<Book> GetBooksByDomain(Domain domain)
        {
            List<Book> books = this.GetBooks().ToList();
            List<Domain> childDomains
                = this.domainDataServices.GetChildDomains(domain).ToList();

            books = books.Where(b =>
                b.Domains.Any(bd => childDomains.Any(cd => cd.Id == bd.Id)))
                .ToList();

            return books;
        }

        /// <inheritdoc/>
        public bool UpdateBook(Book book)
        {
            // Check if <book> is null.
            if (book == null)
            {
                this.logger.Warn("Attempted to update a null book.");
                return false;
            }

            // Check if <book> is invalid.
            var context = new ValidationContext(book, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            if (!Validator.TryValidateObject(book, context, results, true))
            {
                this.logger.Warn("Attempted to update an invalid book. " + string.Join(' ', results));
                return false;
            }

            // Check if <book> exists.
            if (this.bookDataServices.GetBookById(book.Id) == null)
            {
                this.logger.Warn("Attempted to update a non-existing book.");
                return false;
            }

            // If all checks pass call UpdateBook.
            return this.bookDataServices.UpdateBook(book);
        }

        /// <inheritdoc/>
        public bool DeleteBook(Book book)
        {
            // Check if <book> is null.
            if (book == null)
            {
                this.logger.Warn("Attempted to delete a null book.");
                return false;
            }

            // Check if <book> exists.
            if (this.bookDataServices.GetBookById(book.Id) == null)
            {
                this.logger.Warn("Attempted to delete a non-existing book.");
                return false;
            }

            // If all checks pass call DeleteBook.
            return this.bookDataServices.DeleteBook(book);
        }

        /// <summary>Ares the domains directly related.</summary>
        /// <param name="domain1">The domain1.</param>
        /// <param name="domain2">The domain2.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        private bool AreDomainsDirectlyRelated(Domain domain1, Domain domain2)
        {
            List<Domain> childDomains1 = this.domainDataServices.GetChildDomains(domain1).ToList();
            List<Domain> childDomains2 = this.domainDataServices.GetChildDomains(domain2).ToList();

            if (childDomains1.Any(cd => cd.Id == domain2.Id) ||
                childDomains2.Any(cd => cd.Id == domain1.Id))
            {
                return true;
            }

            return false;
        }
    }
}
