using Library_CourseProject_Anikin_24VP2.Models;

namespace Library_CourseProject_Anikin_24VP2.Forms
{
    public partial class ClientForm : Form
    {
        private Client currentClient;

        public string ClientName
        {
            get => NameTextBox.Text.Trim();
        }

        public string ClientPhone
        {
            get => PhoneTextBox.Text.Trim();
        }

        public string ClientAddress
        {
            get => AddressTextBox.Text.Trim();
        }

        public ClientForm()
        {
            InitializeComponent();
        }

        public ClientForm(Client client)
        {
            InitializeComponent();

            this.Text = "Редактирование данных читателя";
            SaveBtn.Text = "Изменить";

            currentClient = client;
            FillFields();
        }

        /// <summary>
        /// Заполняет поля данными текущего клиента
        /// </summary>
        private void FillFields()
        {
            NameTextBox.Text = currentClient.FullName;
            PhoneTextBox.Text = currentClient.Phone;
            AddressTextBox.Text = currentClient.Address;
        }

        /// <summary>
        /// Проверяет корректность данных клиента
        /// </summary>
        private bool ValidateClient()
        {
            if (string.IsNullOrWhiteSpace(ClientName))
            {
                MessageBox.Show("Введите полное ФИО.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                NameTextBox.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(ClientPhone))
            {
                MessageBox.Show("Введите телефон.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                PhoneTextBox.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(ClientAddress))
            {
                MessageBox.Show("Введите адрес.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                AddressTextBox.Focus();
                return false;
            }

            return true;
        }

        /// <summary>
        /// Обработчик ввода телефона
        /// </summary>
        private void ClientPhoneTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (char.IsControl(ch))
            {
                return;
            }

            const string allowed = "0123456789+-() ";

            if (!allowed.Contains(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Обработка нажатия кнопки Создать
        /// </summary>
        private void SaveBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateClient())
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
