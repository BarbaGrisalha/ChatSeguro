using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace homePage
{
    internal class PasswordValidator
    {
        public static List<string> ValidatePassword(string password)
        {
            List<string> errors = new List<string>();

            // Comprimento mínimo
            if (password.Length < 8)
            {
                errors.Add("A senha deve ter no mínimo 8 caracteres.");
            }

            // Complexidade
            bool hasUpperCase = password.Any(char.IsUpper);
            bool hasLowerCase = password.Any(char.IsLower);
            bool hasDigit = password.Any(char.IsDigit);
            bool hasSpecialChar = password.Any(c => !char.IsLetterOrDigit(c));

            if (!(hasUpperCase && hasLowerCase))
            {
                errors.Add("A senha deve conter pelo menos: Uma letra maiúscula e uma letra minúscula");
            }

            if (!(hasDigit))
            {
                errors.Add("A senha deve conter pelo menos: Um número");
            }

            if (!(hasSpecialChar))
            {
                errors.Add("A senha deve conter pelo menos: Um caractere especial");
            }

            // Verificação contra listas de senhas comuns (opcional)
            string[] commonPasswords = { "password", "123456", "qwerty", "abc123" };
            if (commonPasswords.Contains(password.ToLower()))
            {
                errors.Add("A senha não pode ser uma senha comum.");
            }

            // Não permitir sequências óbvias (opcional)
            if (IsCommonSequence(password))
            {
                errors.Add("A senha não pode conter sequências óbvias.");
            }

            return errors;
        }

        public static bool ComparePasswords(string password, string password2)
        {
            if(password != password2)
            {
                return true;
            }
            return false;
        }

        private static bool IsCommonSequence(string password)
        {
            string[] commonSequences = { "123", "abc", "qwe", "password" };
            foreach (string sequence in commonSequences)
            {
                if (password.ToLower().Contains(sequence))
                {
                    return true;
                }
            }
            return false;
        }

    }
}
