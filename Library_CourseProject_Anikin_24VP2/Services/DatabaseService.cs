using Library_CourseProject_Anikin_24VP2.Models;
using Microsoft.Data.Sqlite;

namespace Library_CourseProject_Anikin_24VP2.Services
{
    public class DatabaseService
    {
        /// <summary>
        /// Текущий путь к БД в рабочей области
        /// </summary>
        private string CurrentDatabasePath { get; set; } = string.Empty;

        /// <summary>
        /// Строка подключения
        /// </summary>
        private string connectionString = string.Empty;

        /// <summary>
        /// Проверка открытия БД
        /// </summary>
        public bool IsDatabaseOpened
        {
            get => !string.IsNullOrWhiteSpace(CurrentDatabasePath);
        }

        /// <summary>
        /// Создает подключение
        /// </summary>
        /// <returns>Подключение к БД</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public SqliteConnection CreateConnection()
        {
            if (!IsDatabaseOpened)
            {
                throw new InvalidOperationException("База данных не открыта.");
            }

            return new SqliteConnection(connectionString);
        }

        /// <summary>
        /// Папка БД
        /// </summary>
        private readonly string databaseDirectory = Path.Combine(Application.StartupPath, "databases");

        public DatabaseService()
        {
            Directory.CreateDirectory(databaseDirectory);
        }

        /// <summary>
        /// Возвращает полный путь к БД в рабочей области
        /// </summary>
        /// <param name="databaseName">Название БД без расширения</param>
        /// <returns>Путь к БД</returns>
        public string GetDatabasePath(string databaseName)
        {
            return Path.Combine(databaseDirectory, $"{databaseName}.db");
        }

        /// <summary>
        /// Открывает БД
        /// </summary>
        /// <param name="databaseName">Название БД без расширения</param>
        /// <exception cref="FileNotFoundException"></exception>
        public void OpenDatabase(string databaseName)
        {
            string databasePath = GetDatabasePath(databaseName);

            if (!File.Exists(databasePath))
            {
                throw new FileNotFoundException("База данных не найдена.");
            }

            CurrentDatabasePath = databasePath;
            connectionString = $"Data Source={databasePath}";
        }

        /// <summary>
        /// Закрывает БД
        /// </summary>
        public void CloseDatabase()
        {
            CurrentDatabasePath = string.Empty;
            connectionString = string.Empty;
        }

        /// <summary>
        /// Проверка БД на активную
        /// </summary>
        /// <param name="databaseName">Название БД без расширения</param>
        /// <returns>Является ли открытой</returns>
        public bool IsDatabaseActive(string databaseName)
        {
            if (!IsDatabaseOpened)
            {
                return false;
            }

            string path = GetDatabasePath(databaseName);
            return Path.GetFullPath(path) == Path.GetFullPath(CurrentDatabasePath);
        }

        /// <summary>
        /// Проверяет существование БД в рабочей области
        /// </summary>
        /// <param name="databaseName">Название БД без расширения</param>
        public bool DatabaseExists(string databaseName)
        {
            return File.Exists(GetDatabasePath(databaseName));
        }

        /// <summary>
        /// Создает новую БД в рабочей области
        /// </summary>
        /// <param name="databaseName">Название БД без расширения</param>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public void CreateDatabase(string databaseName)
        {
            if (string.IsNullOrWhiteSpace(databaseName))
            {
                throw new ArgumentException("Название базы данных не может быть пустым.");
            }

            string path = GetDatabasePath(databaseName);

            if (File.Exists(path))
            {
                throw new InvalidOperationException("База данных с этим именем уже существует.");
            }

            string databasePath = GetDatabasePath(databaseName);
            CurrentDatabasePath = databasePath;
            connectionString = $"Data Source={databasePath}";

            using var connection = CreateConnection();
            connection.Open();

            string sql = @"
                CREATE TABLE IF NOT EXISTS books
                (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    title TEXT NOT NULL,
                    author TEXT NOT NULL,
                    publish_year INTEGER,
                    genre TEXT,
                    available_count INTEGER DEFAULT 1
                );

                CREATE TABLE IF NOT EXISTS clients
                (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    full_name TEXT NOT NULL,
                    phone TEXT UNIQUE,
                    address TEXT,
                    registration_date TEXT,
                    is_debtor INTEGER DEFAULT 0
                );

                CREATE TABLE IF NOT EXISTS borrowings
                (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    client_id INTEGER NOT NULL,
                    book_id INTEGER NOT NULL,
                    borrow_date TEXT,
                    due_date TEXT,
                    return_date TEXT,
                    is_returned INTEGER DEFAULT 0,
                    FOREIGN KEY (client_id) REFERENCES clients (id) ON DELETE CASCADE ON UPDATE CASCADE,
                    FOREIGN KEY (book_id) REFERENCES books (id) ON UPDATE CASCADE
                );
            ";

            using var command = new SqliteCommand(sql, connection);
            command.ExecuteNonQuery();
        }

        /// <summary>
        /// Удаляет БД из рабочей области
        /// </summary>
        /// <param name="databaseName">Название БД без расширения</param>
        /// <exception cref="FileNotFoundException"></exception>
        public void DeleteDatabase(string databaseName)
        {
            string path = GetDatabasePath(databaseName);

            if (!File.Exists(path))
            {
                throw new FileNotFoundException("БД не найдена.");
            }

            if (IsDatabaseActive(databaseName))
            {
                CloseDatabase();
            }

            SqliteConnection.ClearAllPools();

            File.Delete(path);
        }

        /// <summary>
        /// Переименовывает БД
        /// </summary>
        /// <param name="oldName">Старое имя БД без расширения</param>
        /// <param name="newName">Новое имя БД без расширения</param>
        /// <exception cref="FileNotFoundException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public void RenameDatabase(string oldName, string newName)
        {
            string oldPath = GetDatabasePath(oldName);
            string newPath = GetDatabasePath(newName);

            if (!File.Exists(oldPath))
            {
                throw new FileNotFoundException("БД не найдена.");
            }

            if (File.Exists(newPath))
            {
                throw new InvalidOperationException("БД с таким именем уже существует.");
            }

            SqliteConnection.ClearAllPools();

            if (IsDatabaseActive(oldName))
            {
                CloseDatabase();
                File.Move(oldPath, newPath);
                OpenDatabase(newName);
            }
            else
            {
                File.Move(oldPath, newPath);
            }
        }

        /// <summary>
        /// Экспортирует БД
        /// </summary>
        /// <param name="databaseName">Название экспортируемой БД</param>
        /// <param name="destinationPath">Целевой путь</param>
        public void ExportDatabase(string databaseName, string destinationPath)
        {
            string sourcePath = GetDatabasePath(databaseName);
            File.Copy(sourcePath, destinationPath, true);
        }

        /// <summary>
        /// Испортирует БД
        /// </summary>
        /// <param name="sourcePath">Путь к импортируемой БД</param>
        /// <exception cref="FileNotFoundException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public void ImportDatabase(string sourcePath)
        {
            if (!File.Exists(sourcePath))
            {
                throw new FileNotFoundException("Файл не найден.");
            }

            string fileName = Path.GetFileNameWithoutExtension(sourcePath);
            string databasePath = GetDatabasePath(fileName);

            if (IsDatabaseActive(fileName))
            {
                CloseDatabase();
                SqliteConnection.ClearAllPools();
            }

            File.Copy(sourcePath, databasePath, true);
        }

        /// <summary>
        /// Возвращает список БД
        /// </summary>
        /// <returns>Список структур БД</returns>
        public List<Database> GetDatabases()
        {
            List<Database> result = new List<Database>();

            string[] files = Directory.GetFiles(databaseDirectory, "*.db");

            foreach (string file in files)
            {
                string databaseName = Path.GetFileNameWithoutExtension(file);
                result.Add(new Database
                {
                    Name = databaseName,
                    IsActive = IsDatabaseActive(databaseName)
                });
            }

            return result;
        }
    }
}
