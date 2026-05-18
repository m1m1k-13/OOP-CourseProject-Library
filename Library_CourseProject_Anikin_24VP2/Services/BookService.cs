using Microsoft.Data.Sqlite;
using Library_CourseProject_Anikin_24VP2.Models;

namespace Library_CourseProject_Anikin_24VP2.Services
{
    public class BookService
    {
        /// <summary>
        /// Сервис работы с БД
        /// </summary>
        private readonly DatabaseService database;

        public BookService(DatabaseService database)
        {
            this.database = database;
        }

        /// <summary>
        /// Добавляет книгу
        /// </summary>
        /// <param name="book">Добавляемая книга</param>
        public void Create(Book book)
        {
            using var connection = database.CreateConnection();
            connection.Open();

            string sql = @"
                INSERT INTO books (title, author, publish_year, genre, available_count)
                VALUES ( @title, @author, @publishYear, @genre, @availableCount);
                ";

            using var command = new SqliteCommand(sql, connection);
            command.Parameters.AddWithValue("@title", book.Title);
            command.Parameters.AddWithValue("@author", book.Author);
            command.Parameters.AddWithValue("@publishYear", book.PublishYear);
            command.Parameters.AddWithValue("@genre", book.Genre);
            command.Parameters.AddWithValue("@availableCount", book.AvailableCount);

            command.ExecuteNonQuery();
        }

        /// <summary>
        /// Возвращает список книг
        /// </summary>
        /// <returns>Список книг</returns>
        public List<Book> ReadAll()
        {
            List<Book> books = new List<Book>();

            using var connection = database.CreateConnection();
            connection.Open();

            string sql = "SELECT * FROM books";

            using var command = new SqliteCommand(sql, connection);

            using var reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Book book = new Book
                    (
                        id: reader.GetInt32(0),
                        title: reader.GetString(1),
                        author: reader.GetString(2),
                        publishYear: reader.GetInt32(3),
                        genre: reader.GetString(4),
                        availableCount: reader.GetInt32(5)
                    );

                    books.Add(book);
                }
            }
            
            return books;
        }

        /// <summary>
        /// Возвращает книгу по Id
        /// </summary>
        /// <param name="id">Id книги</param>
        /// <returns>Модель книги или null</returns>
        public Book? Read(int id)
        {
            using var connection = database.CreateConnection();
            connection.Open();

            string sql = "SELECT * FROM books WHERE id = @id";

            using var command = new SqliteCommand(sql, connection);
            command.Parameters.AddWithValue("@id", id);

            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                return new Book
                (
                    id: reader.GetInt32(0),
                    title: reader.GetString(1),
                    author: reader.GetString(2),
                    publishYear: reader.GetInt32(3),
                    genre: reader.GetString(4),
                    availableCount: reader.GetInt32(5)
                );
            }

            return null;
        }

        /// <summary>
        /// Обновляет книгу
        /// </summary>
        /// <param name="book">Обновленная книга</param>
        public void Update(Book book)
        {
            using var connection = database.CreateConnection();
            connection.Open();

            string sql = @"
                UPDATE books
                SET
                    title = @title,
                    author = @author,
                    publish_year = @publishYear,
                    genre = @genre,
                    available_count = @availableCount
                WHERE id = @id;
            ";

            using var command = new SqliteCommand(sql, connection);
            command.Parameters.AddWithValue("@id", book.Id);
            command.Parameters.AddWithValue("@title", book.Title);
            command.Parameters.AddWithValue("@author", book.Author);
            command.Parameters.AddWithValue("@publishYear", book.PublishYear);
            command.Parameters.AddWithValue("@genre", book.Genre);
            command.Parameters.AddWithValue("@availableCount", book.AvailableCount);
            command.ExecuteNonQuery();
        }

        /// <summary>
        /// Обновляет доступное количество книг
        /// </summary>
        /// <param name="id">Id изменяемой книги</param>
        /// <param name="count">Изменение количества</param>
        public void UpdateCount(int id, int count)
        {
            using var connection = database.CreateConnection();
            connection.Open();

            string sql = @"
                UPDATE books
                SET
                    available_count = available_count + @delta
                WHERE id = @id;
            ";

            using var command = new SqliteCommand(sql, connection);
            command.Parameters.AddWithValue("@id", id);
            command.Parameters.AddWithValue("@delta", count);
            command.ExecuteNonQuery();
        }

        /// <summary>
        /// Удаляет книгу
        /// </summary>
        /// <param name="id">Id удаляемой книги</param>
        public void Delete(int id)
        {
            using var connection = database.CreateConnection();
            connection.Open();

            string sql = "DELETE FROM books WHERE id = @id";

            using var command = new SqliteCommand(sql, connection);
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();
        }
    }
}