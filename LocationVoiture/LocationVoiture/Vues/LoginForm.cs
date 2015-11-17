using LocationVoiture.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LocationVoiture.Vues
{
    public partial class LoginForm : Form
    {

        public String username { get; private set; }
        private LoginServices loginServices { get; set; }

        public LoginForm()
        {
            InitializeComponent();
            loginServices = new LoginServices();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String username = txtLogin_username.Text;
            String password = txtLogin_password.Text;

            //if(loginServices.loginAccepted(username, password))
            //{
                username = txtLogin_username.Text;
                this.Close();
            //}
            //else
            //{
            //    MessageBox.Show("Connection refusée");
            //}
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
