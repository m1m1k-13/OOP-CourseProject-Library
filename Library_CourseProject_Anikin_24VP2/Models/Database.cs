namespace Library_CourseProject_Anikin_24VP2.Models
{
    /// <summary>
    /// Информация о БД
    /// </summary>
    public struct Database
    {
        /// <summary>
        /// Имя БД
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Активна ли БД
        /// </summary>
        public bool IsActive { get; set; }
    }
}
