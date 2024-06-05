using Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace homePage
{/// <summary>
/// Definimos aqui a tela de login ou inicial
/// mudamos para um login independente, ou seja , uma tela de login 
/// que foi incorporada ao programa.
/// </summary>
    public partial class login : Form
    {
        // Armazenar usuários registrados.
        // por enquanto sem banco de dados e criptografia(hash e salt)
        public static Dictionary<string, string> registeredUsers = new Dictionary<string, string>();
        public login()
        {
            InitializeComponent();
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
             string username = textUsername.Text;
             string password = textPassword.Text;
            
            
            if (registeredUsers.ContainsKey(username) && registeredUsers[username] == password)
            {
                // Usuário está registrado e a senha está correta
                // new Form1().Show();
                // this.Hide();
                this.DialogResult = DialogResult.OK;
                this.Close();
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
            // Abre a janela de registo
            new register().Show();
            this.Hide();
        }


        private void buttonClear_Click(object sender, EventArgs e)
        {
            //Limpa os campos e foca no campo do username
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

        private void login_Load(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
