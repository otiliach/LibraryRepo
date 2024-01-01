// <copyright file="IConditionServices.cs" company="Transilvania University of Brasov">
// Chelmus Otilia-Elena
// </copyright>

namespace ServiceLayer.Interfaces
{
    using DomainModel.Models;

    /// <summary>The condition services interface.</summary>
    public interface IConditionServices
    {
        /// <summary>Adds the condition.</summary>
        /// <param name="condition">The condition.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        bool AddCondition(Condition? condition);

        /// <summary>Gets the conditions.</summary>
        /// <returns>
        ///   <br />
        /// </returns>
        ICollection<Condition> GetConditions();

        /// <summary>Gets the condition by identifier.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        Condition? GetConditionById(int id);

        /// <summary>Gets the name of the condition by.</summary>
        /// <param name="name">The name.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        Condition? GetConditionByName(string name);

        /// <summary>Gets the domenii.</summary>
        /// <returns>Value of the domenii.</returns>
        int GetDOMENII();

        /// <summary>Gets the NMC.</summary>
        /// <returns>Value of the NMC.</returns>
        int GetNMC();

        /// <summary>Gets the per.</summary>
        /// <returns>Value of the PER.</returns>
        int GetPER();

        /// <summary>Gets the C.</summary>
        /// <returns>Value of the C.</returns>
        int GetC();

        /// <summary>Gets the D.</summary>
        /// <returns>Value of the D.</returns>
        int GetD();

        /// <summary>Gets the L.</summary>
        /// <returns>Value of the L.</returns>
        int GetL();

        /// <summary>Gets the LIM.</summary>
        /// <returns>Value of the LIM.</returns>
        int GetLIM();

        /// <summary>Gets the delta.</summary>
        /// <returns>Value of the DELTA.</returns>
        int GetDELTA();

        /// <summary>Gets the NCZ.</summary>
        /// <returns>Value of the NCZ.</returns>
        int GetNCZ();

        /// <summary>Gets the PERSIMP.</summary>
        /// <returns>Value of the PERSIMP.</returns>
        int GetPERSIMP();

        /// <summary>Gets the TIMPIMP.</summary>
        /// <returns>Value of the TIMPIMP.</returns>
        int GetTIMPIMP();

        /// <summary>Updates the condition.</summary>
        /// <param name="condition">The condition.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        bool UpdateCondition(Condition condition);

        /// <summary>Deletes the condition.</summary>
        /// <param name="condition">The condition.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        bool DeleteCondition(Condition condition);
    }
}
