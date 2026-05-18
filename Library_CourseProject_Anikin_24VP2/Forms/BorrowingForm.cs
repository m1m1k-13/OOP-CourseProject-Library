using Library_CourseProject_Anikin_24VP2.Models;

namespace Library_CourseProject_Anikin_24VP2.Forms
{
    public partial class BorrowingForm : Form
    {
        private Borrowing? currentBorrowing;

        public int BorrowClientId
        {
            get
            {
                Client? client = ClientComboBox.SelectedItem as Client;
                return client?.Id ?? 0;
            }
        }

        public int BorrowBookId
        {
            get
            {
                Book? book = BookComboBox.SelectedItem as Book;
                return book?.Id ?? 0;
            }
        }

        public DateTime BorrowDueDate
        {
            get => DueDateTimePicker.Value;
        }

        public bool BorrowIsReturned
        {
            get => IsReturnedCheckBox.Checked;
        }

        public BorrowingForm(List<Client> clients, List<Book> books)
        {
            InitializeComponent();
            DueDateTimePicker.Value = DateTime.Today.AddDays(30);

            IsReturnedCheckBox.Visible = false;

            LoadClients(clients);
            LoadBooks(books.Where(b => b.AvailableCount > 0).ToList());
        }

        public BorrowingForm(Borrowing borrowing, List<Client> clients, List<Book> books)
        {
            InitializeComponent();

            this.Text = "Редактирование выдачи";
            SaveBtn.Text = "Изменить";
            IsReturnedCheckBox.Visible = true;

            LoadClients(clients);
            LoadBooks(books);

            currentBorrowing = borrowing;
            FillFields();
        }

        /// <summary>
        /// Загружает список клиентов
        /// </summary>
        private void LoadClients(List<Client> clients)
        {
            ClientComboBox.DataSource = clients;

            ClientComboBox.DisplayMember = "FullName";
            ClientComboBox.ValueMember = "Id";

            ClientComboBox.SelectedIndex = -1;
        }

        /// <summary>
        /// Загружает список книг
        /// </summary>
        private void LoadBooks(List<Book> books)
        {
            BookComboBox.DataSource = books;

            BookComboBox.DisplayMember = "Title";
            BookComboBox.ValueMember = "Id";

            BookComboBox.SelectedIndex = -1;
        }

        /// <summary>
        /// Заполняет поля данными текущей выдачи
        /// </summary>
        private void FillFields()
        {
            if (currentBorrowing == null)
            {
                return;
            }

            ClientComboBox.SelectedValue = currentBorrowing.ClientId;
            BookComboBox.SelectedValue = currentBorrowing.BookId;
            DueDateTimePicker.Value = currentBorrowing.DueDate;
            IsReturnedCheckBox.Checked = currentBorrowing.IsReturned;
        }

        /// <summary>
        /// Проверяет корректность данных выдачи
        /// </summary>
        private bool ValidateBorrowing()
        {
            if (ClientComboBox.SelectedItem == null)
            {
                MessageBox.Show("Выберите читателя.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ClientComboBox.Focus();
                return false;
            }

            if (BookComboBox.SelectedItem == null)
            {
                MessageBox.Show("Выберите книгу.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                BookComboBox.Focus();
                return false;
            }

            return true;
        }

        /// <summary>
        /// Обработка нажатия кнопки Сохранить
        /// </summary>
        private void SaveBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateBorrowing())
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
