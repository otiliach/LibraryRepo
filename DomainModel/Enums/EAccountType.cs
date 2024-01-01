// <copyright file="EAccountType.cs" company="Transilvania University of Brasov">
// Chelmus Otilia-Elena
// </copyright>

namespace DomainModel.Enums
{
    /// <summary>The type of account.</summary>
    public enum EAccountType
    {
        /// <summary>Unknown account type.</summary>
        Unknown = 0,

        /// <summary>Reader account.</summary>
        Reader = 1,

        /// <summary>Librarian account.</summary>
        Librarian = 2,

        /// <summary>Librarian and reader account.</summary>
        LibrarianReader = 3,
    }
}
