// <copyright file="SQLConditionDataServices.cs" company="Transilvania University of Brasov">
// Chelmus Otilia-Elena
// </copyright>

namespace DataMapper.SqlServerDAO
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Diagnostics.CodeAnalysis;
    using DataMapper.Interfaces;
    using DomainModel.Models;
    using log4net;

    /// <summary>The condition data services.</summary>
    [ExcludeFromCodeCoverage]
    public class SQLConditionDataServices : IConditionDataServices
    {
        private static readonly ILog Logger = LogManager.GetLogger(System.Environment.MachineName);

        /// <inheritdoc/>
        public bool AddCondition(Condition condition)
        {
            using (LibraryContext libraryContext = new LibraryContext())
            {
                try
                {
                    libraryContext.Conditions.Add(condition);
                    libraryContext.SaveChanges();
                }
                catch (Exception exception)
                {
                    Logger.Error("Error while adding a new condition: " + exception.Message.ToString());
                    return false;
                }
            }

            Logger.Info("Condition added successfully!");
            return true;
        }

        /// <inheritdoc/>
        public ICollection<Condition> GetConditions()
        {
            ICollection<Condition> conditions = new List<Condition>();

            using (LibraryContext libraryContext = new LibraryContext())
            {
                conditions = libraryContext.Conditions.OrderBy((condition) => condition.Id).ToList();
            }

            return conditions;
        }

        /// <inheritdoc/>
        public Condition? GetConditionById(int id)
        {
            Condition? condition = null;

            using (LibraryContext libraryContext = new LibraryContext())
            {
                condition = libraryContext.Conditions.FirstOrDefault((condition) => condition.Id == id);
            }

            return condition;
        }

        /// <inheritdoc/>
        public Condition? GetConditionByName(string name)
        {
            Condition? condition = null;

            using (LibraryContext libraryContext = new LibraryContext())
            {
                condition = libraryContext.Conditions.FirstOrDefault((condition) => condition.Name == name);
            }

            return condition;
        }

        /// <inheritdoc/>
        public int GetDOMENII()
        {
            return this.GetConditionByName("DOMENII")?.Value ?? 0;
        }

        /// <inheritdoc/>
        public int GetNMC()
        {
            return this.GetConditionByName("NMC")?.Value ?? 0;
        }

        /// <inheritdoc/>
        public int GetPER()
        {
            return this.GetConditionByName("PER")?.Value ?? 0;
        }

        /// <inheritdoc/>
        public int GetC()
        {
            return this.GetConditionByName("C")?.Value ?? 0;
        }

        /// <inheritdoc/>
        public int GetD()
        {
            return this.GetConditionByName("D")?.Value ?? 0;
        }

        /// <inheritdoc/>
        public int GetL()
        {
            return this.GetConditionByName("L")?.Value ?? 0;
        }

        /// <inheritdoc/>
        public int GetLIM()
        {
            return this.GetConditionByName("LIM")?.Value ?? 0;
        }

        /// <inheritdoc/>
        public int GetDELTA()
        {
            return this.GetConditionByName("DELTA")?.Value ?? 0;
        }

        /// <inheritdoc/>
        public int GetNCZ()
        {
            return this.GetConditionByName("NCZ")?.Value ?? 0;
        }

        /// <inheritdoc/>
        public int GetPERSIMP()
        {
            return this.GetConditionByName("PERSIMP")?.Value ?? 0;
        }

        /// <inheritdoc/>
        public int GetTIMPIMP()
        {
            return this.GetConditionByName("TIMP_IMP")?.Value ?? 0;
        }

        /// <inheritdoc/>
        public bool UpdateCondition(Condition condition)
        {
            using (LibraryContext libraryContext = new LibraryContext())
            {
                try
                {
                    libraryContext.Conditions.Attach(condition);
                    libraryContext.Entry(condition).State = EntityState.Modified;
                    libraryContext.SaveChanges();
                }
                catch (Exception exception)
                {
                    Logger.Error("Error while updating a condition: " + exception.Message.ToString());
                    return false;
                }
            }

            Logger.Info("Condition updated successfully!");
            return true;
        }

        /// <inheritdoc/>
        public bool DeleteCondition(Condition condition)
        {
            using (LibraryContext libraryContext = new LibraryContext())
            {
                try
                {
                    libraryContext.Conditions.Attach(condition);
                    libraryContext.Conditions.Remove(condition);
                    libraryContext.SaveChanges();
                }
                catch (Exception exception)
                {
                    Logger.Error("Error while deleting a new condition: " + exception.Message.ToString());
                    return false;
                }
            }

            Logger.Info("Condition deleted successfully!");
            return true;
        }

        /// <inheritdoc/>
        public bool ConditionNameAlreadyExists(string name)
        {
            if (this.GetConditionByName(name) != null)
            {
                return true;
            }

            return false;
        }
    }
}
