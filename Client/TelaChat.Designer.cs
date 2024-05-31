namespace Client
{
    partial class TelaChat
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1_Message = new System.Windows.Forms.TextBox();
            this.buttonSend = new System.Windows.Forms.Button();
            this.buttonSair = new System.Windows.Forms.Button();
            this.textBoxSendMessenger = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(106, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(302, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Texto a enviar para o Servidor";
            // 
            // textBox1_Message
            // 
            this.textBox1_Message.Location = new System.Drawing.Point(111, 264);
            this.textBox1_Message.Multiline = true;
            this.textBox1_Message.Name = "textBox1_Message";
            this.textBox1_Message.Size = new System.Drawing.Size(508, 258);
            this.textBox1_Message.TabIndex = 4;
            // 
            // buttonSend
            // 
            this.buttonSend.Location = new System.Drawing.Point(649, 145);
            this.buttonSend.Name = "buttonSend";
            this.buttonSend.Size = new System.Drawing.Size(114, 57);
            this.buttonSend.TabIndex = 2;
            this.buttonSend.Text = "Enviar";
            this.buttonSend.UseVisualStyleBackColor = true;
           // this.buttonSend.Click += new System.EventHandler(this.buttonSend_Click);
            // 
            // buttonSair
            // 
            this.buttonSair.Location = new System.Drawing.Point(649, 469);
            this.buttonSair.Name = "buttonSair";
            this.buttonSair.Size = new System.Drawing.Size(114, 53);
            this.buttonSair.TabIndex = 3;
            this.buttonSair.Text = "Sair";
            this.buttonSair.UseVisualStyleBackColor = true;
            this.buttonSair.Click += new System.EventHandler(this.buttonSair_Click);
            // 
            // textBoxSendMessenger
            // 
            this.textBoxSendMessenger.Location = new System.Drawing.Point(111, 119);
            this.textBoxSendMessenger.Multiline = true;
            this.textBoxSendMessenger.Name = "textBoxSendMessenger";
            this.textBoxSendMessenger.Size = new System.Drawing.Size(508, 83);
            this.textBoxSendMessenger.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(106, 221);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(214, 25);
            this.label2.TabIndex = 5;
            this.label2.Text = "Histórico Mensagens";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // TelaChat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(851, 534);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxSendMessenger);
            this.Controls.Add(this.buttonSair);
            this.Controls.Add(this.buttonSend);
            this.Controls.Add(this.textBox1_Message);
            this.Controls.Add(this.label1);
            this.Name = "TelaChat";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.TelaChat_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1_Message;
        private System.Windows.Forms.Button buttonSend;
        private System.Windows.Forms.Button buttonSair;
        private System.Windows.Forms.TextBox textBoxSendMessenger;
        private System.Windows.Forms.Label label2;
    }
}

