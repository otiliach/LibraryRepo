// <copyright file="IDAOFactory.cs" company="Transilvania University of Brasov">
// Chelmus Otilia-Elena
// </copyright>

namespace DataMapper
{
    using System.Diagnostics.CodeAnalysis;
    using DataMapper.Interfaces;

    /// <summary>The service factory.</summary>
    public interface IDAOFactory
    {
        /// <summary>Gets the condition data services.</summary>
        /// <value>The condition data services.</value>
        [ExcludeFromCodeCoverage]
        IConditionDataServices ConditionDataServices
        {
            get;
        }

        /// <summary>Gets the domain data services.</summary>
        /// <value>The domain data services.</value>
        [ExcludeFromCodeCoverage]
        IDomainDataServices DomainDataServices
        {
            get;
        }

        /// <summary>Gets the author data services.</summary>
        /// <value>The author data services.</value>
        [ExcludeFromCodeCoverage]
        IAuthorDataServices AuthorDataServices
        {
            get;
        }

        /// <summary>Gets the user data services.</summary>
        /// <value>The user data services.</value>
        [ExcludeFromCodeCoverage]
        IUserDataServices UserDataServices
        {
            get;
        }

        /// <summary>Gets the book data services.</summary>
        /// <value>The book data services.</value>
        [ExcludeFromCodeCoverage]
        IBookDataServices BookDataServices
        {
            get;
        }

        /// <summary>Gets the edition data services.</summary>
        /// <value>The edition data services.</value>
        [ExcludeFromCodeCoverage]
        IEditionDataServices EditionDataServices
        {
            get;
        }

        /// <summary>Gets the borrowed books data services.</summary>
        /// <value>The borrowed books data services.</value>
        [ExcludeFromCodeCoverage]
        IBorrowedBooksDataServices BorrowedBooksDataServices
        {
            get;
        }
    }
}
