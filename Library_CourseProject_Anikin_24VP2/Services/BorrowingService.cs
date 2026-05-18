using Library_CourseProject_Anikin_24VP2.Models;
using Microsoft.Data.Sqlite;

namespace Library_CourseProject_Anikin_24VP2.Services
{
    public class BorrowingService
    {
        /// <summary>
        /// Сервис работы с БД
        /// </summary>
        private readonly DatabaseService database;

        public BorrowingService(DatabaseService database)
        {
            this.database = database;
        }

        /// <summary>
        /// Добавляет выдачу
        /// </summary>
        /// <param name="borrowing">Добавляемая выдача</param>
        public void Create(Borrowing borrowing)
        {
            using var connection = database.CreateConnection();
            connection.Open();

            string sql = @"
                INSERT INTO borrowings (client_id, book_id, borrow_date, due_date, return_date, is_returned)
                VALUES ( @client_id, @book_id, @borrow_date, @due_date, @return_date, @is_returned);
            ";

            using var command = new SqliteCommand(sql, connection);
            command.Parameters.AddWithValue("@client_id", borrowing.ClientId);
            command.Parameters.AddWithValue("@book_id", borrowing.BookId);
            command.Parameters.AddWithValue("@borrow_date", borrowing.BorrowDate.ToString("yyyy-MM-dd"));
            command.Parameters.AddWithValue("@due_date", borrowing.DueDate.ToString("yyyy-MM-dd"));
            command.Parameters.AddWithValue("@return_date", borrowing.ReturnDate == null ? DBNull.Value : borrowing.ReturnDate.Value.ToString("yyyy-MM-dd"));
            command.Parameters.AddWithValue("@is_returned", borrowing.IsReturned ? 1 : 0);

            command.ExecuteNonQuery();

        }

        /// <summary>
        /// Возвращает список выдач
        /// </summary>
        /// <returns>Список выдач</returns>
        public List<Borrowing> ReadAll()
        {
            List<Borrowing> borrowings = new List<Borrowing>();

            using var connection = database.CreateConnection();
            connection.Open();

            string sql = "SELECT * FROM borrowings";

            using var command = new SqliteCommand(sql, connection);

            using SqliteDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Borrowing borrowing = new Borrowing
                    (
                        id: reader.GetInt32(0),
                        clientId: reader.GetInt32(1),
                        bookId: reader.GetInt32(2),
                        borrowDate: DateTime.Parse(reader.GetString(3)),
                        dueDate: DateTime.Parse(reader.GetString(4)),
                        returnDate: reader.IsDBNull(5) ? null : DateTime.Parse(reader.GetString(5)),
                        isReturned: reader.GetInt32(6) == 1
                    );

                    borrowings.Add(borrowing);
                }
            }

            return borrowings;
        }

        /// <summary>
        /// Возвращает список невозвращенных выдач по Id клиента
        /// </summary>
        /// <param name="id">Id клиента</param>
        /// <returns>Список невозвращенных выдач</returns>
        public List<Borrowing> ReadActiveByClientId(int id)
        {
            List<Borrowing> borrowings = new List<Borrowing>();

            using var connection = database.CreateConnection();
            connection.Open();

            string sql = @"
                SELECT * FROM borrowings
                WHERE client_id = @id AND is_returned = 0;
            ";

            using var command = new SqliteCommand(sql, connection);
            command.Parameters.AddWithValue("@id", id);

            using SqliteDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Borrowing borrowing = new Borrowing
                    (
                        id: reader.GetInt32(0),
                        clientId: reader.GetInt32(1),
                        bookId: reader.GetInt32(2),
                        borrowDate: DateTime.Parse(reader.GetString(3)),
                        dueDate: DateTime.Parse(reader.GetString(4)),
                        returnDate: reader.IsDBNull(5) ? null : DateTime.Parse(reader.GetString(5)),
                        isReturned: reader.GetInt32(6) == 1
                    );

                    borrowings.Add(borrowing);
                }
            }

            return borrowings;
        }

        /// <summary>
        /// Возвращает выдачу по Id
        /// </summary>
        /// <param name="id">Id выдачи</param>
        /// <returns>Модель выдачи или null</returns>
        public Borrowing? Read(int id)
        {
            using var connection = database.CreateConnection();
            connection.Open();

            string sql = "SELECT * FROM borrowings WHERE id = @id";

            using var command = new SqliteCommand(sql, connection);
            command.Parameters.AddWithValue("@id", id);

            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                return new Borrowing
                (
                    id: reader.GetInt32(0),
                    clientId: reader.GetInt32(1),
                    bookId: reader.GetInt32(2),
                    borrowDate: DateTime.Parse(reader.GetString(3)),
                    dueDate: DateTime.Parse(reader.GetString(4)),
                    returnDate: reader.IsDBNull(5) ? null : DateTime.Parse(reader.GetString(5)),
                    isReturned: reader.GetInt32(6) == 1
                );
            }

            return null;
        }

        /// <summary>
        /// Проверяет наличие выдач у книги
        /// </summary>
        /// <param name="bookId">Id книги</param>
        /// <returns>Есть ли выдачи</returns>
        public bool HasBookBorrowings(int bookId)
        {
            using var connection = database.CreateConnection();
            connection.Open();

            string sql = @"
                SELECT COUNT(*)
                FROM borrowings
                WHERE book_id = @bookId;
            ";

            using var command = new SqliteCommand(sql, connection);
            command.Parameters.AddWithValue("@bookId", bookId);

            long count = (long)(command.ExecuteScalar() ?? 0);

            return count > 0;
        }

        /// <summary>
        /// Проверяет наличие выдач у клиента
        /// </summary>
        /// <param name="clientId">Id клиента</param>
        /// <returns>Есть ли выдачи</returns>
        public bool HasClientBorrowings(int clientId)
        {
            using var connection = database.CreateConnection();
            connection.Open();

            string sql = @"
                SELECT COUNT(*)
                FROM borrowings
                WHERE client_id = @clientId;
            ";

            using var command = new SqliteCommand(sql, connection);
            command.Parameters.AddWithValue("@clientId", clientId);

            long count = (long)(command.ExecuteScalar() ?? 0);

            return count > 0;
        }

        /// <summary>
        /// Обновляет выдачу
        /// </summary>
        /// <param name="borrowing">Обновленная выдача</param>
        public void Update(Borrowing borrowing)
        {
            using var connection = database.CreateConnection();
            connection.Open();

            string sql = @"
                UPDATE borrowings
                SET
                    client_id = @client_id,
                    book_id = @book_id,
                    borrow_date = @borrow_date,
                    due_date = @due_date,
                    return_date = @return_date,
                    is_returned = @is_returned
                WHERE id = @id;
            ";

            using var command = new SqliteCommand(sql, connection);
            command.Parameters.AddWithValue("@id", borrowing.Id);
            command.Parameters.AddWithValue("@client_id", borrowing.ClientId);
            command.Parameters.AddWithValue("@book_id", borrowing.BookId);
            command.Parameters.AddWithValue("@borrow_date", borrowing.BorrowDate.ToString("yyyy-MM-dd"));
            command.Parameters.AddWithValue("@due_date", borrowing.DueDate.ToString("yyyy-MM-dd"));
            command.Parameters.AddWithValue("@return_date", borrowing.ReturnDate == null ? DBNull.Value : borrowing.ReturnDate.Value.ToString("yyyy-MM-dd"));
            command.Parameters.AddWithValue("@is_returned", borrowing.IsReturned ? 1 : 0);
            command.ExecuteNonQuery();
        }

        /// <summary>
        /// Удаляет выдачу
        /// </summary>
        /// <param name="id">Id выдачи</param>
        public void Delete(int id)
        {
            using var connection = database.CreateConnection();
            connection.Open();

            string sql = "DELETE FROM borrowings WHERE id = @id";

            using var command = new SqliteCommand(sql, connection);
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();
        }
    }
}
