using Library_CourseProject_Anikin_24VP2.Models;
using Microsoft.Data.Sqlite;

namespace Library_CourseProject_Anikin_24VP2.Services
{
    public class ClientService
    {
        /// <summary>
        /// Сервис работы с БД
        /// </summary>
        private readonly DatabaseService database;

        public ClientService(DatabaseService database)
        {
            this.database = database;
        }

        /// <summary>
        /// Добавляет клиента
        /// </summary>
        /// <param name="client">Добавляемый клиента</param>
        public void Create(Client client)
        {
            using var connection = database.CreateConnection();
            connection.Open();

            string sql = @"
                INSERT INTO clients (full_name, phone, address, registration_date, is_debtor)
                VALUES ( @full_name, @phone, @address, @registration_date, @is_debtor);
                ";

            using var command = new SqliteCommand(sql, connection);
            command.Parameters.AddWithValue("@full_name", client.FullName);
            command.Parameters.AddWithValue("@phone", client.Phone);
            command.Parameters.AddWithValue("@address", client.Address);
            command.Parameters.AddWithValue("@registration_date", client.RegistrationDate.ToString("yyyy-MM-dd"));
            command.Parameters.AddWithValue("@is_debtor", client.IsDebtor ? 1 : 0);

            command.ExecuteNonQuery();
        }

        /// <summary>
        /// Возвращает список клиентов
        /// </summary>
        /// <returns>Список клиентов</returns>
        public List<Client> ReadAll()
        {
            List<Client> clients = new List<Client>();

            using var connection = database.CreateConnection();
            connection.Open();

            string sql = "SELECT * FROM clients";

            using var command = new SqliteCommand(sql, connection);

            using SqliteDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Client client = new Client
                    (
                        id: reader.GetInt32(0),
                        fullName: reader.GetString(1),
                        phone: reader.GetString(2),
                        address: reader.GetString(3),
                        registrationDate: DateTime.Parse(reader.GetString(4)),
                        isDebtor: reader.GetInt32(5) == 1
                    );

                    clients.Add(client);
                }
            }

            return clients;
        }


        /// <summary>
        /// Возвращает клиента по Id
        /// </summary>
        /// <param name="id">Id клиента</param>
        /// <returns>Модель клиента или null</returns>
        public Client? Read(int id)
        {
            using var connection = database.CreateConnection();
            connection.Open();

            string sql = "SELECT * FROM clients WHERE id = @id";

            using var command = new SqliteCommand(sql, connection);
            command.Parameters.AddWithValue("@id", id);

            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                return new Client
                (
                    id: reader.GetInt32(0),
                    fullName: reader.GetString(1),
                    phone: reader.GetString(2),
                    address: reader.GetString(3),
                    registrationDate: DateTime.Parse(reader.GetString(4)),
                    isDebtor: reader.GetInt32(5) == 1
                );
            }

            return null;
        }

        /// <summary>
        /// Обновляет клиента
        /// </summary>
        /// <param name="client">Обновленный клиент</param>
        public void Update(Client client)
        {
            using var connection = database.CreateConnection();
            connection.Open();

            string sql = @"
                UPDATE clients
                SET
                    full_name = @full_name,
                    phone = @phone, 
                    address = @address, 
                    registration_date = @registration_date,
                    is_debtor = @is_debtor
                WHERE id = @id;
            ";

            using var command = new SqliteCommand(sql, connection);
            command.Parameters.AddWithValue("@id", client.Id);
            command.Parameters.AddWithValue("@full_name", client.FullName);
            command.Parameters.AddWithValue("@phone", client.Phone);
            command.Parameters.AddWithValue("@address", client.Address);
            command.Parameters.AddWithValue("@registration_date", client.RegistrationDate.ToString("yyyy-MM-dd"));
            command.Parameters.AddWithValue("@is_debtor", client.IsDebtor ? 1 : 0);
            command.ExecuteNonQuery();
        }

        /// <summary>
        /// Удаляет клиента
        /// </summary>
        /// <param name="id">Id удаляемого клиента</param>
        public void Delete(int id)
        {
            using var connection = database.CreateConnection();
            connection.Open();

            string sql = "DELETE FROM clients WHERE id = @id";

            using var command = new SqliteCommand(sql, connection);
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();
        }
    }
}
