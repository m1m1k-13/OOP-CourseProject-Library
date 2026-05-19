using Library_CourseProject_Anikin_24VP2.Forms;
using Library_CourseProject_Anikin_24VP2.Models;
using Library_CourseProject_Anikin_24VP2.Services;
using System.Data;

namespace Library_CourseProject_Anikin_24VP2
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// Сервис работы с БД
        /// </summary>
        private readonly DatabaseService dbService = new DatabaseService();

        /// <summary>
        /// Сервис работы с книгами
        /// </summary>
        private readonly BookService bookService;

        /// <summary>
        /// Сервис работы с клиентами
        /// </summary>
        private readonly ClientService clientService;

        /// <summary>
        /// Сервис работы с выдачами
        /// </summary>
        private readonly BorrowingService borrowingService;

        /// <summary>
        /// Сервис работы с отчетами
        /// </summary>
        private readonly ReportService reportService;

        /// <summary>
        /// Таблица книг
        /// </summary>
        private DataTable booksTable = new DataTable();

        /// <summary>
        /// Таблица клиентов
        /// </summary>
        private DataTable clientsTable = new DataTable();

        /// <summary>
        /// Таблица выдач
        /// </summary>
        private DataTable borrowingsTable = new DataTable();

        /// <summary>
        /// Таймер обновления времени
        /// </summary>
        private System.Windows.Forms.Timer clockTimer;

        public MainForm()
        {
            InitializeComponent();

            bookService = new BookService(dbService);
            clientService = new ClientService(dbService);
            borrowingService = new BorrowingService(dbService);
            reportService = new ReportService();

            booksTable.Columns.Add("Id", typeof(int));
            booksTable.Columns.Add("Title", typeof(string));
            booksTable.Columns.Add("Author", typeof(string));
            booksTable.Columns.Add("PublishYear", typeof(string));
            booksTable.Columns.Add("Genre", typeof(string));
            booksTable.Columns.Add("AvailableCount", typeof(int));
            BooksDataGridView.DataSource = booksTable;

            clientsTable.Columns.Add("Id", typeof(int));
            clientsTable.Columns.Add("FullName", typeof(string));
            clientsTable.Columns.Add("Phone", typeof(string));
            clientsTable.Columns.Add("Address", typeof(string));
            clientsTable.Columns.Add("RegistrationDate", typeof(DateTime));
            clientsTable.Columns.Add("IsDebtor", typeof(bool));
            ClientsDataGridView.DataSource = clientsTable;

            borrowingsTable.Columns.Add("Id", typeof(int));
            borrowingsTable.Columns.Add("Client", typeof(string));
            borrowingsTable.Columns.Add("Book", typeof(string));
            borrowingsTable.Columns.Add("BorrowDate", typeof(DateTime));
            borrowingsTable.Columns.Add("DueDate", typeof(DateTime));
            borrowingsTable.Columns.Add("ReturnDate", typeof(DateTime));
            borrowingsTable.Columns.Add("IsReturned", typeof(bool));
            BorrowingsDataGridView.DataSource = borrowingsTable;

            BookSearchField.SelectedIndex = 0;
            BookSearchFilter.SelectedIndex = 0;
            ClientSearchField.SelectedIndex = 0;
            ClientSearchFilter.SelectedIndex = 0;
            BorrowingSearchField.SelectedIndex = 0;
            BorrowingSearchFilter.SelectedIndex = 0;

            ShowWelcomeForm(true);

            clockTimer = new System.Windows.Forms.Timer { Interval = 1000 };
            clockTimer.Tick += ClockTimer_Tick;
            clockTimer.Start();

            UpdateTreeView();
        }

        /// <summary>
        /// Показ приветственного окна
        /// </summary>
        private void ShowWelcomeForm(bool closeByTimer)
        {
            using (WelcomeForm welcome = new WelcomeForm(closeByTimer))
            {
                welcome.ShowDialog(this);
            }
        }

        /// <summary>
        /// Обновляет дату и время
        /// </summary>
        private void ClockTimer_Tick(object? sender, EventArgs e)
        {
            TimeStatusLabel.Text = DateTime.Now.ToString("HH:mm:ss dd.MM.yyyy");
        }

        /// <summary>
        /// Обновление TreeView со списком баз данных
        /// </summary>
        private void UpdateTreeView()
        {
            DBView.Nodes.Clear();

            TreeNode root = new TreeNode("📁 databases");

            TreeNode? selectedNode = null;

            var databases = dbService.GetDatabases();

            foreach (var db in databases)
            {
                TreeNode node = new TreeNode(db.Name);

                node.Nodes.Add(new TreeNode("Книги"));
                node.Nodes.Add(new TreeNode("Читатели"));
                node.Nodes.Add(new TreeNode("Выдачи книг"));

                root.Nodes.Add(node);

                if (db.IsActive)
                {
                    selectedNode = node;
                    DBStatusLabel.Text = $"БД: {db.Name}.db";
                }
            }

            DBView.Nodes.Add(root);
            root.Expand();

            if (selectedNode != null)
            {
                DBView.SelectedNode = selectedNode;
                selectedNode.Expand();
                selectedNode.EnsureVisible();
                DBView.Focus();
            }
        }

        /// <summary>
        /// Получение имени БД из списка
        /// </summary>
        /// <returns>Название выбранной БД</returns>
        private string? GetSelectedDatabaseName()
        {
            TreeNode? selectedNode = DBView.SelectedNode;

            if (selectedNode == null || (selectedNode.Level != 1 && selectedNode.Level != 2))
            {
                return null;
            }

            if (selectedNode.Level == 2)
            {
                TreeNode? databaseNode = selectedNode.Parent;
                if (databaseNode == null)
                {
                    return null;
                }
                return databaseNode.Text;
            }

            return selectedNode.Text;
        }

        /// <summary>
        /// Метод создания БД
        /// </summary>
        private void CreateDatabase()
        {
            using DatabaseForm form = new DatabaseForm();

            if (form.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string databaseName = form.DBName;

                    if (dbService.DatabaseExists(databaseName))
                    {
                        MessageBox.Show("База данных с этим именем уже существует.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    dbService.CreateDatabase(databaseName);

                    DBStatusLabel.Text = $"БД: {databaseName}.db";
                    StatusLabel.Text = "SQLite: База данных создана";

                    UpdateTreeView();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Метод переименования БД
        /// </summary>
        private void RenameDatabase()
        {
            string? databaseName = GetSelectedDatabaseName();

            if (databaseName == null)
            {
                MessageBox.Show("Выберите базу данных для переименования.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using DatabaseForm form = new DatabaseForm(databaseName);

            if (form.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string newDatabaseName = form.DBName;

                    if (string.Equals(databaseName, newDatabaseName))
                    {
                        return;
                    }

                    if (dbService.DatabaseExists(newDatabaseName))
                    {
                        MessageBox.Show("База данных с этим именем уже существует.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    dbService.RenameDatabase(databaseName, newDatabaseName);

                    DBStatusLabel.Text = $"БД: {newDatabaseName}.db";
                    StatusLabel.Text = "SQLite: База данных переименована";

                    UpdateTreeView();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Метод удаления БД
        /// </summary>
        private void DeleteDatabase()
        {
            try
            {
                string? databaseName = GetSelectedDatabaseName();

                if (databaseName == null)
                {
                    MessageBox.Show("Выберите базу данных для удаления.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult result = MessageBox.Show($"Вы действительно хотите удалить базу данных \"{databaseName}\"?", "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result != DialogResult.Yes)
                {
                    return;
                }

                dbService.DeleteDatabase(databaseName);

                DBStatusLabel.Text = "БД: не выбрана";
                StatusLabel.Text = "SQLite: База данных удалена";

                UpdateTreeView();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Метод сохранения БД в файл
        /// </summary>
        private void SaveDatabase()
        {
            try
            {
                string? databaseName = GetSelectedDatabaseName();

                if (databaseName == null)
                {
                    MessageBox.Show("Выберите базу данных для сохранения.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using SaveFileDialog dialog = new SaveFileDialog();

                dialog.Filter = "SQLite Database (*.db)|*.db";
                dialog.FileName = Path.GetFileName($"{databaseName}.db");

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    dbService.ExportDatabase(databaseName, dialog.FileName);
                    StatusLabel.Text = $"SQLite: База данных сохранена в файл {dialog.FileName}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Метод загрузки БД из файла
        /// </summary>
        private void UploadDatabase()
        {
            using OpenFileDialog dialog = new OpenFileDialog();

            dialog.Filter = "SQLite Database (*.db)|*.db";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string databaseName = Path.GetFileNameWithoutExtension(dialog.FileName);

                    if (dbService.DatabaseExists(databaseName))
                    {
                        DialogResult result = MessageBox.Show($"База данных с этим именем уже существует. Вы хотите заменить \"{databaseName}\"?", "Подтверждение замены", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (result != DialogResult.Yes)
                        {
                            return;
                        }
                    }

                    dbService.ImportDatabase(dialog.FileName);

                    DBStatusLabel.Text = $"БД: {databaseName}.db";
                    StatusLabel.Text = "SQLite: База данных загружена";

                    dbService.OpenDatabase(databaseName);

                    UpdateTreeView();
                    LoadAllTables();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Загружает все таблицы
        /// </summary>
        private void LoadAllTables()
        {
            if (!dbService.IsDatabaseOpened)
            {
                return;
            }

            LoadBooks();
            LoadClients();
            LoadBorrowings();
        }

        /// <summary>
        /// Загружает таблицу книг
        /// </summary>
        private void LoadBooks()
        {
            if (!dbService.IsDatabaseOpened)
            {
                return;
            }

            booksTable.Rows.Clear();

            foreach (var book in bookService.ReadAll())
            {
                booksTable.Rows.Add
                (
                    book.Id,
                    book.Title,
                    book.Author,
                    book.PublishYear,
                    book.Genre,
                    book.AvailableCount
                );
            }

            UpdateNodesCount();
        }

        /// <summary>
        /// Загружает таблицу клиентов
        /// </summary>
        private void LoadClients()
        {
            if (!dbService.IsDatabaseOpened)
            {
                return;
            }

            clientsTable.Rows.Clear();

            foreach (var client in clientService.ReadAll())
            {
                bool IsDebtor = false;
                bool WasDebtor = client.IsDebtor;

                foreach (Borrowing borrowing in borrowingService.ReadActiveByClientId(client.Id))
                {
                    if (DateTime.Now.Date > borrowing.DueDate)
                    {
                        client.IsDebtor = true;
                        IsDebtor = true;
                        break;
                    }
                }

                if (WasDebtor && !IsDebtor)
                {
                    client.IsDebtor = false;
                    clientService.Update(client);
                }
                else if (!WasDebtor && IsDebtor)
                {
                    clientService.Update(client);
                }

                clientsTable.Rows.Add
                (
                    client.Id,
                    client.FullName,
                    client.Phone,
                    client.Address,
                    client.RegistrationDate.Date,
                    client.IsDebtor
                );
            }

            UpdateNodesCount();
        }

        /// <summary>
        /// Загружает таблицу выдач книг
        /// </summary>
        private void LoadBorrowings()
        {
            if (!dbService.IsDatabaseOpened)
            {
                return;
            }

            borrowingsTable.Rows.Clear();

            foreach (var borrowing in borrowingService.ReadAll())
            {
                string? client = clientService.Read(borrowing.ClientId)?.ToString();
                string? book = bookService.Read(borrowing.BookId)?.ToString();

                borrowingsTable.Rows.Add
                (
                    borrowing.Id,
                    client ?? "Клиент не найден",
                    book ?? "Книга не найдена",
                    borrowing.BorrowDate.Date,
                    borrowing.DueDate.Date,
                    borrowing.ReturnDate?.Date,
                    borrowing.IsReturned
                );
            }

            UpdateNodesCount();
        }

        /// <summary>
        /// Создает запись текущей таблицы
        /// </summary>
        private void CreateCurrentNode()
        {
            if (!dbService.IsDatabaseOpened)
            {
                MessageBox.Show("Сначала откройте базу данных.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (TabControl.SelectedTab == BookTabPage)
            {
                CreateBook();
            }
            else if (TabControl.SelectedTab == ClientTabPage)
            {
                CreateClient();
            }

            else if (TabControl.SelectedTab == BorrowingTabPage)
            {
                CreateBorrowing();
            }
        }

        /// <summary>
        /// Добавляет книгу
        /// </summary>
        private void CreateBook()
        {
            try
            {
                using BookForm form = new BookForm();

                if (form.ShowDialog() == DialogResult.OK)
                {
                    Book book = new Book
                    (
                        id: 0,
                        title: form.BookTitle,
                        author: form.BookAuthor,
                        publishYear: form.BookPublishYear,
                        genre: form.BookGenre,
                        availableCount: form.BookCount
                    );

                    bookService.Create(book);

                    LoadBooks();

                    StatusLabel.Text = "SQLite: Книга добавлена";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Регистрация клиента
        /// </summary>
        private void CreateClient()
        {
            try
            {
                using ClientForm form = new ClientForm();

                if (form.ShowDialog() == DialogResult.OK)
                {
                    Client client = new Client
                    (
                        id: 0,
                        fullName: form.ClientName,
                        phone: form.ClientPhone,
                        address: form.ClientAddress,
                        registrationDate: DateTime.Now,
                        isDebtor: false
                    );

                    clientService.Create(client);

                    LoadClients();

                    StatusLabel.Text = "SQLite: Читатель зарегистрирован";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Добавление выдачи книги
        /// </summary>
        private void CreateBorrowing()
        {
            try
            {
                using BorrowingForm form = new BorrowingForm(clientService.ReadAll(), bookService.ReadAll());

                if (form.ShowDialog() == DialogResult.OK)
                {
                    Borrowing borrowing = new Borrowing
                    (
                        id: 0,
                        clientId: form.BorrowClientId,
                        bookId: form.BorrowBookId,
                        borrowDate: DateTime.Now,
                        dueDate: form.BorrowDueDate,
                        returnDate: null,
                        isReturned: false
                    );

                    borrowingService.Create(borrowing);

                    bookService.UpdateCount(borrowing.BookId, -1);

                    LoadAllTables();
                    StatusLabel.Text = "SQLite: Выдача книги добавлена";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Редактирует запись текущей таблицы
        /// </summary>
        private void UpdateCurrentNode()
        {
            if (!dbService.IsDatabaseOpened)
            {
                MessageBox.Show("Сначала откройте базу данных.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (TabControl.SelectedTab == BookTabPage)
            {
                UpdateBook();
            }
            else if (TabControl.SelectedTab == ClientTabPage)
            {
                UpdateClient();
            }
            else if (TabControl.SelectedTab == BorrowingTabPage)
            {
                UpdateBorrowing();
            }
        }

        /// <summary>
        /// Изменяет данные книги
        /// </summary>
        private void UpdateBook()
        {
            try
            {
                if (BooksDataGridView.CurrentRow == null)
                {
                    MessageBox.Show("Выберите книгу.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DataRowView? row = BooksDataGridView.CurrentRow.DataBoundItem as DataRowView;
                if (row == null)
                {
                    return;
                }

                int curId = (int)row["Id"];

                Book? book = bookService.Read(curId);
                if (book == null)
                {
                    MessageBox.Show("Книга не найдена.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using BookForm form = new BookForm(book);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    book.Title = form.BookTitle;
                    book.Author = form.BookAuthor;
                    book.PublishYear = form.BookPublishYear;
                    book.Genre = form.BookGenre;
                    book.AvailableCount = form.BookCount;

                    bookService.Update(book);

                    LoadBooks();
                    StatusLabel.Text = "SQLite: Книга изменена";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Изменяет данные клиента
        /// </summary>
        private void UpdateClient()
        {
            try
            {
                if (ClientsDataGridView.CurrentRow == null)
                {
                    MessageBox.Show("Выберите клиента.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DataRowView? row = ClientsDataGridView.CurrentRow.DataBoundItem as DataRowView;
                if (row == null)
                {
                    return;
                }

                int curId = (int)row["Id"];

                Client? client = clientService.Read(curId);
                if (client == null)
                {
                    MessageBox.Show("Клиент не найден.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using ClientForm form = new ClientForm(client);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    client.FullName = form.ClientName;
                    client.Phone = form.ClientPhone;
                    client.Address = form.ClientAddress;

                    clientService.Update(client);

                    LoadClients();
                    StatusLabel.Text = "SQLite: Читатель изменен";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Изменяет данные выдачи
        /// </summary>
        private void UpdateBorrowing()
        {
            try
            {
                if (BorrowingsDataGridView.CurrentRow == null)
                {
                    MessageBox.Show("Выберите выдачу.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DataRowView? row = BorrowingsDataGridView.CurrentRow.DataBoundItem as DataRowView;
                if (row == null)
                {
                    return;
                }

                int curId = (int)row["Id"];

                Borrowing? borrowing = borrowingService.Read(curId);
                if (borrowing == null)
                {
                    MessageBox.Show("История выдачи не найдена.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using BorrowingForm form = new BorrowingForm(borrowing, clientService.ReadAll(), bookService.ReadAll());
                if (form.ShowDialog() == DialogResult.OK)
                {
                    borrowing.ClientId = form.BorrowClientId;
                    borrowing.DueDate = form.BorrowDueDate;

                    if (!borrowing.IsReturned)
                    {
                        if (borrowing.BookId != form.BorrowBookId)
                        {
                            bookService.UpdateCount(borrowing.BookId, +1);
                            borrowing.BookId = form.BorrowBookId;
                            bookService.UpdateCount(borrowing.BookId, -1);
                        }

                        if (form.BorrowIsReturned)
                        {
                            borrowing.ReturnDate = DateTime.Now.Date;
                            bookService.UpdateCount(borrowing.BookId, +1);
                        }
                    }
                    else
                    {
                        borrowing.BookId = form.BorrowBookId;

                        if (!form.BorrowIsReturned)
                        {
                            borrowing.ReturnDate = null;
                            bookService.UpdateCount(borrowing.BookId, -1);
                        }
                    }

                    borrowing.IsReturned = form.BorrowIsReturned;

                    borrowingService.Update(borrowing);

                    LoadAllTables();
                    StatusLabel.Text = "SQLite: Выдача книги изменена";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Удаляет запись текущей таблицы
        /// </summary>
        private void DeleteCurrentNode()
        {
            if (!dbService.IsDatabaseOpened)
            {
                MessageBox.Show("Сначала откройте базу данных.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (TabControl.SelectedTab == BookTabPage)
            {
                DeleteBook();
            }
            else if (TabControl.SelectedTab == ClientTabPage)
            {
                DeleteClient();
            }
            else if (TabControl.SelectedTab == BorrowingTabPage)
            {
                DeleteBorrowing();
            }
        }

        /// <summary>
        /// Удаляет книгу
        /// </summary>
        private void DeleteBook()
        {
            try
            {
                if (BooksDataGridView.CurrentRow == null)
                {
                    MessageBox.Show("Выберите книгу.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DataRowView? row = BooksDataGridView.CurrentRow.DataBoundItem as DataRowView;

                if (row == null)
                {
                    return;
                }

                DialogResult result = MessageBox.Show($"Удалить книгу \"{row["Title"]}\"?", "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result != DialogResult.Yes)
                {
                    return;
                }

                int id = (int)row["Id"];

                if (borrowingService.HasBookBorrowings(id))
                {
                    MessageBox.Show("Нельзя удалить книгу, так как существуют связанные выдачи.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                bookService.Delete(id);

                LoadBooks();
                StatusLabel.Text = "SQLite: Книга удалена";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Удаляет клиента
        /// </summary>
        private void DeleteClient()
        {
            try
            {
                if (ClientsDataGridView.CurrentRow == null)
                {
                    MessageBox.Show("Выберите читателя.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DataRowView? row = ClientsDataGridView.CurrentRow.DataBoundItem as DataRowView;

                if (row == null)
                {
                    return;
                }

                DialogResult result = MessageBox.Show($"Удалить читателя \"{row["FullName"]}\"?", "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result != DialogResult.Yes)
                {
                    return;
                }

                int id = (int)row["Id"];

                if (borrowingService.HasClientBorrowings(id))
                {
                    MessageBox.Show("Нельзя удалить читателя, так как существуют связанные выдачи.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                clientService.Delete(id);

                LoadClients();
                StatusLabel.Text = "SQLite: Читатель удалён";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Удаляет выдачу
        /// </summary>
        private void DeleteBorrowing()
        {
            try
            {
                if (BorrowingsDataGridView.CurrentRow == null)
                {
                    MessageBox.Show("Выберите выдачу.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DataRowView? row = BorrowingsDataGridView.CurrentRow.DataBoundItem as DataRowView;

                if (row == null)
                {
                    return;
                }

                DialogResult result = MessageBox.Show($"Удалить выдачу книги {row["Book"]} читателю {row["Client"]}?", "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result != DialogResult.Yes)
                {
                    return;
                }

                borrowingService.Delete((int)row["Id"]);

                LoadBorrowings();
                StatusLabel.Text = "SQLite: Выдача удалена";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Открывает БД
        /// </summary>
        private void OpenDatabaseNode(TreeNode databaseNode)
        {
            string databaseName = databaseNode.Text;

            if (!dbService.IsDatabaseActive(databaseName))
            {
                dbService.OpenDatabase(databaseName);
                LoadAllTables();
            }

            DBStatusLabel.Text = $"БД: {databaseName}.db";
            databaseNode.Expand();
        }

        /// <summary>
        /// Открывает таблицу
        /// </summary>
        private void OpenTableNode(TreeNode tableNode)
        {
            TreeNode? databaseNode = tableNode.Parent;
            if (databaseNode == null)
            {
                return;
            }

            OpenDatabaseNode(databaseNode);

            string tableName = tableNode.Text;

            switch (tableName)
            {
                case "Книги":
                    TabControl.SelectedTab = BookTabPage;
                    break;

                case "Читатели":
                    TabControl.SelectedTab = ClientTabPage;
                    break;

                case "Выдачи книг":
                    TabControl.SelectedTab = BorrowingTabPage;
                    break;
            }
        }

        /// <summary>
        /// Обновляет количество записей
        /// </summary>
        private void UpdateNodesCount()
        {
            if (TabControl.SelectedTab == BookTabPage)
            {
                NodesCountStatusLabel.Text = $"Записей: {BooksDataGridView.Rows.Count}";
            }
            else if (TabControl.SelectedTab == ClientTabPage)
            {
                NodesCountStatusLabel.Text = $"Записей: {ClientsDataGridView.Rows.Count}";
            }
            else if (TabControl.SelectedTab == BorrowingTabPage)
            {
                NodesCountStatusLabel.Text = $"Записей: {BorrowingsDataGridView.Rows.Count}";
            }
        }

        /// <summary>
        /// Создает отчет по БД
        /// </summary>
        private void Report()
        {
            try
            {
                string? databaseName = GetSelectedDatabaseName();

                if (databaseName == null)
                {
                    MessageBox.Show("Выберите базу данных для сохранения.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using SaveFileDialog dialog = new SaveFileDialog();

                dialog.Filter = "PDF (*.pdf)|*.pdf";
                dialog.FileName = $"{databaseName}_Report_{DateTime.Now:HH_mm_ss_dd_MM_yyyy}.pdf";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    reportService.GenerateReport(dialog.FileName, databaseName, bookService, clientService, borrowingService);
                    StatusLabel.Text = $"SQLite: Отчет по БД сохранен в файл {dialog.FileName}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Обработка нажатия кнопки Файл - Создать БД в Меню
        /// </summary>
        private void DBCreateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateDatabase();
        }

        /// <summary>
        /// Обработка нажатия кнопки Файл - Переименовать БД в Меню
        /// </summary>
        private void DBRenameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RenameDatabase();
        }

        /// <summary>
        /// Обработка нажатия кнопки Файл - Удалить БД в Меню
        /// </summary>
        private void DBDeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteDatabase();
        }

        /// <summary>
        /// Обработка нажатия кнопки Файл - Сохранить БД в файл в Меню
        /// </summary>
        private void DBSaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveDatabase();
        }

        /// <summary>
        /// Обработка нажатия кнопки Файл - Загрузить БД из файла в Меню
        /// </summary>
        private void DBImportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UploadDatabase();
        }

        /// <summary>
        /// Обработка нажатия кнопки Файл - Выход в Меню
        /// </summary>
        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                clockTimer?.Stop();
                clockTimer?.Dispose();

                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Обработка нажатия кнопки Записи - Добавить в Меню
        /// </summary>
        private void NodeCreateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateCurrentNode();
        }

        /// <summary>
        /// Обработка нажатия кнопки Записи - Редактировать в Меню
        /// </summary>
        private void NodeUpdateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateCurrentNode();
        }

        /// <summary>
        /// Обработка нажатия кнопки Записи - Удалить в Меню
        /// </summary>
        private void NodeDeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteCurrentNode();
        }

        /// <summary>
        /// Обработка нажатия кнопки Поиск - Поиск книг в Меню
        /// </summary>
        private void SearchBookToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TabControl.SelectedTab = BookTabPage;
            BookSearchTextBox.Focus();
        }

        /// <summary>
        /// Обработка нажатия кнопки Поиск - Поиск читателей в Меню
        /// </summary>
        private void SearchClientoolStripMenuItem_Click(object sender, EventArgs e)
        {
            TabControl.SelectedTab = ClientTabPage;
            ClientSearchTextBox.Focus();
        }

        /// <summary>
        /// Обработка нажатия кнопки Поиск - Поиск выдач книг в Меню
        /// </summary>
        private void SearchBorrowingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TabControl.SelectedTab = BorrowingTabPage;
            BorrowingSearchTextBox.Focus();
        }

        /// <summary>
        /// Обработка нажатия кнопки Справка - О программе в Меню
        /// </summary>
        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowWelcomeForm(false);
        }

        /// <summary>
        /// Обработка нажатия кнопки Справка - Сформировать отчёт в Меню
        /// </summary>
        private void ReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Report();
        }

        /// <summary>
        /// Обработка нажатия кнопки Создать БД в Панели инструментов
        /// </summary>
        private void DBCreateToolStripButton_Click(object sender, EventArgs e)
        {
            CreateDatabase();
        }

        /// <summary>
        /// Обработка нажатия кнопки Обновить в Панели инструментов
        /// </summary>
        private void ViewUpdateToolStripButton_Click(object sender, EventArgs e)
        {
            UpdateTreeView();
        }

        /// <summary>
        /// Обработка нажатия кнопки Сохранить БД в файл в Панели инструментов
        /// </summary>
        private void DBSaveToolStripButton_Click(object sender, EventArgs e)
        {
            SaveDatabase();
        }

        /// <summary>
        /// Обработка нажатия кнопки Загрузить БД из файла в Панели инструментов
        /// </summary>
        private void DBImportToolStripButton_Click(object sender, EventArgs e)
        {
            UploadDatabase();
        }

        /// <summary>
        /// Обработка нажатия кнопки Переименовать БД в Панели инструментов
        /// </summary>
        private void DBRenameToolStripButton_Click(object sender, EventArgs e)
        {
            RenameDatabase();
        }

        /// <summary>
        /// Обработка нажатия кнопки Удалить БД в Панели инструментов
        /// </summary>
        private void DBDeleteToolStripButton_Click(object sender, EventArgs e)
        {
            DeleteDatabase();
        }

        /// <summary>
        /// Обработка события выбора БД в TreeView
        /// </summary>
        private void DBView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                TreeNode? selectedNode = e.Node;

                if (selectedNode?.Level == 1)
                {
                    OpenDatabaseNode(selectedNode);
                }
                else if (selectedNode?.Level == 2)
                {
                    OpenTableNode(selectedNode);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Обработка нажатия кнопки Обновить в Панели инструментов
        /// </summary>
        private void RefreshToolStripButton_Click(object sender, EventArgs e)
        {
            LoadAllTables();
        }

        /// <summary>
        /// Обработка нажатия кнопки Добавить запись в Панели инструментов
        /// </summary>
        private void CreateToolStripButton_Click(object sender, EventArgs e)
        {
            CreateCurrentNode();
        }

        /// <summary>
        /// Обработка нажатия кнопки Редактировать запись в Панели инструментов
        /// </summary>
        private void UpdateToolStripButton_Click(object sender, EventArgs e)
        {
            UpdateCurrentNode();
        }

        /// <summary>
        /// Обработка нажатия кнопки Удалить запись в Панели инструментов
        /// </summary>
        private void DeleteToolStripButton_Click(object sender, EventArgs e)
        {
            DeleteCurrentNode();
        }

        /// <summary>
        /// Обработка смены вкладки
        /// </summary>
        private void TabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateNodesCount();
        }

        /// <summary>
        /// Обработка нажатия кнопки Найти в Панели поиска книг
        /// </summary>
        private void BookSearchBtn_Click(object sender, EventArgs e)
        {
            if (BookSearchField.SelectedItem == null || BookSearchFilter.SelectedItem == null)
            {
                return;
            }

            string text = BookSearchTextBox.Text.Trim();
            string field = BookSearchField.SelectedItem.ToString()!;
            string filter = BookSearchFilter.SelectedItem.ToString()!;

            string rowFilter = "";

            switch (field)
            {
                case "Название":
                    rowFilter = $"Title LIKE '%{text}%'";
                    break;

                case "Автор":
                    rowFilter = $"Author LIKE '%{text}%'";
                    break;

                case "Жанр":
                    rowFilter = $"Genre LIKE '%{text}%'";
                    break;

                case "Год издания":
                    rowFilter = $"PublishYear LIKE '%{text}%'";
                    break;
            }

            switch (filter)
            {
                case "Все":
                    break;

                case "В наличии":
                    if (!string.IsNullOrEmpty(rowFilter)) rowFilter += " AND ";
                    rowFilter += "AvailableCount > 0";
                    break;

                case "Нет в наличии":
                    if (!string.IsNullOrEmpty(rowFilter)) rowFilter += " AND ";
                    rowFilter += "AvailableCount = 0";
                    break;
            }

            booksTable.DefaultView.RowFilter = rowFilter;

            UpdateNodesCount();
        }

        /// <summary>
        /// Обработка нажатия кнопки Сбросить в Панели поиска книг
        /// </summary>
        private void BookSearchResetBtn_Click(object sender, EventArgs e)
        {
            booksTable.DefaultView.RowFilter = "";
            BookSearchTextBox.Clear();
            BookSearchFilter.SelectedIndex = 0;
            UpdateNodesCount();
        }

        /// <summary>
        /// Обработка нажатия кнопки Найти в Панели поиска клиентов
        /// </summary>
        private void ClientSearchBtn_Click(object sender, EventArgs e)
        {
            if (ClientSearchField.SelectedItem == null || ClientSearchFilter.SelectedItem == null)
            {
                return;
            }

            string text = ClientSearchTextBox.Text.Trim();
            string field = ClientSearchField.SelectedItem.ToString()!;
            string filter = ClientSearchFilter.SelectedItem.ToString()!;

            string rowFilter = "";

            switch (field)
            {
                case "ФИО":
                    rowFilter = $"FullName LIKE '%{text}%'";
                    break;

                case "Телефон":
                    rowFilter = $"Phone LIKE '%{text}%'";
                    break;

                case "Адрес":
                    rowFilter = $"Address LIKE '%{text}%'";
                    break;
            }

            switch (filter)
            {
                case "Все":
                    break;

                case "Должники":
                    if (!string.IsNullOrEmpty(rowFilter)) rowFilter += " AND ";
                    rowFilter += "IsDebtor = true";
                    break;

                case "Не должники":
                    if (!string.IsNullOrEmpty(rowFilter)) rowFilter += " AND ";
                    rowFilter += "IsDebtor = false";
                    break;
            }

            clientsTable.DefaultView.RowFilter = rowFilter;

            UpdateNodesCount();
        }

        /// <summary>
        /// Обработка нажатия кнопки Сбросить в Панели поиска клиентов
        /// </summary>
        private void ClientSearchResetBtn_Click(object sender, EventArgs e)
        {
            clientsTable.DefaultView.RowFilter = "";
            ClientSearchTextBox.Clear();
            ClientSearchFilter.SelectedIndex = 0;
            UpdateNodesCount();
        }

        /// <summary>
        /// Обработка нажатия кнопки Найти в Панели поиска выдач книг
        /// </summary>
        private void BorrowingSearchBtn_Click(object sender, EventArgs e)
        {
            if (BorrowingSearchField.SelectedItem == null || BorrowingSearchFilter.SelectedItem == null)
            {
                return;
            }

            string text = BorrowingSearchTextBox.Text.Trim();
            string field = BorrowingSearchField.SelectedItem.ToString()!;
            string filter = BorrowingSearchFilter.SelectedItem.ToString()!;

            string rowFilter = "";

            switch (field)
            {
                case "Клиент":
                    rowFilter = $"Client LIKE '%{text}%'";
                    break;

                case "Книга":
                    rowFilter = $"Book LIKE '%{text}%'";
                    break;
            }

            switch (filter)
            {
                case "Все":
                    break;

                case "Возвращенные":
                    if (!string.IsNullOrEmpty(rowFilter)) rowFilter += " AND ";
                    rowFilter += "IsReturned = true";
                    break;

                case "Не возвращенные":
                    if (!string.IsNullOrEmpty(rowFilter)) rowFilter += " AND ";
                    rowFilter += "IsReturned = false";
                    break;

                case "Просроченные":
                    if (!string.IsNullOrEmpty(rowFilter)) rowFilter += " AND ";
                    rowFilter += $"IsReturned = false AND DueDate < #{DateTime.Now.Date:yyyy-MM-dd}#";
                    break;
            }

            borrowingsTable.DefaultView.RowFilter = rowFilter;

            UpdateNodesCount();
        }

        /// <summary>
        /// Обработка нажатия кнопки Сбросить в Панели поиска выдач книг
        /// </summary>
        private void BorrowingSearchResetBtn_Click(object sender, EventArgs e)
        {
            borrowingsTable.DefaultView.RowFilter = "";
            BorrowingSearchTextBox.Clear();
            BorrowingSearchFilter.SelectedIndex = 0;
            UpdateNodesCount();
        }

        /// <summary>
        /// Обработка нажатия кнопки Создать БД в контекстном меню
        /// </summary>
        private void DBCreateContextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateDatabase();
        }

        /// <summary>
        /// Обработка нажатия кнопки Переименовать БД в контекстном меню
        /// </summary>
        private void DBRenameContextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RenameDatabase();
        }

        /// <summary>
        /// Обработка нажатия кнопки Удалить БД в контекстном меню
        /// </summary>
        private void DBDeleteContextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteDatabase();
        }

        /// <summary>
        /// Обработка нажатия кнопки Сохранить БД в файл в контекстном меню
        /// </summary>
        private void DBSaveContextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveDatabase();
        }

        /// <summary>
        /// Обработка нажатия кнопки Загрузить БД из файла в контекстном меню
        /// </summary>
        private void DBUploadContextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UploadDatabase();
        }

        /// <summary>
        /// Обработка нажатия кнопки Сформировать отчёт в контекстном меню
        /// </summary>
        private void ReportContextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Report();
        }

        /// <summary>
        /// Обработка нажатия кнопки Добавить запись в контекстном меню
        /// </summary>
        private void NodeCreateContextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateCurrentNode();
        }

        /// <summary>
        /// Обработка нажатия кнопки Редактировать запись в контекстном меню
        /// </summary>
        private void NodeUpdateContextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateCurrentNode();
        }

        /// <summary>
        /// Обработка нажатия кнопки Удалить запись в контекстном меню
        /// </summary>
        private void NodeDeleteContextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteCurrentNode();
        }
    }
}
