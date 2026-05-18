using Library_CourseProject_Anikin_24VP2.Models;
using Library_CourseProject_Anikin_24VP2.Services;

namespace TestLibraryProject
{
    [TestClass]
    public sealed class LibraryServiceTests
    {
        private DatabaseService databaseService;
        private BookService bookService;
        private ClientService clientService;
        private BorrowingService borrowingService;

        private string TestDatabaseName = "test_db";

        /// <summary>
        /// Выполняется перед каждым тестом
        /// </summary>
        [TestInitialize]
        public void Setup()
        {
            TestDatabaseName = $"test_db_{Guid.NewGuid():N}";

            databaseService = new DatabaseService();

            if (databaseService.DatabaseExists(TestDatabaseName))
            {
                databaseService.DeleteDatabase(TestDatabaseName);
            }

            databaseService.CreateDatabase(TestDatabaseName);

            bookService = new BookService(databaseService);
            clientService = new ClientService(databaseService);
            borrowingService = new BorrowingService(databaseService);
        }

        /// <summary>
        /// Выполняется после каждого теста
        /// </summary>
        [TestCleanup]
        public void Cleanup()
        {
            databaseService.CloseDatabase();

            if (databaseService.DatabaseExists(TestDatabaseName))
            {
                databaseService.DeleteDatabase(TestDatabaseName);
            }
        }

        /// <summary>
        /// Проверка создания книги
        /// </summary>
        [TestMethod]
        public void CreateBook_ShouldAddBook()
        {
            Book book = new Book(0, "1984", "George Orwell", 1949, "Dystopia", 5);

            bookService.Create(book);

            List<Book> books = bookService.ReadAll();

            Assert.HasCount(1, books);
            Assert.AreEqual("1984", books[0].Title);
        }

        /// <summary>
        /// Проверка чтения книги
        /// </summary>
        [TestMethod]
        public void ReadBook_ShouldReturnBook()
        {
            Book book = new Book(0, "Clean Code", "Robert Martin", 2008, "Programming", 3);

            bookService.Create(book);

            Book savedBook = bookService.ReadAll().First();
            Book? result = bookService.Read(savedBook.Id);

            Assert.IsNotNull(result);
            Assert.AreEqual("Clean Code", result.Title);
        }

        /// <summary>
        /// Проверка обновления книги
        /// </summary>
        [TestMethod]
        public void UpdateBook_ShouldUpdateTitle()
        {
            Book book = new Book(0, "Old Title", "Author", 2000, "Genre", 1);

            bookService.Create(book);

            Book savedBook = bookService.ReadAll().First();
            savedBook.Title = "New Title";

            bookService.Update(savedBook);

            Book updatedBook = bookService.Read(savedBook.Id)!;
            Assert.AreEqual("New Title", updatedBook.Title);
        }

        /// <summary>
        /// Проверка удаления книги
        /// </summary>
        [TestMethod]
        public void DeleteBook_ShouldRemoveBook()
        {
            Book book = new Book(0, "Delete Me", "Author", 2020, "Test", 1);

            bookService.Create(book);

            Book savedBook = bookService.ReadAll().First();

            bookService.Delete(savedBook.Id);

            List<Book> books = bookService.ReadAll();

            Assert.IsEmpty(books);
        }

        /// <summary>
        /// Проверка изменения количества книг
        /// </summary>
        [TestMethod]
        public void UpdateCount_ShouldChangeCount()
        {
            Book book = new Book(0, "Count Book", "Author", 2022, "Test", 10);

            bookService.Create(book);

            Book savedBook = bookService.ReadAll().First();

            bookService.UpdateCount(savedBook.Id, -3);

            Book updatedBook = bookService.Read(savedBook.Id)!;

            Assert.AreEqual(7, updatedBook.AvailableCount);
        }

        /// <summary>
        /// Проверка создания клиента
        /// </summary>
        [TestMethod]
        public void CreateClient_ShouldAddClient()
        {
            Client client = new Client(0, "Иван Иванов", "+79999999999", "Москва", DateTime.Now, false);

            clientService.Create(client);

            List<Client> clients = clientService.ReadAll();

            Assert.HasCount(1, clients);
            Assert.AreEqual("Иван Иванов", clients[0].FullName);
        }

        /// <summary>
        /// Проверка создания выдачи
        /// </summary>
        [TestMethod]
        public void CreateBorrowing_ShouldAddBorrowing()
        {
            Book book = new Book(0, "Book", "Author", 2024, "Genre", 2);

            bookService.Create(book);

            Client client = new Client(0, "Client", "123456", "Address", DateTime.Now, false);

            clientService.Create(client);

            Book savedBook = bookService.ReadAll().First();
            Client savedClient = clientService.ReadAll().First();

            Borrowing borrowing = new Borrowing(0, savedClient.Id, savedBook.Id, DateTime.Now, DateTime.Now.AddDays(7), null, false);

            borrowingService.Create(borrowing);

            List<Borrowing> borrowings = borrowingService.ReadAll();

            Assert.HasCount(1, borrowings);
            Assert.AreEqual(savedClient.Id, borrowings[0].ClientId);
        }

        /// <summary>
        /// Проверка выдач у книги
        /// </summary>
        [TestMethod]
        public void HasBookBorrowings_ShouldReturnTrue()
        {
            Book book = new Book(0, "Book", "Author", 2020, "Genre", 1);

            bookService.Create(book);

            Client client = new Client(0, "Client", "555", "Address", DateTime.Now, false);

            clientService.Create(client);

            Book savedBook = bookService.ReadAll().First();
            Client savedClient = clientService.ReadAll().First();

            Borrowing borrowing = new Borrowing(0, savedClient.Id, savedBook.Id, DateTime.Now, DateTime.Now.AddDays(5), null, false);

            borrowingService.Create(borrowing);

            bool result = borrowingService.HasBookBorrowings(savedBook.Id);

            Assert.IsTrue(result);
        }

        /// <summary>
        /// Проверка активных выдач клиента
        /// </summary>
        [TestMethod]
        public void ReadActiveByClientId_ShouldReturnOnlyActive()
        {
            Book book = new Book(0, "Book", "Author", 2020, "Genre", 1);

            bookService.Create(book);

            Client client = new Client(0, "Client", "777", "Address", DateTime.Now, false);

            clientService.Create(client);

            Book savedBook = bookService.ReadAll().First();
            Client savedClient = clientService.ReadAll().First();

            Borrowing borrowing = new Borrowing(0, savedClient.Id, savedBook.Id, DateTime.Now, DateTime.Now.AddDays(7), null, false);

            borrowingService.Create(borrowing);

            List<Borrowing> active = borrowingService.ReadActiveByClientId(savedClient.Id);

            Assert.HasCount(1, active);
            Assert.IsFalse(active[0].IsReturned);
        }
    }
}