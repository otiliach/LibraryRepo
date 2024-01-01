// <copyright file="DAOFactoryMethod.cs" company="Transilvania University of Brasov">
// Chelmus Otilia-Elena
// </copyright>

namespace DataMapper
{
    using System.Configuration;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>The DAOFactoryMethod class.</summary>
    [ExcludeFromCodeCoverage]
    public static class DAOFactoryMethod
    {
        /// <summary>The current DAO factory.</summary>
        private static readonly IDAOFactory? _currentDAOFactory;

        static DAOFactoryMethod()
        {
            string? currentDataProvider = ConfigurationManager.AppSettings["dataProvider"];
            if (string.IsNullOrWhiteSpace(currentDataProvider))
            {
                _currentDAOFactory = null;
            }
            else
            {
                switch (currentDataProvider.ToLower().Trim())
                {
                    case "sqlserver":
                        _currentDAOFactory = new SQLServerDAOFactory();
                        break;
                    case "oracle":
                        // de fapt ar trebui sa fie un new OracleDAOFactory, dar care nu e inca scris
                        _currentDAOFactory = null;
                        break;
                    default: _currentDAOFactory = new SQLServerDAOFactory();
                        break;
                }
            }
        }

        /// <summary>Gets the current DAO factory.</summary>
        /// <value>The current DAO factory.</value>
        public static IDAOFactory? CurrentDAOFactory
        {
            get
            {
                return _currentDAOFactory;
            }
        }
    }
}
