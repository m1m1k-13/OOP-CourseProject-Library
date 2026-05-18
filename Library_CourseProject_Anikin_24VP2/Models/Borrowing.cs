namespace Library_CourseProject_Anikin_24VP2.Models
{
    /// <summary>
    /// Модель выдачи книги клиенту
    /// </summary>
    public class Borrowing
    {
        /// <summary>
        /// Идентификатор выдачи
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Идентификатор клиента
        /// </summary>
        public int ClientId { get; set; }

        /// <summary>
        /// Идентификатор книги
        /// </summary>
        public int BookId { get; set; }

        /// <summary>
        /// Дата выдачи книги
        /// </summary>
        public DateTime BorrowDate { get; set; }

        /// <summary>
        /// Срок возврата книги
        /// </summary>
        public DateTime DueDate { get; set; }

        /// <summary>
        /// Дата возврата книги
        /// </summary>
        public DateTime? ReturnDate { get; set; }

        /// <summary>
        /// Флаг возврата книги
        /// </summary>
        public bool IsReturned { get; set; }

        public Borrowing() {}

        public Borrowing(int id, int clientId, int bookId, DateTime borrowDate, DateTime dueDate, DateTime? returnDate, bool isReturned)
        {
            Id = id;
            ClientId = clientId;
            BookId = bookId;
            BorrowDate = borrowDate;
            DueDate = dueDate;
            ReturnDate = returnDate;
            IsReturned = isReturned;
        }
    }
}
