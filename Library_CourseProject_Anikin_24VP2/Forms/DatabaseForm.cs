namespace Library_CourseProject_Anikin_24VP2.Forms
{
    public partial class DatabaseForm : Form
    {
        public string DBName
        {
            get => DBNameTextBox.Text.Trim();
        }

        public DatabaseForm()
        {
            InitializeComponent();
        }

        public DatabaseForm(string databaseName)
        {
            InitializeComponent();

            this.Text = "Переименование БД";
            SaveBtn.Text = "Сохранить";

            DBNameTextBox.Text = databaseName;
        }

        /// <summary>
        /// Обработка нажатия кнопки Сохранить
        /// </summary>
        private void SaveBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(DBName))
                {
                    MessageBox.Show("Введите название БД.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult = DialogResult.OK;
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
