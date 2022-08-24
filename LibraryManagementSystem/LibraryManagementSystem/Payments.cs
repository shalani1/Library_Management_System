using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace LibraryManagementSystem
{
    public partial class Payments : Form
    {
        SqlConnection cs = new SqlConnection(Properties.Settings.Default.Connection);
        SqlDataAdapter da = new SqlDataAdapter();
        DataSet ds = new DataSet();

        //int sum = 0;
        double sum = 0;
        int StudentPaymentActive;
        int StaffPaymentActive;
        string total;
        string xy;
        int delayChargeTotal;
        string y;
        string memberID;
        string memberIDCell;
        bool totalClicked;
        bool totalChanged;
        

        public Payments()
        {
            InitializeComponent();
        }

      
      
        private void btnPay_Click(object sender, EventArgs e)
        {
            if (txtNo.Text == "")
            {
                MessageBox.Show("Please select details!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (totalClicked == false)
                {
                   
                    MessageBox.Show("Please click on total field to see Total amount has to pay!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    da.UpdateCommand = new SqlCommand("UPDATE BooksIssuing SET PaymentStatus=1  WHERE IssuingStatus=1 AND MemberID='" + memberIDCell + "'", cs);
                    cs.Open();
                    da.UpdateCommand.ExecuteNonQuery();
                    MessageBox.Show("Payment Successful!", "Successed", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    UnpaidPayments();
                    foreach (Control c in Controls)    //Clear all text fields
                    {
                        if (c is TextBox)
                        {
                            c.Text = "";
                        }
                    }
                    cs.Close();
                }
            }
        }

         private void StudentPayment_Load(object sender, EventArgs e)
        {
            btnDelete.Hide();
            UnpaidPayments();
            this.dataGridView7.Columns[0].Visible = false;
            this.dataGridView7.Columns[5].Visible = false;
        }
      
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView7_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                txtNo.Text = dataGridView7.CurrentRow.Cells[1].Value.ToString();
            }
            catch(NullReferenceException)
            {
                MessageBox.Show("There is no details to diaplay!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            txtTotal.Clear();
        }

        private void btnAllPayments_Click(object sender, EventArgs e)
        {
            paidPayments();
            btnDelete.Show();
            btnPay.Hide();
            txtTotal.Hide();
            label2.Hide();
            txtNo.ResetText();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string Query = "";
            if (txtSearch.Text != string.Empty)
                Query += "[Registration No] like '%" + txtSearch.Text.Trim() + "%' and ";
            Query += "1 = 1";
            (dataGridView7.DataSource as DataTable).DefaultView.RowFilter = Query;

            dataGridViewRowCount();
        }

        private void btnUnpaid_Click(object sender, EventArgs e)
        {
            UnpaidPayments();
            btnPay.Enabled = true;
            btnDelete.Hide();
            btnPay.Show();
            label2.Show();
            txtTotal.Show();
            txtNo.ResetText();
        }

        public void UnpaidPayments()    //display details of unpaid payments
        {
            da.SelectCommand = new SqlCommand("SELECT B.IssueID as [IssueID],M.RegistrationNo as [Registration No],B.IssuedDate as [Issued Date],B.ReturnedDate as [Returned Date],B.DelayCharge as [Delay Charge],M.MemberID FROM BooksIssuing B JOIN MEMBER M ON(B.MemberID=M.MemberID) WHERE B.IssuingStatus=1 AND B.PaymentStatus=0", cs);
            DataTable ds = new DataTable();
            da.Fill(ds);
            dataGridView7.DataSource = ds;

            dataGridViewRowCount();
        }

        private void btnDelete_Click_1(object sender, EventArgs e)
        {
            if(txtNo.Text.Trim()=="")
            {
                MessageBox.Show("Please select details", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {

            DialogResult dr;
            dr = MessageBox.Show("Are you sure? \nThere is no undo after deleting!", "Confirm deletion.", MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                da.DeleteCommand = new SqlCommand("DELETE FROM BooksIssuing WHERE IssueID=@issueid", cs);
                da.DeleteCommand.Parameters.Add("@issueid", SqlDbType.VarChar).Value = dataGridView7.CurrentRow.Cells[0].Value.ToString();
                cs.Open();

                da.DeleteCommand.ExecuteNonQuery();

                MessageBox.Show("Permanently Deleted!","Successed",MessageBoxButtons.OK,MessageBoxIcon.Information);
                paidPayments();
                cs.Close();
            }
            else
            {
                MessageBox.Show("Deletion Cancelled!","Alert",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
            }
        }
        public void paidPayments()      //display details of paid payments
        {
            da.SelectCommand = new SqlCommand("SELECT B.IssueID as [IssueID],M.RegistrationNo as [Registration No],B.IssuedDate as [Issued Date],B.ReturnedDate as [Returned Date],B.DelayCharge as [Delay Charge] FROM BooksIssuing B JOIN MEMBER M ON(B.MemberID=M.MemberID) WHERE B.IssuingStatus=1 AND B.PaymentStatus=1", cs);
            DataTable ds = new DataTable();
            da.Fill(ds);
            dataGridView7.DataSource = ds;

            dataGridViewRowCount();
        }

        public void dataGridViewRowCount()  //count rows of the table
        {
            int rowcount = dataGridView7.Rows.Count;
            lblCount.Text = rowcount.ToString();
        }       
       
        public void TotalCalculation()  //calculate the total charge that pay for the delay book returning for relavent member
        {

            memberIDCell = this.dataGridView7.CurrentRow.Cells[5].Value.ToString();
            //{
            string memberid = memberIDCell;
            da.SelectCommand = new SqlCommand("SELECT DelayCharge FROM BooksIssuing WHERE PaymentStatus=0 AND MemberID=@memberid", cs);
            da.SelectCommand.Parameters.Add("@memberid", SqlDbType.VarChar).Value = memberIDCell;

            cs.Open();

            SqlDataReader dy = da.SelectCommand.ExecuteReader();
            while (dy.Read())
            {

                string delayCharge = dy.GetValue(0).ToString();
                double dblCharge = Convert.ToDouble(delayCharge);

                sum = sum + dblCharge;
            }
            txtTotal.Text = sum.ToString()+".00";
          
            cs.Close();
            
        }

        private void txtTotal_Click(object sender, EventArgs e)
        {
            totalClicked = true;

            if (txtTotal.Text == "")
            {
                TotalCalculation();

            }
        }

       
    }
}
