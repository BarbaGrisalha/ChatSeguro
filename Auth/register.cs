using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Auth.Controllers;
using Auth;
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

            if (errors.Any())
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
                textPassword.Text = "";
                textConPassword.Text = "";
                textPassword.Focus();
                labelConPassword.Visible = true;
            }
            else
            {
                // Chama o método Register no AuthController para registrar o usuário
                AuthController.Register(username, password);
                MessageBox.Show("Usuário criado com sucesso");

                textUsername.Text = "";
                textPassword.Text = "";
                textConPassword.Text = "";
                textUsername.Focus();
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

        private void buttonClear_Click(object sender, EventArgs e)
        {
            // Limpa os campos e foca no campo do username
            textUsername.Text = "";
            textPassword.Text = "";
            textConPassword.Text = "";
            textUsername.Focus();
        }

        
    }
}
