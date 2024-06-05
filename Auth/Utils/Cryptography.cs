using System;
using System.Security.Cryptography;
using System.Text;

namespace ChatSeguro.Auth.Utils
{
    public static class Cryptography
    {
        // Método para gerar o hash da senha com salt
        public static string GenerateHash(string password, string salt)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(password + salt); // Concatena a senha com o salt
                var hash = sha256.ComputeHash(bytes); // Gera o hash
                return BitConverter.ToString(hash).Replace("-", ""); // Converte o hash para string
            }
        }

        // Método para gerar um salt
        public static string GenerateSalt()
        {
            byte[] saltBytes = new byte[32]; // Cria um array de bytes para o salt
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(saltBytes); // Gera bytes aleatórios para o salt
            }
            return BitConverter.ToString(saltBytes).Replace("-", ""); // Converte o salt para string
        }
    }
}
