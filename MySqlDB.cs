using MySql.Data.MySqlClient;
using System;

namespace YourNamespace
{
    class UniqueMySqlDB
    {
        private readonly MySqlConnection connection;

        public UniqueMySqlDB(string connectionString)
        {
            connection = new MySqlConnection(connectionString);
        }

        public void OpenConnection()
        {
            try
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                {
                    connection.Open();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while opening the connection: " + ex.Message);
            }
        }

        public void CloseConnection()
        {
            try
            {
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while closing the connection: " + ex.Message);
            }
        }

        public MySqlConnection GetConnection()
        {
            return connection;
        }
    }
}
