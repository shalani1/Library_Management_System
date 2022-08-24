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
    public partial class BookIssuing : Form
    {
        SqlConnection cs = new SqlConnection(Properties.Settings.Default.Connection);
        SqlDataAdapter da = new SqlDataAdapter();
        DataSet ds = new DataSet();
        string lendCount;
        bool clickTitleStatus;
        bool clickNameStatus;
        bool dateChanged;
        string lendBookCount1;
        string lendBookCount2;
        string strFName;
        string studentcount;
        string book;
        string fname;
        string lname;
        string student;
        string fullname;
        string title;
        int memberID;
        int bookID;
        DateTime returnDate;
        int positionID;
        bool dataGridViewActive;
        bool dateReturnChanged;
        int issuedBookCount;
        bool btnAllActive;

        public BookIssuing()
        {
            InitializeComponent();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
           
            GetMemberID();
            GetBookID();
            GetPositonID();
            CheckBookCount();
 
            if ((txtBookCode.Text.Trim() == "") || (txtRegistrationNo.Text.Trim() == "") || (dateChanged == false))
            {
                MessageBox.Show("Please fill all the fields!","Warning",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
            else
            {

                if (positionID == 1)
                {
                    returnDate = dateTimePickerIssuingDate.Value.AddDays(14);
                }
                else if (positionID == 2)
                {
                    returnDate = dateTimePickerIssuingDate.Value.AddDays(21);
                }


                if (positionID == 1 && issuedBookCount == 3)
                {
                    MessageBox.Show("Already lended 3 books.\nCan not issue another books for this member.","Alert",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                }
                else if (positionID == 2 && issuedBookCount == 4)
                {
                    MessageBox.Show("Already lended 4 books.\nCan not issue another books for this member.","Alert",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                }
                else
                {
                   
                    da.InsertCommand = new SqlCommand("INSERT INTO BooksIssuing VALUES(@memberid,@bookid,@issueddate,@returneddate,@issuingstatus,@delaycharge,@paymentstatus)", cs);

                    da.InsertCommand.Parameters.Add("@memberid", SqlDbType.VarChar).Value = memberID;
                    da.InsertCommand.Parameters.Add("@bookid", SqlDbType.VarChar).Value = bookID;
                    da.InsertCommand.Parameters.Add("@issueddate", SqlDbType.Date).Value = dateTimePickerIssuingDate.Value.Date;
                    da.InsertCommand.Parameters.Add("@returneddate", SqlDbType.Date).Value = returnDate;
                    da.InsertCommand.Parameters.Add("@issuingstatus", SqlDbType.VarChar).Value = 0;
                    da.InsertCommand.Parameters.Add("@delaycharge", SqlDbType.Decimal).Value = 0.00;
                    da.InsertCommand.Parameters.Add("@paymentstatus", SqlDbType.VarChar).Value = 0;

                    cs.Open();

                    try
                    {
                        da.InsertCommand.ExecuteNonQuery();
                        MessageBox.Show("Successfuly Inserted!", "Successed", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        FieldsClear();
                        IssuingDetails();
                    }
                    catch(SqlException)
                    {
                        MessageBox.Show("Book or member does not exist!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    finally
                    {

                        cs.Close();
                    }
                }
            }
        }

        private void StudentBookLending_Load(object sender, EventArgs e)
        {
            btnReturn.Hide();
            txtPayment.Hide();
            dateTimePickerReturn.Hide();
            lblPayment.Hide();
            lblReturnDate.Hide();
            txtIssueID.Hide();
            btnGoBack.Hide();
            btnReturnBook.Hide();
            btnUpdate.Hide();
        
            IssuingDetails();
            this.dataGridView5.Columns[0].Visible = false;
            this.dataGridView5.Columns[0].Visible = false;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }

        private void dateTimePickerIssuingDate_ValueChanged(object sender, EventArgs e)
        {
            dateChanged = true;
        }
 
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
           
            string Query = "";
            if (txtSearch.Text != string.Empty)
                Query += "[Registration No] like '%" + txtSearch.Text.Trim() + "%' and ";
            Query += "1 = 1";
            (dataGridView5.DataSource as DataTable).DefaultView.RowFilter = Query;

            int rowcount = dataGridView5.Rows.Count - 1;
            lblCount.Text = rowcount.ToString();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView5_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridViewActive = true;
            btnReturnBook.Show();
            btnUpdate.Show();
            btnInsert.Hide();
            try
            {
            txtIssueID.Text = this.dataGridView5.CurrentRow.Cells[0].Value.ToString();
            txtBookCode.Text = this.dataGridView5.CurrentRow.Cells[2].Value.ToString();
            txtRegistrationNo.Text = this.dataGridView5.CurrentRow.Cells[1].Value.ToString();
            dateTimePickerIssuingDate.Text = this.dataGridView5.CurrentRow.Cells[3].Value.ToString();
            dateTimePickerReturn.Text = this.dataGridView5.CurrentRow.Cells[4].Value.ToString();
            txtPayment.Text = "0.00";
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("There is no details to diaplay!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        public void GetMemberID()   ////get member id that relavent to the user entered member
        {
            string registrationno = txtRegistrationNo.Text;
            da.SelectCommand = new SqlCommand("SELECT MemberID FROM Member WHERE RegistrationNo=@registrationno", cs);
            da.SelectCommand.Parameters.Add("@registrationno", SqlDbType.VarChar).Value = txtRegistrationNo.Text;
            cs.Open();
            SqlDataReader dy = da.SelectCommand.ExecuteReader();
            if (dy.Read())
            {
                string y = dy.GetValue(0).ToString();
                memberID = int.Parse(y);

            }
            cs.Close();
        }

        public void GetBookID()     //get book id that relavent to the user entered book name 
        {
            string bookcode = txtBookCode.Text;
            da.SelectCommand = new SqlCommand("SELECT BookID FROM Book WHERE BookCode=@bookcode", cs);
            da.SelectCommand.Parameters.Add("@bookcode", SqlDbType.VarChar).Value = txtBookCode.Text;
            cs.Open();
            SqlDataReader dy = da.SelectCommand.ExecuteReader();
            if (dy.Read())
            {

                string x = dy.GetValue(0).ToString();
                bookID = int.Parse(x);
            }
            cs.Close();
        }

        public void IssuingDetails()    //display details of issued books and relavent members at this moment
        {
          
            da.SelectCommand = new SqlCommand("SELECT L.IssueID,M.RegistrationNo as [Registration No],B.BookCode as [Book Code],L.IssuedDate as [Issued Date],L.ReturnedDate as [Returned Date],P.Position FROM BooksIssuing L JOIN Member M ON(L.MemberID=M.MemberID) JOIN Book B ON(L.BookID=B.BookID) JOIN Position P ON(M.PositionID=P.PositionID)WHERE L.IssuingStatus=0", cs);
            DataTable ds = new DataTable();
            da.Fill(ds);

            dataGridView5.DataSource = ds;

            dataGridViewRowCount();
        }

        public void FieldsClear()
        {
            dateTimePickerIssuingDate.Value = new DateTime(2021, 01, 01);
            dateChanged = false;

            btnInsert.Enabled = true;

            foreach (Control c in Controls)    //Clear all text fields
            {
                if (c is TextBox)
                {
                    c.Text = "";
                }
            }
        }

        public void GetPositonID()      //get position id that relavent to the user selected position
        {
            string registrationno = txtRegistrationNo.Text;
            da.SelectCommand = new SqlCommand("SELECT PositionID FROM Member WHERE RegistrationNo=@registrationno", cs);
            da.SelectCommand.Parameters.Add("@registrationno", SqlDbType.VarChar).Value = txtRegistrationNo.Text;
            cs.Open();
            SqlDataReader dy = da.SelectCommand.ExecuteReader();
            if (dy.Read())
            {

                string x = dy.GetValue(0).ToString();
                positionID = int.Parse(x);

            }
            cs.Close();
        }       

        private void btnUpdate_Click(object sender, EventArgs e)
        {

            GetMemberID();
            GetBookID();
            GetPositonID();
            if(txtBookCode.Text.Trim()=="" || txtRegistrationNo.Text.Trim()=="")
            {
                MessageBox.Show("Please select details!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (txtBookCode.Text.Trim() == "" && txtRegistrationNo.Text.Trim() == "")
                {
                    MessageBox.Show("Please fill all the fields!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {

            if (positionID == 1)
            {
                returnDate = dateTimePickerIssuingDate.Value.AddDays(14);
            }
            else if (positionID == 2)
            {
                returnDate = dateTimePickerIssuingDate.Value.AddDays(21);
            }

            da.UpdateCommand = new SqlCommand("UPDATE BooksIssuing SET BookID='" + bookID + "',MemberID='" + memberID + "',IssuedDate='" + dateTimePickerIssuingDate.Value + "',ReturnedDate='" + returnDate + "' WHERE IssueID='" + txtIssueID.Text + "'", cs);
            cs.Open();

            da.UpdateCommand.ExecuteNonQuery();

            MessageBox.Show("Details Updated!","Successed",MessageBoxButtons.OK,MessageBoxIcon.Information);
            
            cs.Close();
            FieldsClear();
            IssuingDetails();
                }
        }
        }
     
        private void btnAll_Click(object sender, EventArgs e)
        {
            btnGoBack.Show();

            btnAllActive = true;
            AllReturnedDetails();
            dateTimePickerReturn.Enabled = false;
            txtBookCode.Enabled = false;
            txtRegistrationNo.Enabled = false;
            dateTimePickerIssuingDate.Enabled = false;
            btnReturn.Hide();
            btnReturnBook.Hide();
            btnInsert.Hide();
            btnUpdate.Hide();

        }
    
        public void AllReturnedDetails()    //display data of returned books and relavent members
        {
           
            da.SelectCommand = new SqlCommand("SELECT L.IssueID,M.RegistrationNo as [Registration No],B.BookCode as [Book Code],L.IssuedDate as [Issued Date],L.ReturnedDate as [Returned Date],P.Position FROM BooksIssuing L JOIN Member M ON(L.MemberID=M.MemberID) JOIN Book B ON(L.BookID=B.BookID) JOIN  Position P ON(M.PositionID=P.PositionID) WHERE L.IssuingStatus=1", cs);
            DataTable ds = new DataTable();
            da.Fill(ds);

            dataGridView5.DataSource = ds;
            dataGridViewRowCount();
        }
     
        private void btnReturn_Click(object sender, EventArgs e)
        {

            if (dateReturnChanged == false)
            {
                MessageBox.Show("Please select a date!","Warning",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
            else
            {

                da.UpdateCommand = new SqlCommand("UPDATE BooksIssuing SET ReturnedDate='" + dateTimePickerReturn.Value + "',DelayCharge='" + txtPayment.Text + "',IssuingStatus=1 WHERE IssueID='" + txtIssueID.Text + "'", cs);
                cs.Open();

                da.UpdateCommand.ExecuteNonQuery();

                MessageBox.Show("Details Updated!","Successed",MessageBoxButtons.OK,MessageBoxIcon.Information);
                cs.Close();
                IssuingDetails();
                FieldsClear();
            }
        }

        private void btnReturnBook_Click(object sender, EventArgs e)
        {

            btnReturn.Show();
            txtRegistrationNo.Enabled = false;
            txtBookCode.Enabled = false;
            dateTimePickerIssuingDate.Enabled = false;
            label1.Show();
            label3.Show();
            label5.Show();
            btnInsert.Hide();
            btnUpdate.Hide();
            txtPayment.Show();
            lblPayment.Show();
            lblReturnDate.Show();
            dateTimePickerReturn.Show();
            btnGoBack.Show();


        }

        private void dateTimePickerReturn_ValueChanged(object sender, EventArgs e)
        {
            dateReturnChanged = true;

            GetPositonID();
            if (positionID == 1)
            {
                StudentDelayCharge();
            }
            else if (positionID == 2)
            {
                StaffDelayCharge();
            }

        }

        public void StudentDelayCharge()
        {

            TimeSpan difference = (dateTimePickerReturn.Value) - (dateTimePickerIssuingDate.Value);     //calculation for charging to delay book returning relavent to the student member
            int days = difference.Days;

            if (days <= 14)
            {
                txtPayment.Text = (0.00).ToString();
            }
            else if (days > 14 && days <= 21)
            {
                double x = 5.00 * (days - 14);
                txtPayment.Text = x.ToString();
            }
            else if (days > 21 && days <= 28)
            {
                double y = 5.00 * 7;
                double x = 10.00 * (days - 21);
                double sum = x + y;
                txtPayment.Text = sum.ToString();
            }
            else
            {
                double y = 5.00 * 7;
                double x = 10.00 * 7;
                double z = 20.00 * (days - 28);
                double sum = x + y + z;
                txtPayment.Text = sum.ToString();
            }
        }

        public void StaffDelayCharge()
        {
            
            TimeSpan difference = (dateTimePickerReturn.Value) - (dateTimePickerIssuingDate.Value);     //calculation for charging to delay book returning relavent to the staff member
            int days = difference.Days;

            if (days <= 21)
            {
                txtPayment.Text = (0.00).ToString();
            }
            else if (days > 21 && days <= 28)
            {
                double x = 5.00 * (days - 21);
                txtPayment.Text = x.ToString();
            }
            else if (days > 28 && days <= 35)
            {
                double y = 5.00 * 7;
                double x = 10.00 * (days - 28);
                double sum = x + y;
                txtPayment.Text = sum.ToString();
            }
            else
            {
                double y = 5.00 * 7;
                double x = 10.00 * 7;
                double z = 20.00 * (days - 35);
                double sum = x + y + z;
                txtPayment.Text = sum.ToString();
            }
        }

        private void btnGoBack_Click(object sender, EventArgs e)
        {
            btnReturn.Hide();
            btnGoBack.Hide();

            btnInsert.Show();
            btnUpdate.Hide();
            btnReturnBook.Hide();
            dateTimePickerReturn.Hide();
            txtPayment.Hide();
            txtBookCode.Enabled = true;
            txtRegistrationNo.Enabled = true;
            dateTimePickerIssuingDate.Enabled = true;
            FieldsClear();
            lblReturnDate.Hide();
            lblPayment.Hide();
            
        }

        public void CheckBookCount()        //check number of books that issued for relavent member,before issue a new book.The reason is preventing excced the book issuing limit to relavent member
        {

            string registrationno = txtRegistrationNo.Text;
            da.SelectCommand = new SqlCommand("SELECT COUNT(M.RegistrationNo) FROM BooksIssuing L JOIN Member M ON(L.MemberID=M.MemberID) JOIN Book B ON(L.BookID=B.BookID) WHERE L.IssuingStatus=0 AND RegistrationNo=@registrationno", cs);
            da.SelectCommand.Parameters.Add("@registrationno", SqlDbType.VarChar).Value = txtRegistrationNo.Text;
            cs.Open();
            issuedBookCount = Convert.ToInt32(da.SelectCommand.ExecuteScalar());
            cs.Close();
        }

        private void btnIssuedDetails_Click(object sender, EventArgs e)
        {
            IssuingDetails();
            btnGoBack.Show();
        }

        public void CheckMemberAvailability()       //check the user registered or not before issue a book
        {
            string registrationno = txtRegistrationNo.Text;
            da.SelectCommand = new SqlCommand("SELECT COUNT(RegistrationNo) FROM BooksIssuing WHERE IssuingStatus=0 and RegistrationNo=@registrationno", cs);
            da.SelectCommand.Parameters.Add("@registrationno", SqlDbType.VarChar).Value = txtRegistrationNo.Text;
            cs.Open();
            SqlDataReader dy = da.SelectCommand.ExecuteReader();
            if (dy.Read())
            {

                string BookCount = dy.GetValue(0).ToString();
            }
            cs.Close();
        }

        public void dataGridViewRowCount()      //count rows of the table
        {
           
            int rowcount = dataGridView5.Rows.Count;
            lblCount.Text = rowcount.ToString();
        }  
           
    }
}
