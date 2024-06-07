using System;
using System.Windows.Forms;
using Auth.Controllers;
using Auth;
using Client;


namespace ChatSeguro.Auth
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            string username = textUsername.Text;
            string password = textPassword.Text;

            // Chama o método Authenticate no AuthController para autenticar o usuário
            if (AuthController.Authenticate(username, password))
            {
                // Autenticação bem-sucedida, abre a tela principal do cliente
                var form = new TelaChat();
                form.Show();
                this.Hide();
            }
            else
            {
                // Usuário não está registrado ou a senha está incorreta
                MessageBox.Show("Invalid Username or Password");
                textUsername.Text = "";
                textPassword.Text = "";
                textUsername.Focus();
            }
        }

        private void labelRegister_Click(object sender, EventArgs e)
        {
            // Abre a janela de registro
            new RegisterForm().Show();
            this.Hide();
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            // Limpa os campos e foca no campo do username
            textUsername.Text = "";
            textPassword.Text = "";
            textUsername.Focus();
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
            }
        }
    }
}
