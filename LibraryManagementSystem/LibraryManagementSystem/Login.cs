using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LibraryManagementSystem
{
    public partial class Login : Form
    {        
        public Login()
        {
            InitializeComponent();

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            //If user name and password are correct enable menustrip
            if ((txtUsername.Text == "admin") && (txtPassword.Text == "1234"))
            {              
                Main.menu.bookIssuingToolStripMenuItem.Visible = true;
                Main.menu.categoryToolStripMenuItem.Visible = true;
                Main.menu.paymentToolStripMenuItem.Visible = true;
                Main.menu.booksToolStripMenuItem.Visible = true;
                Main.menu.memberToolStripMenuItem1.Visible = true;
                Main.menu.logoutToolStripMenuItem.Visible = true;
               
                this.Close();               
            }
            else
            {             
               
                MessageBox.Show("Wrong Username or Password!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {
            //Exit From Main Form
            Main.menu.Close();
           
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
