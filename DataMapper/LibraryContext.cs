// <copyright file="LibraryContext.cs" company="Transilvania University of Brasov">
// Chelmus Otilia-Elena
// </copyright>

namespace DataMapper
{
    using System.Data.Entity;
    using System.Diagnostics.CodeAnalysis;
    using DomainModel.Models;

    /// <summary>The library context.</summary>
    [ExcludeFromCodeCoverage]
    public class LibraryContext : DbContext
    {
        /// <summary>Initializes a new instance of the <see cref="LibraryContext" /> class.</summary>
        public LibraryContext()
            : base("LibraryDbConnectionString")
        {
            this.Configuration.LazyLoadingEnabled = true;
        }

        /// <summary>Gets or sets the authors.</summary>
        /// <value>The authors.</value>
        public virtual DbSet<Author> Authors { get; set; }

        /// <summary>Gets or sets the books.</summary>
        /// <value>The books.</value>
        public virtual DbSet<Book> Books { get; set; }

        /// <summary>Gets or sets the borrowed books.</summary>
        /// <value>The borrowed books.</value>
        public virtual DbSet<BorrowedBooks> BorrowedBooks { get; set; }

        /// <summary>Gets or sets the conditions.</summary>
        /// <value>The conditions.</value>
        public virtual DbSet<Condition> Conditions { get; set; }

        /// <summary>Gets or sets the domains.</summary>
        /// <value>The domains.</value>
        public virtual DbSet<Domain> Domains { get; set; }

        /// <summary>Gets or sets the editions.</summary>
        /// <value>The editions.</value>
        public virtual DbSet<Edition> Editions { get; set; }

        /// <summary>Gets or sets the users.</summary>
        /// <value>The users.</value>
        public virtual DbSet<User> Users { get; set; }

        /// <summary>
        /// This method is called when the model for a derived context has been initialized, but
        /// before the model has been locked down and used to initialize the context.  The default
        /// implementation of this method does nothing, but it can be overridden in a derived class
        /// such that the model can be further configured before it is locked down.
        /// </summary>
        /// <param name="modelBuilder">The builder that defines the model for the context being created.</param>
        /// <remarks>
        /// Typically, this method is called only once when the first instance of a derived context
        /// is created.  The model for that context is then cached and is for all further instances of
        /// the context in the app domain.  This caching can be disabled by setting the ModelCaching
        /// property on the given ModelBuilder, but note that this can seriously degrade performance.
        /// More control over caching is provided through use of the DbModelBuilder and DbContextFactory
        /// classes directly.
        /// </remarks>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BorrowedBooks>()
                .HasRequired(bb => bb.Reader)
                .WithMany(r => r.BorrowedBooks)
                .WillCascadeOnDelete(false);
        }
    }
}
