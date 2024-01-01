// <copyright file="BorrowedBooksServicesImplementation.cs" company="Transilvania University of Brasov">
// Chelmus Otilia-Elena
// </copyright>

namespace ServiceLayer.Implementation
{
    using System.ComponentModel.DataAnnotations;
    using DataMapper.Interfaces;
    using DomainModel.Enums;
    using DomainModel.Models;
    using log4net;
    using ServiceLayer.Interfaces;

    /// <summary>The BorrowedBooks Services Implementation class.</summary>
    /// <seealso cref="ServiceLayer.Interfaces.IBorrowedBooksServices" />
    public class BorrowedBooksServicesImplementation : IBorrowedBooksServices
    {
        /// <summary>The logger.</summary>
        private ILog logger;

        /// <summary>The borrowed books data services.</summary>
        private IBorrowedBooksDataServices borrowedBooksDataServices;

        /// <summary>The condition data services.</summary>
        private IConditionDataServices conditionDataServices;

        /// <summary>The edition data services.</summary>
        private IEditionDataServices editionDataServices;

        /// <summary>Initializes a new instance of the <see cref="BorrowedBooksServicesImplementation" /> class.</summary>
        /// <param name="borrowedBooksDataServices">The borrowed books data services.</param>
        /// <param name="conditionDataServices">The condition data services.</param>
        /// <param name="editionDataServices">The edition data services.</param>
        /// <param name="logger">The logger.</param>
        public BorrowedBooksServicesImplementation(IBorrowedBooksDataServices borrowedBooksDataServices, IConditionDataServices conditionDataServices, IEditionDataServices editionDataServices, ILog logger)
        {
            this.borrowedBooksDataServices = borrowedBooksDataServices;
            this.conditionDataServices = conditionDataServices;
            this.editionDataServices = editionDataServices;
            this.logger = logger;
        }

        /// <inheritdoc/>
        public bool AddBorrowedBooks(BorrowedBooks borrowedBooks)
        {
            // Check if <borrowedBooks> is null.
            if (borrowedBooks == null)
            {
                this.logger.Warn("Attempted to add a null borrowedBooks.");
                return false;
            }

            // Check if <borrowedBooks> is invalid.
            var context = new ValidationContext(borrowedBooks, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            if (!Validator.TryValidateObject(borrowedBooks, context, results, true))
            {
                this.logger.Warn("Attempted to add an invalid borrowedBooks. " + string.Join(' ', results));
                return false;
            }

            // Check if enough books are in stock and can be borrowed.
            foreach (Edition edition in borrowedBooks.Editions)
            {
                if (edition.NumberOfCopiesForBorrowing <= 0)
                {
                    this.logger.Warn("Attempted to borrow out of stock books.");
                    return false;
                }

                if (edition.NumberOfCopiesForBorrowing < 0.1 * edition.InitialStock)
                {
                    this.logger.Warn("Attempted to borrow with not enough remaining books.");
                    return false;
                }
            }

            int nmc = this.conditionDataServices.GetNMC();
            int c = this.conditionDataServices.GetC();
            int d = this.conditionDataServices.GetD();
            int l = this.conditionDataServices.GetL();
            int lim = this.conditionDataServices.GetLIM();
            int per = this.conditionDataServices.GetPER();
            int delta = this.conditionDataServices.GetDELTA();

            if (borrowedBooks.Reader.AccountType == EAccountType.LibrarianReader)
            {
                nmc *= 2;
                c *= 2;
                d *= 2;
                lim *= 2;
                per /= 2;
                delta /= 2;
            }

            // Check if number of user's books is less than NMC in PER.
            List<BorrowedBooks> userBookOrdersFromPER = this.borrowedBooksDataServices
                .GetBorrowedBooksByUserId(borrowedBooks.Reader.Id)
                .ToList()
                .Where(bb => bb.BorrowDate >= DateTime.Today.AddDays(
                -1 * per)).ToList();

            int booksInPER = 0;

            foreach (BorrowedBooks bookOrder in userBookOrdersFromPER)
            {
                booksInPER += bookOrder.Editions.Count;
            }

            // * nr. of books orderd + nr. of books to order > max nr. of books
            if (booksInPER + borrowedBooks.Editions.Count > nmc)
            {
                this.logger.Warn("Attempted to borrow more than NMC books in PER.");
                return false;
            }

            // Check if user is trying to borrow more than C books at once.
            if (borrowedBooks.Editions.Count > c)
            {
                this.logger.Warn("Attempted to borrow more than C books at once.");
                return false;
            }

            // Check if there is not enough distinct domains.
            List<Book> books = borrowedBooks.Editions.Select(edition => edition.Book).ToList();
            if (borrowedBooks.Editions.Count >= 3 && !HasDistinctDomains(books))
            {
                this.logger.Warn("Attempted to borrow books with not enough distinct domains.");
                return false;
            }

            // Check if number of user's books with the same domain is less than D in L months
            List<BorrowedBooks> userBookLMonths = this.borrowedBooksDataServices
                .GetBorrowedBooksByUserId(borrowedBooks.Reader.Id)
                .ToList()
                .Where(bb => bb.BorrowDate >= DateTime.Today.AddMonths(
                -l)).ToList();
            userBookLMonths.Add(borrowedBooks);
            if (this.TooManyBooksFromOneDomain(userBookLMonths, d))
            {
                this.logger.Warn("Attempted to borrow more than D books with the same domain in L months.");
                return false;
            }

            // Check if user borrowed the same book in a DELTA time.
            List<BorrowedBooks> userBookDELTA = this.borrowedBooksDataServices
                .GetBorrowedBooksByUserId(borrowedBooks.Reader.Id)
                .ToList()
                .Where(bb => bb.BorrowDate >= DateTime.Today.AddDays(
                -delta)).ToList();
            if (this.BookBorrowedTooManyTimesInDeltaTime(userBookDELTA, books))
            {
                this.logger.Warn("Attempted to borrow same book in DELTA time.");
                return false;
            }

            // Check if reader borrowed more than NCZ books in a day.
            List<BorrowedBooks> readerBookOrdersDay = this.borrowedBooksDataServices
                    .GetBorrowedBooksByUserId(borrowedBooks.Reader.Id)
                    .ToList()
                    .Where(bb => bb.BorrowDate >= DateTime.Today).ToList();
            readerBookOrdersDay.Add(borrowedBooks);
            if (borrowedBooks.Reader.AccountType == EAccountType.Reader
                && readerBookOrdersDay.Count > this.conditionDataServices.GetNCZ())
            {
                this.logger.Warn("Attempted to borrow more than NCZ books in a day.");
                return false;
            }

            // Check if librarian lent more than PERSIMP books in a day.
            List<BorrowedBooks> librarianBookOrdersDay = this.borrowedBooksDataServices
                .GetBorrowedBooksByUserId(borrowedBooks.Reader.Id)
                .ToList()
                .Where(bb => bb.BorrowDate >= DateTime.Today).ToList();
            librarianBookOrdersDay.Add(borrowedBooks);
            if (borrowedBooks.Reader.AccountType == EAccountType.Reader
                && librarianBookOrdersDay.Count() > this.conditionDataServices.GetPERSIMP())
            {
                this.logger.Warn("Attempted to lend more than PERSIMP books in a day.");
                return false;
            }

            // If all checks pass call AddBorrowedBooks.
            bool response = this.borrowedBooksDataServices.AddBorrowedBooks(borrowedBooks);
            if (response)
            {
                foreach (var edition in borrowedBooks.Editions)
                {
                    edition.NumberOfCopiesForBorrowing--;
                    this.editionDataServices.UpdateEdition(edition);
                }
            }

            return response;
        }

        /// <inheritdoc/>
        public ICollection<BorrowedBooks> GetBorrowedBooks()
        {
            return this.borrowedBooksDataServices.GetBorrowedBooks();
        }

        /// <inheritdoc/>
        public List<BorrowedBooks> GetBorrowedBooksByUserId(int userId)
        {
            return this.borrowedBooksDataServices.GetBorrowedBooksByUserId(userId);
        }

        /// <inheritdoc/>
        public bool UpdateBorrowedBooks(BorrowedBooks borrowedBooks)
        {
            // Check if <borrowedBooks> is null.
            if (borrowedBooks == null)
            {
                this.logger.Warn("Attempted to update a null [borrowedBooks].");
                return false;
            }

            // Check if <book> is invalid.
            var context = new ValidationContext(borrowedBooks, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            if (!Validator.TryValidateObject(borrowedBooks, context, results, true))
            {
                this.logger.Warn("Attempted to update an invalid [borrowedBooks]. " + string.Join(' ', results));
                return false;
            }

            // Check if <book> exists.
            if (this.borrowedBooksDataServices.GetBorrowedBooksByUserId(borrowedBooks.Reader.Id) == null)
            {
                this.logger.Warn("Attempted to update a non-existing [borrowedBooks].");
                return false;
            }

            // Check if sum of extends in last 3 months is more then LIM.
            int lim = this.conditionDataServices.GetLIM();

            if (borrowedBooks.Reader.AccountType == EAccountType.LibrarianReader)
            {
                lim *= 2;
            }

            List<BorrowedBooks> usersBorrowedBooks = this.borrowedBooksDataServices
                .GetBorrowedBooksByUserId(borrowedBooks.Reader.Id)
                .ToList()
                .Where(bb => bb.BorrowDate >= DateTime.Today.AddMonths(
                -3)).ToList();

            usersBorrowedBooks.Add(borrowedBooks);

            int sumOfBookExtensions = usersBorrowedBooks
                .Sum(bb => (bb.ActualReturnDate - bb.OriginalReturnDate).Days);

            if (sumOfBookExtensions > lim)
            {
                this.logger.Warn("Attempted to extend more than LIM in the last 3 months.");
                return false;
            }

            // If all checks pass call UpdateBook.
            return this.borrowedBooksDataServices.UpdateBorrowedBooks(borrowedBooks);
        }

        /// <summary>Determines whether [has distinct domains] [the specified books].</summary>
        /// <param name="books">The books.</param>
        /// <returns>
        ///   <c>true</c> if [has distinct domains] [the specified books]; otherwise, <c>false</c>.</returns>
        private static bool HasDistinctDomains(List<Book> books)
        {
            HashSet<string> domains = new HashSet<string>();

            foreach (var book in books)
            {
                foreach (var domain in book.Domains)
                {
                    domains.Add(domain.Name);
                }
            }

            return domains.Count >= 2;
        }

        /// <summary>Determines if books the borrowed too many times in delta time.</summary>
        /// <param name="userBooksDELTA">The user books delta.</param>
        /// <param name="newBooks">The new books.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        private bool BookBorrowedTooManyTimesInDeltaTime(List<BorrowedBooks> userBooksDELTA, List<Book> newBooks)
        {
            foreach (var borrowedBooks in userBooksDELTA)
            {
                List<Book> booksDELTA = borrowedBooks.Editions
                    .Select(edition => edition.Book).ToList();

                foreach (var book in newBooks)
                {
                    if (booksDELTA.Any(bd => bd.Id == book.Id))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>Determines if borrowed too many books from one domain.</summary>
        /// <param name="userBookLMonths">The user book l months.</param>
        /// <param name="d">The d.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        private bool TooManyBooksFromOneDomain(List<BorrowedBooks> userBookLMonths, int d)
        {
            Dictionary<string, int> domainOccurrences = new Dictionary<string, int>();

            foreach (var borrowedBooks in userBookLMonths)
            {
                List<Book> books = borrowedBooks.Editions
                   .Select(edition => edition.Book)
                   .ToList();

                // TODO: Check if this works!
                // List<Book> books = borrowedBooks.Editions
                //    .Select(e => new { Book = e.Book, Domains = e.Book.Domains })
                //    .SelectMany(b => b.Domains.ToList(), (e, d) =>
                //    new Book
                //    {
                //    })
                //    .ToList();
                foreach (var book in books)
                {
                    foreach (var domain in book.Domains)
                    {
                        if (domainOccurrences.ContainsKey(domain.Name))
                        {
                            domainOccurrences[domain.Name]++;
                        }
                        else
                        {
                            domainOccurrences.Add(domain.Name, 1);
                        }
                    }
                }
            }

            foreach (KeyValuePair<string, int> domainOccurrence in domainOccurrences)
            {
                if (domainOccurrence.Value > d)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
