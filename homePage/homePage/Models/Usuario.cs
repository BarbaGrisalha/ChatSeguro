using MySql.Data.MySqlClient;
using System;
using System.Security.Cryptography;
using System.Text;

namespace Auth.Models
{
    public class Usuario
    {
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Salt { get; set; }

        // Método para criar um novo usuário com hash e salt
        public static void RegisterUser(string username, string password)
        {
            // Gerar salt
            byte[] saltBytes = new byte[32];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(saltBytes); // Gera um salt aleatório
            }
            string salt = BitConverter.ToString(saltBytes).Replace("-", ""); // Converte o salt para string

            // Gerar hash da senha concatenada com o salt
            string passwordHash = GenerateHash(password, salt);

            // Conectar ao banco de dados e inserir o usuário
            string connectionDB = "server=localhost;user=root;database=ChatSeguroDB;port=3306;password=yourpassword";
            using (var connection = new MySqlConnection(connectionDB))
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
            string connectionString = "server=localhost;user=root;database=ChatSeguroDB;port=3306;password=yourpassword";
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
                            string computedHash = GenerateHash(password, storedSalt);
                            // Compara o hash gerado com o hash armazenado
                            return storedHash == computedHash;
                        }
                    }
                }
            }
            return false; // Retorna falso se o usuário não for encontrado ou a senha estiver incorreta
        }

        // Método para gerar hash da senha concatenada com o salt
        public static string GenerateHash(string password, string salt)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(password + salt);
                var hash = sha256.ComputeHash(bytes);
                return BitConverter.ToString(hash).Replace("-", ""); // Converte o hash para string
            }
        }
    }
}
