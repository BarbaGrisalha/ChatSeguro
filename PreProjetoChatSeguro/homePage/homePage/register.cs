using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace homePage
{
    public partial class register : Form
    {
        
        public register()
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

            else if(PasswordValidator.ComparePasswords(password,password2))
            {
                labelConPassword.Text = "";

                labelConPassword.Text = "Passwords doest not match, Please Re-enter";
                textPassword.Text = "";
                textConPassword.Text = "";
                textPassword.Focus();

                labelConPassword.Visible = true;
            }
            else
            {
                login.registeredUsers.Add(username, password);
                MessageBox.Show("Usuário criado com sucesso");
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
            new login().Show();
            this.Hide();
        }

        private void checkBoxShowPas_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxShowPas.Checked)
            {
                textPassword.PasswordChar = '\0';
            }
            else
            {
                textPassword.PasswordChar = '●';
                textConPassword.PasswordChar = '●';
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            //Limpa os campos e foca no campo do username
            textUsername.Text = "";
            textPassword.Text = "";
            textConPassword.Text = "";
            textUsername.Focus();
        }
    }
}
