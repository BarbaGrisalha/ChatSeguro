using MySql.Data.MySqlClient;
using System;
using System.Security.Cryptography;
using System.Text;
using Auth.Utils;

namespace Auth.Models
{
    public class Usuario
    {
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Salt { get; set; }

        public static string connectionString = DatabaseConnection.ConnectionString;


        // Método para criar um novo usuário com hash e salt
        public static void RegisterUser(string username, string password)
        {


            // Gerar salt

            string salt = Cryptography.GenerateSalt();

            // Gerar hash da senha concatenada com o salt
            string passwordHash = Cryptography.GenerateHash(password, salt);

            // Conectar ao banco de dados e inserir o usuário
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open(); // Abre a conexão com o banco de dados
                string query = "INSERT INTO usuarios (username, password_hash, salt) VALUES (@username, @password_hash, @salt)";
                using (var cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password_hash", passwordHash);
                    cmd.Parameters.AddWithValue("@salt", salt);
                    cmd.ExecuteNonQuery(); // Executa o comando de inserção
                }
            }
        }

        // Método para validar as credenciais do usuário
        public static bool ValidateUser(string username, string password)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open(); // Abre a conexão com o banco de dados
                string query = "SELECT password_hash, salt FROM usuarios WHERE username = @username";
                using (var cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Recupera o hash e salt armazenados para o usuário
                            string storedHash = reader.GetString("password_hash");
                            string storedSalt = reader.GetString("salt");
                            // Gera o hash da senha fornecida usando o salt armazenado
                            string computedHash = Cryptography.GenerateHash(password, storedSalt);
                            // Compara o hash gerado com o hash armazenado
                            return storedHash == computedHash;
                        }
                    }
                }
            }
            return false; // Retorna falso se o usuário não for encontrado ou a senha estiver incorreta
        }
        public static class DatabaseConnection
        {
           public static string ConnectionString { get; } = "server=192.168.1.170;user=root;database=ChatSeguroDB;port=3306;password=zezinho";
        }
    }
}
