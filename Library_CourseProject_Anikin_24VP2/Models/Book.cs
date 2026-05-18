namespace Library_CourseProject_Anikin_24VP2.Models
{
    /// <summary>
    /// Модель книги библиотеки
    /// </summary>
    public class Book
    {
        /// <summary>
        /// Идентификатор книги
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название книги
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Автор книги
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// Год издания книги
        /// </summary>
        public int PublishYear { get; set; }

        /// <summary>
        /// Жанр книги
        /// </summary>
        public string Genre { get; set; }

        /// <summary>
        /// Количество доступных экземпляров
        /// </summary>
        public int AvailableCount { get; set; }

        public Book() {}

        public Book(int id, string title, string author, int publishYear, string genre, int availableCount)
        {
            Id = id;
            Title = title;
            Author = author;
            PublishYear = publishYear;
            Genre = genre;
            AvailableCount = availableCount;
        }

        /// <summary>
        /// Возвращает строковое представление книги
        /// </summary>
        public override string ToString()
        {
            return $"{Title} - {Author}";
        }
    }
}
