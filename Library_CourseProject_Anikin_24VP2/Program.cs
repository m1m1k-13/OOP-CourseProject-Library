namespace Library_CourseProject_Anikin_24VP2
{
    internal static class Program
    {
        /// <summary>
        ///  Главная точка входа приложения
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new MainForm());
        }
    }
}