// <copyright file="SQLServerDAOFactory.cs" company="Transilvania University of Brasov">
// Chelmus Otilia-Elena
// </copyright>

namespace DataMapper
{
    using System.Diagnostics.CodeAnalysis;
    using DataMapper.Interfaces;
    using DataMapper.SqlServerDAO;

    /// <summary>The service factory.</summary>
    [ExcludeFromCodeCoverage]
    internal class SQLServerDAOFactory : IDAOFactory
    {
        /// <inheritdoc/>
        public IConditionDataServices ConditionDataServices
        {
            get
            {
                return new SQLConditionDataServices();
            }
        }

        /// <inheritdoc/>
        public IDomainDataServices DomainDataServices
        {
            get
            {
                return new SQLDomainDataServices();
            }
        }

        /// <inheritdoc/>
        public IAuthorDataServices AuthorDataServices
        {
            get
            {
                return new SQLAuthorDataServices();
            }
        }

        /// <inheritdoc/>
        public IUserDataServices UserDataServices
        {
            get
            {
                return new SQLUserDataServices();
            }
        }

        /// <inheritdoc/>
        public IBookDataServices BookDataServices
        {
            get
            {
                return new SQLBookDataServices();
            }
        }

        /// <inheritdoc/>
        public IEditionDataServices EditionDataServices
        {
            get
            {
                return new SQLEditionDataServices();
            }
        }

        /// <inheritdoc/>
        public IBorrowedBooksDataServices BorrowedBooksDataServices
        {
            get
            {
                return new SQLBorrowedBooksDataServices();
            }
        }
    }
}
