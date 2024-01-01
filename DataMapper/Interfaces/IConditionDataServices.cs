// <copyright file="IConditionDataServices.cs" company="Transilvania University of Brasov">
// Chelmus Otilia-Elena
// </copyright>

namespace DataMapper.Interfaces
{
    using DomainModel.Models;

    /// <summary>The <see cref="Book"/> data services.</summary>
    public interface IConditionDataServices
    {
        /// <summary>Adds a new condition to the database.</summary>
        /// <param name="condition">The condition.</param>
        /// <returns>
        ///     <b>true</b> if the condition was added successfully to the database.
        ///     <br/>
        ///     <b>false</b> if an error occured while adding the condition to the database.
        /// </returns>
        bool AddCondition(Condition condition);

        /// <summary>Gets all the conditions from the database.</summary>
        /// <returns>A list of all existing conditions.</returns>
        ICollection<Condition> GetConditions();

        /// <summary>
        /// Gets the condition with the provided id from the database.
        /// </summary>
        /// <param name="id">The condition's id.</param>
        /// <returns> The condition with the provided id.</returns>
        Condition? GetConditionById(int id);

        /// <summary>
        /// Gets the condition with the provided name.
        /// </summary>
        /// <param name="name">The name of the condition.</param>
        /// <returns>The condition with the provided name.</returns>
        Condition? GetConditionByName(string name);

        /// <summary>
        /// Gets the DOMENII condition.
        /// </summary>
        /// <returns>The maximum number of domains a book can have.</returns>
        int GetDOMENII();

        /// <summary>
        /// Gets the NMC condition.
        /// </summary>
        /// <returns>The maximum number of books a user can borrow in PER days.</returns>
        int GetNMC();

        /// <summary>
        /// Gets the PER condition.
        /// </summary>
        /// <returns> The number of days in which a user can borrow NMC books.</returns>
        int GetPER();

        /// <summary>
        /// Gets the C condition.
        /// </summary>
        /// <returns>The maximum number of books a user can borrow at one time.</returns>
        int GetC();

        /// <summary>
        /// Gets the D condition.
        /// </summary>
        /// <returns>The maximum number of books from the same domain in the last L months.</returns>
        int GetD();

        /// <summary>
        /// Gets the L condition.
        /// </summary>
        /// <returns>The number of months for the maximum D number of books from the same domain.</returns>
        int GetL();

        /// <summary>
        /// Gets the LIM condition.
        /// </summary>
        /// <returns>The mamimum sum of extensions in the last 3 months.</returns>
        int GetLIM();

        /// <summary>
        /// Gets the DELTA condition.
        /// </summary>
        /// <returns>The number of days until the user can borrow the same book again.</returns>
        int GetDELTA();

        /// <summary>
        /// Gets the NCZ condition.
        /// </summary>
        /// <returns>The maximum number of books a reader can borrow in one day.</returns>
        int GetNCZ();

        /// <summary>
        /// Gets the PERSIMP condition.
        /// </summary>
        /// <returns>The maximum number of books a librarian can give in one day.</returns>
        int GetPERSIMP();

        /// <summary>
        /// Gets the TIMPIMP condition.
        /// </summary>
        /// <returns>The number of days for the reader to return the borrowed books.</returns>
        int GetTIMPIMP();

        /// <summary>
        /// Updates the condition.
        /// </summary>
        /// <param name="condition">The condition.</param>
        /// <returns>
        ///     <b>true</b> if the condition was updated successfully.
        ///     <br/>
        ///     <b>false</b> if an error occured while updating the condition.
        /// </returns>
        bool UpdateCondition(Condition condition);

        /// <summary>
        /// Deletes the condition.
        /// </summary>
        /// <param name="condition">The condition.</param>
        /// <returns>
        ///     <b>true</b> if the condition was deleted successfully.
        ///     <br/>
        ///     <b>false</b> if an error occured while deleting the condition.
        /// </returns>
        bool DeleteCondition(Condition condition);

        /// <summary>
        /// Checks if an condition with the same name already exists in the database.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>
        ///     <b>true</b> if a condition with the provided name exists in the database.
        ///     <br/>
        ///     <b>false</b> if a condition with the provided name doesn't exist in the database.
        /// </returns>
        bool ConditionNameAlreadyExists(string name);
    }
}
