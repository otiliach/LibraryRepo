// <copyright file="Program.cs" company="Transilvania University of Brasov">
// Chelmus Otilia-Elena
// </copyright>

using DataMapper;
using DomainModel.Enums;
using DomainModel.Models;
using log4net;
using ServiceLayer.Implementation;
using ServiceLayer.Interfaces;

/// <summary>The program.</summary>
internal class Program
{
    private static void Main(string[] args)
    {
        if (DAOFactoryMethod.CurrentDAOFactory != null)
        {
            ILog logger = LogManager.GetLogger(Environment.MachineName);

            IConditionServices conditionServices = new ConditionServicesImplementation(DAOFactoryMethod.CurrentDAOFactory.ConditionDataServices, logger);
            IDomainServices domainServices = new DomainServicesImplementation(DAOFactoryMethod.CurrentDAOFactory.DomainDataServices, logger);
            IAuthorServices authorServices = new AuthorServicesImplementation(DAOFactoryMethod.CurrentDAOFactory.AuthorDataServices, logger);
            IUserServices userServices = new UserServicesImplementation(DAOFactoryMethod.CurrentDAOFactory.UserDataServices, logger);
            IBookServices bookServices = new BookServicesImplementation(DAOFactoryMethod.CurrentDAOFactory.BookDataServices, DAOFactoryMethod.CurrentDAOFactory.DomainDataServices, DAOFactoryMethod.CurrentDAOFactory.ConditionDataServices, logger);
            IEditionServices editionServices = new EditionServicesImplementation(DAOFactoryMethod.CurrentDAOFactory.EditionDataServices, logger);
            IBorrowedBooksServices borrowedBooksServices = new BorrowedBooksServicesImplementation(DAOFactoryMethod.CurrentDAOFactory.BorrowedBooksDataServices, DAOFactoryMethod.CurrentDAOFactory.ConditionDataServices, DAOFactoryMethod.CurrentDAOFactory.EditionDataServices, logger);

            if (userServices.GetUsers().Count != 0)
            {
                // Add conditions
                conditionServices.AddCondition(null);
                conditionServices.AddCondition(new Condition(null, null, 0));
                conditionServices.AddCondition(new Condition(string.Empty, "abcdefghijklmnopqrstuvwxyz", 0));
                conditionServices.AddCondition(new Condition("blablabla", string.Empty, 0));
                conditionServices.AddCondition(new Condition("DOMENII", "Nr. max domenii.", 5));

                Condition? domeniiCondition = conditionServices.GetConditionByName("DOMENII");
                if (domeniiCondition != null)
                {
                    domeniiCondition.Description = "O carte nu poate face parte din mai mult de [DOMENII] domenii.";
                    domeniiCondition.Value = 3;
                    conditionServices.UpdateCondition(domeniiCondition);
                }

                conditionServices.AddCondition(new Condition("NMC", "Un cititor poate imprumuta un numar maxim de carti [NMC] intr-o perioada <PER> de zile.", 9));
                conditionServices.AddCondition(new Condition("PER", "Un cititor poate imprumuta un numar maxim de carti <NMC> intr-o perioada [PER] de zile.", 30));
                conditionServices.AddCondition(new Condition("C", "La un imprumut cititorii pot prelua cel mult [C] carti.", 3));
                conditionServices.AddCondition(new Condition("D", "Cititorii nu pot imprumuta mai mult de [D] carti dintr-un acelasi domeniu in ultimele <L> luni.", 9));
                conditionServices.AddCondition(new Condition("L", "Cititorii nu pot imprumuta mai mult de <D> carti dintr-un acelasi domeniu in ultimele [L] luni.", 2));
                conditionServices.AddCondition(new Condition("LIM", "Suma prelungirilor acordate in ultimele 3 luni nu poate depasi o valoare limita [LIM] de zile.", 28));
                conditionServices.AddCondition(new Condition("DELTA", "Cititorii nu pot imprumuta aceeasi carte de mai multe ori intr-un interval [DELTA] de zile specificat.", 60));
                conditionServices.AddCondition(new Condition("NCZ", "Cititorii Pot imprumuta cel mult [NCZ] carti intr-o zi.", 5));
                conditionServices.AddCondition(new Condition("PERSIMP", "Personalul bibliotecii nu poate acorda mai mult de [PERSIMP] carti intr-o zi.", 50));
                conditionServices.AddCondition(new Condition("TIMP_IMP", "Cititorul trebuie sa returneze cartile la cel mult [TIMP_IMPRUMUT] zile de la data imprumutarii.", 14));

                // Add domains
                domainServices.AddDomain(new Domain("Știință", null));

                Domain? parentDomain = domainServices.GetDomainByName("Știință");

                if (parentDomain != null)
                {
                    domainServices.AddDomain(new Domain("Matematică", parentDomain));
                    domainServices.AddDomain(new Domain("Fizică", parentDomain));
                    domainServices.AddDomain(new Domain("Chimie", parentDomain));
                    domainServices.AddDomain(new Domain("Informatică", parentDomain));
                }

                parentDomain = domainServices.GetDomainByName("Informatică");

                if (parentDomain != null)
                {
                    domainServices.AddDomain(new Domain("Algoritmi", parentDomain));
                    domainServices.AddDomain(new Domain("Structuri de date", parentDomain));
                    domainServices.AddDomain(new Domain("Programare", parentDomain));
                    domainServices.AddDomain(new Domain("Baze de date", parentDomain));
                    domainServices.AddDomain(new Domain("Rețele de calculatoare", parentDomain));
                }

                parentDomain = domainServices.GetDomainByName("Algoritmi");

                if (parentDomain != null)
                {
                    domainServices.AddDomain(new Domain("Algoritmica grafurilor", parentDomain));
                    domainServices.AddDomain(new Domain("Algoritmi cuantici", parentDomain));
                }

                List<Domain> domains = domainServices.GetDomains().ToList();

                // Print domains
                foreach (Domain currentDomain in domains)
                {
                    Console.Write(currentDomain.Name);

                    Domain? parent = currentDomain.ParentDomain;

                    while (parent != null)
                    {
                        Console.Write(" --> " + parent.Name);
                        parent = parent.ParentDomain;
                    }

                    Console.WriteLine();
                }

                authorServices.AddAuthor(null);
                authorServices.AddAuthor(new Author("Mich T. Good"));
                authorServices.AddAuthor(new Author("Roberto Tamassia"));
                authorServices.AddAuthor(new Author("David Mount"));

                // Add users
                userServices.AddUser(new User(
                    "Otilia",
                    "Chelmus",
                    "Str. BlaBlaBla, nr. 3, bl. C23, sc. A, et. 2, ap. 15",
                    "OtiC@FakeMail.com",
                    "0123456789",
                    "Pswrd@23",
                    EAccountType.Reader));

                userServices.AddUser(new User(
                    "Otilia",
                    "Matei",
                    "Str. BlaBla, nr. 7, bl. C3, sc. A, et. 2, ap. 15",
                    "OtiM@FakeMail.com",
                    "0123456780",
                    "Pswrd@23",
                    EAccountType.Reader));

                userServices.AddUser(new User(
                    "Librarian",
                    "Debu",
                    "Str. BlaBlaBla, nr. 1, bl. C2, sc. A, et. 1, ap. 1",
                    "Debu@FakeMail.com",
                    "0123456788",
                    "Libra@23",
                    EAccountType.Librarian));

                userServices.AddUser(new User(
                    "Elena",
                    "Libra",
                    "Str. BlaBlaBla, nr. 1, bl. C2, sc. A, et. 1, ap. 2",
                    "Elena@FakeMail.com",
                    "0123456787",
                    "Libra@23",
                    EAccountType.LibrarianReader));

                // Add books.
                Domain? domain1 = domainServices.GetDomainByName("Structuri de date");
                Domain? domain2 = domainServices.GetDomainByName("Algoritmi");
                List<Author> authors = authorServices.GetAuthors().ToList();
                List<Domain> bookDomains = new List<Domain>();

                if (domain1 != null)
                {
                    bookDomains.Add(domain1);
                }

                if (domain2 != null)
                {
                    bookDomains.Add(domain2);
                }

                List<Edition> bookEditions = new List<Edition>()
                {
                    new Edition("Litera", 2010, 714, 1, EBookType.Paperback, 5, 2),
                };

                // add new edition.
                Edition testEdition = new Edition("TEST", 2010, 714, 1, EBookType.Paperback, 5, 2);
                editionServices.AddEdition(testEdition);

                bookServices.AddBook(new Book(
                    "TEST",
                    authors,
                    bookDomains,
                    new List<Edition> { testEdition }));

                // add books.
                bookServices.AddBook(new Book(
                    "Data Structures & Algorithms in C++",
                    authors,
                    bookDomains,
                    bookEditions));

                Domain? sd = domainServices.GetDomainByName("Structuri de date");

                if (sd != null)
                {
                    bookServices.AddBook(new Book(
                        "Data Structures in C",
                        authors,
                        new List<Domain>() { sd },
                        new List<Edition>() { new Edition("Litera", 2007, 914, 1, EBookType.Hardcover, 5, 2) }));
                }

                // Add book without domain.
                bookServices.AddBook(new Book(
                    "Data Structures in C#",
                    authors,
                    new List<Domain>(),
                    new List<Edition>() { new Edition("Litera", 2012, 914, 1, EBookType.Hardcover, 5, 2) }));

                // Add book with directly related domains.
                Edition edition = new Edition("Litera", 2012, 914, 1, EBookType.Hardcover, 5, 2);
                Domain? info = domainServices.GetDomainByName("Informatică");
                Domain? alg = domainServices.GetDomainByName("Algoritmi");

                if (info != null && alg != null)
                {
                    Book newBook = new Book(
                            "Algorithms in C#",
                            authors,
                            new List<Domain>() { info, alg },
                            new List<Edition>() { edition });

                    bookServices.AddBook(newBook);
                }

                // Get books by domain.
                Domain? domain = domainServices.GetDomainByName("Informatică");

                if (domain == null)
                {
                    Console.WriteLine("Nu s-a gasit domeniul!");
                }
                else
                {
                    Console.Write("\nCartile din domeniul Informatică:\n");
                    List<Book> booksInDomain = bookServices.GetBooksByDomain(domain).ToList();
                    foreach (Book bookInDomain in booksInDomain)
                    {
                        Console.WriteLine(bookInDomain.Title);
                    }
                }

                if (domain != null)
                {
                    List<Domain> childDomains = domainServices.GetChildDomains(domain).ToList();

                    Console.WriteLine("\nChild domains of <Informatica>\n");

                    foreach (Domain currentDomain in childDomains)
                    {
                        Console.WriteLine($"{currentDomain.Name}");
                    }

                    List<Book> itBooks = bookServices.GetBooksByDomain(domain).ToList();

                    Console.WriteLine("\nBooks of domain or child domains of <Informatica>\n");

                    foreach (Book book in itBooks)
                    {
                        Console.WriteLine($"{book.Title}");
                    }
                }

                if (sd != null)
                {
                    List<Book> sdBooks = bookServices.GetBooksByDomain(sd).ToList();

                    Console.WriteLine("\nBooks of domain or child domains of <Structuri de date>\n");

                    foreach (Book book in sdBooks)
                    {
                        Console.WriteLine($"{book.Title}");
                    }
                }

                if (alg != null)
                {
                    List<Book> algBooks = bookServices.GetBooksByDomain(alg).ToList();

                    Console.WriteLine("\nBooks of domain or child domains of <Algoritmi>\n");

                    foreach (Book book in algBooks)
                    {
                        Console.WriteLine($"{book.Title}");
                    }
                }

                Book? book1 = bookServices.GetBookById(1);

                // Add BorrowedBooks
                User? reader = userServices.GetUserByEmail("OtiC@FakeMail.com");
                User? librarian = userServices.GetUserByEmail("Debu@FakeMail.com");
                Book? dsaBook = bookServices.GetBookByTitle("Data Structures & Algorithms in C++");
                Book? dsBook = bookServices.GetBookByTitle("Data Structures in C");

                if (reader != null && librarian != null &&
                    dsaBook != null && dsaBook.Editions.Count > 0 &&
                    dsBook != null && dsBook.Editions.Count > 0)
                {
                    bool response;

                    do
                    {
                        response = borrowedBooksServices.AddBorrowedBooks(
                            new BorrowedBooks(
                                DateTime.Now,
                                reader,
                                new List<Edition>()
                                {
                                dsaBook.Editions.ElementAt(0),
                                dsBook.Editions.ElementAt(0),
                                },
                                DateTime.Now.AddDays(conditionServices.GetTIMPIMP()),
                                librarian,
                                EBorrowStatus.Borrowed));
                    }
                    while (response);
                }
            }

            Console.WriteLine("Done!");
        }
    }
}