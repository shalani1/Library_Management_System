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
    public partial class Main : Form
    {
        public static Main menu;
              
        public Main()
        {
          
            InitializeComponent();
            
        }

        public void HideMenuStrip()
        {          
            //Disable menustrip before logging
            this.bookIssuingToolStripMenuItem.Visible = false;
            this.categoryToolStripMenuItem.Visible = false;
            this.paymentToolStripMenuItem.Visible = false;
            this.booksToolStripMenuItem.Visible = false;
            this.memberToolStripMenuItem1.Visible = false;
            this.logoutToolStripMenuItem.Visible = false;

            menu = this;
        }
       
        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (Form Login in this.MdiChildren)
            {
                Login.Visible = false;
                Login.Dispose();
            }
           
            Login log = new Login();
            log.MdiParent = this;
            log.Dock = DockStyle.Fill;
            log.Show();

            HideMenuStrip();         
        }    

        private void categoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form Category in this.MdiChildren)
            {
                Category.Visible = false;
                Category.Dispose();
            }

            Category cat = new Category();
            cat.MdiParent = this;
            cat.Dock = DockStyle.Fill;
            cat.Show();
        }     
       
        private void booksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form Books in this.MdiChildren)
            {
                Books.Visible = false;
                Books.Dispose();
            }

            Books book = new Books();
            book.MdiParent = this;
            book.Dock = DockStyle.Fill;
            book.Show();
        }     

        private void bookIssuingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form  BookIssuing in this.MdiChildren)
            {
                BookIssuing.Visible = false;
                BookIssuing.Dispose();
            }

            BookIssuing bookissue = new BookIssuing();
            bookissue.MdiParent = this;
            bookissue.Dock = DockStyle.Fill;
            bookissue.Show();
        }

        
        private void PaymentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form Payment in this.MdiChildren)
            {
                Payment.Visible = false;
                Payment.Dispose();
            }

            Payments payment = new Payments();
            payment.MdiParent = this;
            payment.Dock = DockStyle.Fill;
            payment.Show();
        }

        private void memberToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form Member in this.MdiChildren)
            {
                Member.Visible = false;
                Member.Dispose();
            }

            Member member = new Member();
            member.MdiParent = this;
            member.Dock = DockStyle.Fill;
            member.Show();
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
