// <copyright file="ConditionServicesImplementation.cs" company="Transilvania University of Brasov">
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

    /// <summary>The Condition Services Implementation class.</summary>
    /// <seealso cref="ServiceLayer.Interfaces.IConditionServices" />
    public class ConditionServicesImplementation : IConditionServices
    {
        /// <summary>The logger.</summary>
        private ILog logger;

        /// <summary>The condition data services.</summary>
        private IConditionDataServices conditionDataServices;

        /// <summary>Initializes a new instance of the <see cref="ConditionServicesImplementation" /> class.</summary>
        /// <param name="conditionDataServices">The condition data services.</param>
        /// <param name="logger">The logger.</param>
        public ConditionServicesImplementation(IConditionDataServices conditionDataServices, ILog logger)
        {
            this.conditionDataServices = conditionDataServices;
            this.logger = logger;
        }

        /// <inheritdoc/>
        public bool AddCondition(Condition? condition)
        {
            // Check if <condition> is null.
            if (condition == null)
            {
                this.logger.Warn("Attempted to add a null condition.");
                return false;
            }

            // Check if <condition> is invalid.
            var context = new ValidationContext(condition, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            if (!Validator.TryValidateObject(condition, context, results, true))
            {
                this.logger.Warn("Attempted to add an invalid condition. " + string.Join(' ', results));
                return false;
            }

            // Check if <condition> already exist.
            if (this.conditionDataServices.ConditionNameAlreadyExists(condition.Name))
            {
                this.logger.Warn("Attempted to add an already existing condition.");
                return false;
            }

            // If all checks pass call AddCondition.
            return this.conditionDataServices.AddCondition(condition);
        }

        /// <inheritdoc/>
        public ICollection<Condition> GetConditions()
        {
            return this.conditionDataServices.GetConditions();
        }

        /// <inheritdoc/>
        public Condition? GetConditionById(int id)
        {
            return this.conditionDataServices.GetConditionById(id);
        }

        /// <inheritdoc/>
        public Condition? GetConditionByName(string name)
        {
            return this.conditionDataServices.GetConditionByName(name);
        }

        /// <inheritdoc/>
        public int GetDOMENII()
        {
            return this.conditionDataServices.GetDOMENII();
        }

        /// <inheritdoc/>
        public int GetNMC()
        {
            return this.conditionDataServices.GetNMC();
        }

        /// <inheritdoc/>
        public int GetPER()
        {
            return this.conditionDataServices.GetPER();
        }

        /// <inheritdoc/>
        public int GetC()
        {
            return this.conditionDataServices.GetC();
        }

        /// <inheritdoc/>
        public int GetD()
        {
            return this.conditionDataServices.GetD();
        }

        /// <inheritdoc/>
        public int GetL()
        {
            return this.conditionDataServices.GetL();
        }

        /// <inheritdoc/>
        public int GetLIM()
        {
            return this.conditionDataServices.GetLIM();
        }

        /// <inheritdoc/>
        public int GetDELTA()
        {
            return this.conditionDataServices.GetDELTA();
        }

        /// <inheritdoc/>
        public int GetNCZ()
        {
            return this.conditionDataServices.GetNCZ();
        }

        /// <inheritdoc/>
        public int GetPERSIMP()
        {
            return this.conditionDataServices.GetPERSIMP();
        }

        /// <inheritdoc/>
        public int GetTIMPIMP()
        {
            return this.conditionDataServices.GetTIMPIMP();
        }

        /// <inheritdoc/>
        public bool UpdateCondition(Condition condition)
        {
            // Check if <condition> is null.
            if (condition == null)
            {
                this.logger.Warn("Attempted to update a null condition.");
                return false;
            }

            // Check if <condition> is invalid.
            var context = new ValidationContext(condition, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            if (!Validator.TryValidateObject(condition, context, results, true))
            {
                this.logger.Warn("Attempted to update an invalid condition. " + string.Join(' ', results));
                return false;
            }

            // Check if <condition> exists.
            if (this.conditionDataServices.GetConditionById(condition.Id) == null)
            {
                this.logger.Warn("Attempted to update a non-existing condition.");
                return false;
            }

            // Check if new <condition name> already exist.
            if (this.conditionDataServices.GetConditionByName(condition.Name) != null)
            {
                this.logger.Warn("Attempted to update a condition name to an already existing condition name.");
                return false;
            }

            // If all checks pass call UpdateCondition.
            return this.conditionDataServices.UpdateCondition(condition);
        }

        /// <inheritdoc/>
        public bool DeleteCondition(Condition condition)
        {
            // Check if <condition> is null.
            if (condition == null)
            {
                this.logger.Warn("Attempted to delete a null condition.");
                return false;
            }

            // Check if <condition> exists.
            if (this.conditionDataServices.GetConditionById(condition.Id) == null)
            {
                this.logger.Warn("Attempted to delete a non-existing condition.");
                return false;
            }

            // If all checks pass call DeleteCondition.
            return this.conditionDataServices.DeleteCondition(condition);
        }
    }
}
