namespace Library_CourseProject_Anikin_24VP2.Forms
{
    public partial class WelcomeForm : Form
    {
        private System.Windows.Forms.Timer timer;

        private void Timer_Tick(object? sender, EventArgs e)
        {
            timer.Stop();
            Close();
        }

        public WelcomeForm()
        {
            InitializeComponent();
        }

        public WelcomeForm(bool closeByTimer)
        {
            InitializeComponent();

            if (closeByTimer)
            {
                timer = new System.Windows.Forms.Timer { Interval = 10000 };
                timer.Tick += Timer_Tick;
                timer.Start();
            }
        }

        /// <summary>
        /// Обработка нажатия кнопки Начать работу
        /// </summary>
        private void StartBtn_Click(object sender, EventArgs e)
        {
            try
            {
                timer?.Stop();
                timer?.Dispose();
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
