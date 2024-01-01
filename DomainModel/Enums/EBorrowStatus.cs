// <copyright file="EBorrowStatus.cs" company="Transilvania University of Brasov">
// Chelmus Otilia-Elena
// </copyright>

namespace DomainModel.Enums
{
    /// <summary>The status of borrowed books.</summary>
    public enum EBorrowStatus
    {
        /// <summary>Unknown status type.</summary>
        Unknown = 0,

        /// <summary>Borrowed status type.</summary>
        Borrowed = 1,

        /// <summary>Extended status type.</summary>
        Extended = 2,

        /// <summary>Returned status type.</summary>
        Returned = 3,
    }
}
