// <copyright file="SQLDomainDataServices.cs" company="Transilvania University of Brasov">
// Chelmus Otilia-Elena
// </copyright>

namespace DataMapper.SqlServerDAO
{
    using System.Data.Entity;
    using System.Diagnostics.CodeAnalysis;
    using DataMapper.Interfaces;
    using DomainModel.Models;
    using log4net;

    /// <summary>The domain data services.</summary>
    [ExcludeFromCodeCoverage]
    public class SQLDomainDataServices : IDomainDataServices
    {
        private static readonly ILog Logger = LogManager.GetLogger(System.Environment.MachineName);

        /// <inheritdoc/>
        public bool AddDomain(Domain domain)
        {
            using (LibraryContext libraryContext = new LibraryContext())
            {
                try
                {
                    if (domain.ParentDomain != null)
                    {
                        libraryContext.Domains.Attach(domain.ParentDomain);
                    }

                    libraryContext.Domains.Add(domain);
                    libraryContext.SaveChanges();
                }
                catch (Exception exception)
                {
                    Logger.Error("Error while adding a new domain: " + exception.Message.ToString());
                    return false;
                }
            }

            Logger.Info("Domain added successfully!");
            return true;
        }

        /// <inheritdoc/>
        public ICollection<Domain> GetDomains()
        {
            ICollection<Domain> domains = new List<Domain>();

            using (LibraryContext libraryContext = new LibraryContext())
            {
                domains = libraryContext.Domains.Include("ParentDomain").OrderBy((domain) => domain.Id).ToList();
            }

            return domains;
        }

        /// <inheritdoc/>
        public Domain? GetDomainById(int id)
        {
            Domain? domain = null;

            using (LibraryContext libraryContext = new LibraryContext())
            {
                domain = libraryContext.Domains.Include("ParentDomain").FirstOrDefault((domain) => domain.Id == id);
            }

            return domain;
        }

        /// <inheritdoc/>
        public Domain? GetDomainByName(string name)
        {
            Domain? domain = null;

            using (LibraryContext libraryContext = new LibraryContext())
            {
                domain = libraryContext.Domains.Include("ParentDomain").FirstOrDefault((domain) => domain.Name == name);
            }

            return domain;
        }

        /// <inheritdoc/>
        public ICollection<Domain> GetChildDomains(Domain domain)
        {
            List<Domain>? childDomains = new List<Domain>();
            List<Domain> domains = this.GetDomains().ToList();

            if (domains.Count == 0)
            {
                return childDomains;
            }

            foreach (Domain currentDomain in domains)
            {
                Domain? parent = currentDomain;

                while (parent != null)
                {
                    if (parent.Id == domain.Id)
                    {
                        childDomains.Add(currentDomain);
                    }

                    parent = parent.ParentDomain;
                }
            }

            return childDomains;
        }

        /// <inheritdoc/>
        public bool UpdateDomain(Domain domain)
        {
            using (LibraryContext libraryContext = new LibraryContext())
            {
                try
                {
                    libraryContext.Domains.Attach(domain);
                    libraryContext.Entry(domain).State = EntityState.Modified;
                    libraryContext.SaveChanges();
                }
                catch (Exception exception)
                {
                    Logger.Error("Error while updating a domain: " + exception.Message.ToString());
                    return false;
                }
            }

            Logger.Info("Domain updated successfully!");
            return true;
        }

        /// <inheritdoc/>
        public bool DeleteDomain(Domain domain)
        {
            using (LibraryContext libraryContext = new LibraryContext())
            {
                try
                {
                    libraryContext.Domains.Attach(domain);
                    libraryContext.Domains.Remove(domain);
                    libraryContext.SaveChanges();
                }
                catch (Exception exception)
                {
                    Logger.Error("Error while deleting a new domain: " + exception.Message.ToString());
                    return false;
                }
            }

            Logger.Info("Domain deleted successfully!");
            return true;
        }
    }
}
