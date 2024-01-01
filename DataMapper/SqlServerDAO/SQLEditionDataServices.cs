// <copyright file="SQLEditionDataServices.cs" company="Transilvania University of Brasov">
// Chelmus Otilia-Elena
// </copyright>

namespace DataMapper.SqlServerDAO
{
    using System.Data.Entity;
    using System.Diagnostics.CodeAnalysis;
    using DataMapper.Interfaces;
    using DomainModel.Models;
    using log4net;

    /// <summary>The edition data services.</summary>
    [ExcludeFromCodeCoverage]
    public class SQLEditionDataServices : IEditionDataServices
    {
        private static readonly ILog Logger = LogManager.GetLogger(System.Environment.MachineName);

        /// <inheritdoc/>
        public bool AddEdition(Edition edition)
        {
            using (LibraryContext libraryContext = new LibraryContext())
            {
                try
                {
                    libraryContext.Editions.Add(edition);
                    libraryContext.SaveChanges();
                }
                catch (Exception exception)
                {
                    Logger.Error("Error while adding a new edition: " + exception.Message.ToString());
                    return false;
                }
            }

            Logger.Info("Edition added successfully!");
            return true;
        }

        /// <inheritdoc/>
        public ICollection<Edition> GetEditions()
        {
            ICollection<Edition> editions = new List<Edition>();

            using (LibraryContext libraryContext = new LibraryContext())
            {
                editions = libraryContext.Editions.OrderBy((edition) => edition.Id).ToList();
            }

            return editions;
        }

        /// <inheritdoc/>
        public Edition? GetEditionById(int id)
        {
            Edition? edition = null;

            using (LibraryContext libraryContext = new LibraryContext())
            {
                edition = libraryContext.Editions.FirstOrDefault((edition) => edition.Id == id);
            }

            return edition;
        }

        /// <inheritdoc/>
        public bool UpdateEdition(Edition edition)
        {
            using (LibraryContext libraryContext = new LibraryContext())
            {
                try
                {
                    libraryContext.Editions.Attach(edition);
                    libraryContext.Entry(edition).State = EntityState.Modified;
                    libraryContext.SaveChanges();
                }
                catch (Exception exception)
                {
                    Logger.Error("Error while updating a edition: " + exception.Message.ToString());
                    return false;
                }
            }

            Logger.Info("Edition updated successfully!");
            return true;
        }

        /// <inheritdoc/>
        public bool DeleteEdition(Edition edition)
        {
            using (LibraryContext libraryContext = new LibraryContext())
            {
                try
                {
                    libraryContext.Editions.Attach(edition);
                    libraryContext.Editions.Remove(edition);
                    libraryContext.SaveChanges();
                }
                catch (Exception exception)
                {
                    Logger.Error("Error while deleting a new edition: " + exception.Message.ToString());
                    return false;
                }
            }

            Logger.Info("Edition deleted successfully!");
            return true;
        }
    }
}
