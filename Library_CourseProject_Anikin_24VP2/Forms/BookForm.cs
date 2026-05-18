using Library_CourseProject_Anikin_24VP2.Models;

namespace Library_CourseProject_Anikin_24VP2.Forms
{
    public partial class BookForm : Form
    {
        private Book? currentBook;

        public string BookTitle
        {
            get => TitleTextBox.Text.Trim();
        }

        public string BookAuthor
        {
            get => AuthorTextBox.Text.Trim();
        }

        public string BookGenre
        {
            get => GenreTextBox.Text.Trim();
        }

        public int BookPublishYear
        {
            get => (int)PublishYearUpDown.Value;
        }

        public int BookCount
        {
            get => (int)CountUpDown.Value;
        }

        public BookForm()
        {
            InitializeComponent();
            PublishYearUpDown.Maximum = DateTime.Now.Year;
        }

        public BookForm(Book book)
        {
            InitializeComponent();
            PublishYearUpDown.Maximum = DateTime.Now.Year;

            this.Text = "Изменение книги";
            SaveBtn.Text = "Изменить";

            currentBook = book;
            FillFields();
        }

        /// <summary>
        /// Заполняет поля данными текущей книги
        /// </summary>
        private void FillFields()
        {
            if (currentBook == null)
            {
                return;
            }

            TitleTextBox.Text = currentBook.Title;
            AuthorTextBox.Text = currentBook.Author;
            GenreTextBox.Text = currentBook.Genre;
            PublishYearUpDown.Value = currentBook.PublishYear;
            CountUpDown.Value = currentBook.AvailableCount;
        }

        /// <summary>
        /// Проверяет корректность данных книги
        /// </summary>
        private bool ValidateBook()
        {
            if (string.IsNullOrWhiteSpace(BookTitle))
            {
                MessageBox.Show("Введите название книги.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TitleTextBox.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(BookAuthor))
            {
                MessageBox.Show("Введите автора книги.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                AuthorTextBox.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(BookGenre))
            {
                MessageBox.Show("Введите жанр книги.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                GenreTextBox.Focus();
                return false;
            }

            int year = BookPublishYear;
            int currentYear = DateTime.Now.Year;

            if (year < 1450 || year > currentYear)
            {
                MessageBox.Show($"Год должен быть в диапазоне 1450–{currentYear}.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                PublishYearUpDown.Focus();
                return false;
            }

            if (BookCount < 0)
            {
                MessageBox.Show("Количество не может быть отрицательным.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                CountUpDown.Focus();
                return false;
            }

            return true;
        }

        /// <summary>
        /// Обработка нажатия кнопки Создать
        /// </summary>
        private void SaveBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateBook())
                {
                    DialogResult = DialogResult.OK;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Обработка нажатия кнопки Отмена
        /// </summary>
        private void CancelBtn_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult = DialogResult.Cancel;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
