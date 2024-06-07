using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Auth.Controllers;
using Auth.Utils;
using ChatSeguro.Auth;

namespace Auth
{
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
        }

        private void buttonRegister_Click(object sender, EventArgs e)
        {
            string username = textUsername.Text;
            string password = textPassword.Text;
            string password2 = textConPassword.Text;

            List<string> errors = PasswordValidator.ValidatePassword(password);
            labelWarning.Text = "";

            // Verifica se o username já existe no banco de dados
            if (AuthController.UsernameExists(username))
            {
                labelUserExists.Text = "User already exists";
                labelUserExists.Visible = true;
                ClearFields();
            }

            else if (errors.Any())
            {
                foreach (string erro in errors)
                {
                    labelWarning.Text += $"\n{erro}";
                }
                labelWarning.Visible = true;
            }
            else if (PasswordValidator.ComparePasswords(password, password2))
            {
                labelConPassword.Text = "";
                labelConPassword.Text = "Passwords do not match, Please Re-enter";
                ClearFields();
                labelConPassword.Visible = true;
            }
            else
            {
                // Chama o método Register no AuthController para registrar o usuário
                AuthController.Register(username, password);
                MessageBox.Show("Usuário criado com sucesso");

                ClearFields();
            }
        }

        private void textConPassword_Enter(object sender, EventArgs e)
        {
            string username = textUsername.Text;
            string password = textPassword.Text;

            List<string> errors = PasswordValidator.ValidatePassword(password);

            if (errors.Any())
            {
                labelWarning.Text = "";
                foreach (string erro in errors)
                {
                    labelWarning.Text += $"\n{erro}";
                }
                labelWarning.Visible = true;
            }
        }

        private void labelLogin_Click(object sender, EventArgs e)
        {
            // Abre a janela de login
            new LoginForm().Show();
            this.Hide();
        }

        private void checkBoxShowPas_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxShowPas.Checked)
            {
                textPassword.PasswordChar = '\0';
                textConPassword.PasswordChar = '\0';
            }
            else
            {
                textPassword.PasswordChar = '●';
                textConPassword.PasswordChar = '●';
            }
        }
        
        private void ClearFields()
        {
            // Limpa os campos e foca no campo do username
            textUsername.Text = "";
            textPassword.Text = "";
            textConPassword.Text = "";
            textUsername.Focus();
        }

    }
}
