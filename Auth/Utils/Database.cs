using System;
using MySql.Data.MySqlClient;

namespace Auth.Utils
{
    public static class Database
    {
        // Método para criar a tabela de usuários no banco de dados
        public static void InitializeDatabase(string connectionString)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                // Criação da tabela de usuários
                string createTableQuery = @"
                    CREATE TABLE IF NOT EXISTS Users (
                        Id INT AUTO_INCREMENT PRIMARY KEY,
                        Username VARCHAR(255) NOT NULL,
                        Password VARCHAR(255) NOT NULL,
                        Salt VARCHAR(255) NOT NULL
                    );";

                using (var command = new MySqlCommand(createTableQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
