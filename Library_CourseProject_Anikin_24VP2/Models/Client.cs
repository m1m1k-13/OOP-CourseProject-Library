namespace Library_CourseProject_Anikin_24VP2.Models
{
    /// <summary>
    /// Модель клиента библиотеки
    /// </summary>
    public class Client
    {
        /// <summary>
        /// Идентификатор клиента
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// ФИО клиента
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Номер телефона клиента
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Электронная почта клиента
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Дата регистрации клиента
        /// </summary>
        public DateTime RegistrationDate { get; set; }

        /// <summary>
        /// Является ли клиент должником
        /// </summary>
        public bool IsDebtor { get; set; }

        public Client() {}

        public Client(int id, string fullName, string phone, string address, DateTime registrationDate, bool isDebtor)
        {
            Id = id;
            FullName = fullName;
            Phone = phone;
            Address = address;
            RegistrationDate = registrationDate;
            IsDebtor = isDebtor;
        }

        /// <summary>
        /// Возвращает строковое представление клиента
        /// </summary>
        public override string ToString()
        {
            return FullName;
        }
    }
}