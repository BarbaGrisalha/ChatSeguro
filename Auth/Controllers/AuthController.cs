using System;
using System.Data;
using MySql.Data.MySqlClient;
using Auth.Utils;
using Auth.Models;

namespace Auth.Controllers
{
    public static class AuthController
    {
        // Método para registrar um novo usuário
        public static void Register(string username, string password)
        {
            // Gera um salt aleatório
            string salt = Cryptography.GenerateSalt();
            // Gera o hash da senha concatenada com o salt
            string passwordHash = Cryptography.GenerateHash(password, salt);

            // Conexão com o banco de dados
            using (var connection = new MySqlConnection(Usuario.connectionString))
            {
                connection.Open(); // Abre a conexão com o banco de dados
                string query = "INSERT INTO usuarios (username, password_hash, salt) VALUES (@username, @password_hash, @salt)";
                using (var cmd = new MySqlCommand(query, connection))
                {
                    // Adiciona os parâmetros ao comando SQL
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password_hash", passwordHash);
                    cmd.Parameters.AddWithValue("@salt", salt);
                    cmd.ExecuteNonQuery(); // Executa o comando de inserção
                }
            }
        }

        // Método para verificar se um username já existe no banco de dados
        public static bool UsernameExists(string username)
        {
            // Conexão com o banco de dados
            using (var connection = new MySqlConnection(Usuario.connectionString))
            {
                connection.Open(); // Abre a conexão com o banco de dados
                string query = "SELECT COUNT(*) FROM usuarios WHERE username = @username";
                using (var cmd = new MySqlCommand(query, connection))
                {
                    // Adiciona os parâmetros ao comando SQL
                    cmd.Parameters.AddWithValue("@username", username);
                    return Convert.ToInt32(cmd.ExecuteScalar()) > 0; // Retorna verdadeiro se o count for maior que 0
                }
            }
        }

        // Método para autenticar um usuário
        public static bool Authenticate(string username, string password)
        {
            // Conexão com o banco de dados

            using (var connection = new MySqlConnection(Usuario.connectionString))
            {
                connection.Open(); // Abre a conexão com o banco de dados
                string query = "SELECT password_hash, salt FROM usuarios WHERE username = @username";
                using (var cmd = new MySqlCommand(query, connection))
                {
                    // Adiciona os parâmetros ao comando SQL
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

    }
}
