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
    
    public partial class Books : Form
    {
        SqlConnection cs = new SqlConnection(Properties.Settings.Default.Connection);
        SqlDataAdapter da = new SqlDataAdapter();
        DataSet ds = new DataSet();
   
        int catid;
        bool dateChanged;
        double price;
        string categoryname;
        string strBookCode;
        string studentStatus;
        string staffStatus;
        bool FieldEmpty;
        int BookCount;
        bool BookCodeExist;

        public Books()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            CheckBookCodeAvailability();
          
            if ((txtBookCode.Text.Trim() == "") || (txtBookTitle.Text.Trim() == "") || (txtAuthor.Text.Trim() == "") || (txtPrice.Text.Trim() == "") || (cmbCategory.SelectedIndex == 0))
            {

                MessageBox.Show("Please fill all the fields!","Warning",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
            else
            {

            if (BookCodeExist == true)
            {
                MessageBox.Show("This book code already exist!","Warning",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
            else
            {
                da.InsertCommand = new SqlCommand("INSERT INTO Book VALUES(@bookcode,@categoryid,@booktitle,@author,@price,@registereddate)", cs);
                da.InsertCommand.Parameters.Add("@bookcode", SqlDbType.VarChar).Value = txtBookCode.Text.Trim();
                da.InsertCommand.Parameters.Add("@categoryid", SqlDbType.Int).Value = catid;
                da.InsertCommand.Parameters.Add("@booktitle", SqlDbType.VarChar).Value = txtBookTitle.Text.Trim();
                da.InsertCommand.Parameters.Add("@author", SqlDbType.VarChar).Value = txtAuthor.Text.Trim();
                da.InsertCommand.Parameters.Add("@price", SqlDbType.Decimal).Value = txtPrice.Text.Trim();
                da.InsertCommand.Parameters.Add("@registereddate", SqlDbType.Date).Value = dateTimePickerRegister.Value.Date;

                cs.Open();
                try
                {
                    da.InsertCommand.ExecuteNonQuery();
                    MessageBox.Show("Successfuly Inserted!", "Successed",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    AutoRefreshGridView();
                    FieldsClear();
                }

                catch (FormatException)
                {
                    MessageBox.Show("Price can only be a number!","Warning",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                }
                catch (OverflowException)
                {
                    MessageBox.Show("Maximum number limit exceeded!","Warning",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                }

                cs.Close();
            }
            }          
        }

        private void BookRegistration_Load(object sender, EventArgs e)
        {
            showcategories();
         
            AutoRefreshGridView();
            this.dataGridView2.Columns[0].Visible = false;

            txtBookID.Hide();
           
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dateTimePickerRegister_ValueChanged(object sender, EventArgs e)
        {
            dateChanged = true;
        }

        private void cmbCategory_Validating(object sender, CancelEventArgs e)
        {
            selecedcategory();
        }
       
        public void showcategories()        //Get category list to the combo box
        {         
            da.SelectCommand = new SqlCommand("SELECT * FROM Category", cs);
            cs.Open();
            SqlDataReader dr = da.SelectCommand.ExecuteReader();

            while (dr.Read())
            {
                cmbCategory.Items.Add(dr["CategoryName"]);
            }

            cs.Close();

         
            cmbCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCategory.SelectedIndex = 0;
        }

        public void selecedcategory()
        {
            if (cmbCategory.SelectedIndex == 0)
            {
                MessageBox.Show("Please select category!","Warning",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }

            categoryname = cmbCategory.SelectedItem.ToString();     //get category id of user selected category
            da.SelectCommand = new SqlCommand("SELECT CategoryID FROM Category WHERE CategoryName=@categoryname", cs);
            da.SelectCommand.Parameters.Add("@categoryname", SqlDbType.VarChar).Value = cmbCategory.SelectedItem.ToString();
            cs.Open();

            SqlDataReader dr = da.SelectCommand.ExecuteReader();
            while (dr.Read())
            {
                string x = dr.GetValue(0).ToString();
                catid = int.Parse(x);
               
            }

            cs.Close();
        }

        public void showgirdviewdata()      //Display book details in the datagridview
        {
           
            da.SelectCommand = new SqlCommand("SELECT B.BookCode as [Book Code],B.BookTitle as [Book Title],B.Author,B.Price,C.CategoryName as [Category],B.RegisteredDate as [RegisteredDate] From Book B JOIN Category C ON(B.CategoryID=C.CategoryID)", cs);
            da.Fill(ds);
            dataGridView2.DataSource = ds.Tables[0];

            DataGridViewRowCount();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string Query = "";
            if (txtSearch.Text != string.Empty)
                Query += "[Book Title] like '%" + txtSearch.Text.Trim() + "%' and ";
            Query += "1 = 1";
            (dataGridView2.DataSource as DataTable).DefaultView.RowFilter = Query;

            DataGridViewRowCount();
        }

        private void btnAvailability_Click(object sender, EventArgs e)
        {
            if (lblCount.Text == "0")
            {
                MessageBox.Show("There is no details to check availability","Alert",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            else
            {
                string bookcode = this.dataGridView2.CurrentRow.Cells[1].Value.ToString();
                da.SelectCommand = new SqlCommand("SELECT B.BookCode FROM BooksIssuing L INNER JOIN Book B ON(B.BookID=L.BookID) WHERE B.BookCode=@bookcode AND L.IssuingStatus=0", cs);
                da.SelectCommand.Parameters.Add("@bookcode", SqlDbType.VarChar).Value = this.dataGridView2.CurrentRow.Cells[1].Value.ToString();
                cs.Open();
                SqlDataReader dr = da.SelectCommand.ExecuteReader();
                if (dr.Read())
                {
                    strBookCode = dr.GetValue(0).ToString();
                }

                string booktitle = this.dataGridView2.CurrentRow.Cells[2].Value.ToString();

                if (strBookCode == bookcode)
                {
                    MessageBox.Show(booktitle + " book has lent!", "Book Availability Check", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(booktitle + " book is available in the library!", "Book Availability Check", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                cs.Close();
            }
        }
            
        public void AutoRefreshGridView()  //Refresh datagridview after inserting,updating and deleting
        {
            da.SelectCommand = new SqlCommand("SELECT B.BookID as [Book ID],B.BookCode as [Book Code],B.BookTitle as [Book Title],B.Author,B.Price,C.CategoryName as [Category],B.RegisteredDate as [RegisteredDate] From Book B JOIN Category C ON(B.CategoryID=C.CategoryID)", cs);
            DataTable ds = new DataTable();
            da.Fill(ds);
            dataGridView2.DataSource = ds;

            DataGridViewRowCount();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            selecedcategory();
            if(txtBookCode.Text.Trim()=="")
            {
                MessageBox.Show("Please select a book!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
            if ((txtBookCode.Text.Trim() == "") || (txtBookTitle.Text.Trim() == "") || (txtAuthor.Text.Trim() == "") || (txtPrice.Text.Trim() == "") || (cmbCategory.SelectedIndex == 0))
            {

                MessageBox.Show("Please fill all the fields!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }   
            else
            {

                da.UpdateCommand = new SqlCommand("UPDATE Book SET BookCode='"+txtBookCode.Text+"', BookTitle='" + txtBookTitle.Text + "',Author='" + txtAuthor.Text + "',Price='" + txtPrice.Text + "',CategoryID='" + catid+ "',RegisteredDate='" + dateTimePickerRegister.Value + "'  WHERE BookID='" + txtBookID.Text + "'", cs);
                cs.Open();
                try
                {
                    da.UpdateCommand.ExecuteNonQuery();

                    MessageBox.Show("Book details Updated!", "Successed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    AutoRefreshGridView();
                    FieldsClear();

                }
                catch (FormatException)
                {
                    MessageBox.Show("Price can only be a number!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                catch (OverflowException)
                {
                    MessageBox.Show("Maximum number limit exceeded!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                finally
                {
                    cs.Close();
                }
            }
            }
        }

        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
           
            btnAdd.Enabled = false;
            try
            {               
            txtBookID.Text = this.dataGridView2.CurrentRow.Cells[0].Value.ToString();           
            txtBookCode.Text = this.dataGridView2.CurrentRow.Cells[1].Value.ToString();
            txtBookTitle.Text = this.dataGridView2.CurrentRow.Cells[2].Value.ToString();
            txtAuthor.Text = this.dataGridView2.CurrentRow.Cells[3].Value.ToString();
            txtPrice.Text = this.dataGridView2.CurrentRow.Cells[4].Value.ToString();
            cmbCategory.Text = this.dataGridView2.CurrentRow.Cells[5].Value.ToString();
            dateTimePickerRegister.Text = this.dataGridView2.CurrentRow.Cells[6].Value.ToString();
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("There is no details to diaplay!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        private void btnDelete_Click(object sender, EventArgs e)
        {
          
            if (txtBookCode.Text.Trim() == "")
            {
                MessageBox.Show("Please select a book!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                    DialogResult dr;
                    dr = MessageBox.Show("Are you sure? \nThere is no undo after deleting!", "Confirm deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr == DialogResult.Yes)
                    {
                        da.DeleteCommand = new SqlCommand("DELETE FROM Book WHERE BookID=@bookid", cs);
                        da.DeleteCommand.Parameters.Add("@bookid", SqlDbType.Int).Value = txtBookID.Text;
                        cs.Open();
                        da.DeleteCommand.ExecuteNonQuery();
                        cs.Close();
                        MessageBox.Show("Permanently Deleted!", "Successed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Deletion Cancelled!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    }
                    FieldsClear();
                    AutoRefreshGridView();
            }
        }

        public void FieldsClear()
        {
            dateTimePickerRegister.Value = new DateTime(2021,01,01);
            dateChanged = false;
            txtBookCode.Enabled = true;
            btnAdd.Enabled = true;

            cmbCategory.SelectedIndex = 0;
        
          
            foreach (Control c in Controls)    //Clear all text fields
            {
                if (c is TextBox)
                {
                    c.Text = "";

                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            FieldsClear();
        }

        public void CheckBookCodeAvailability()     //Check that book issued or not to a member
        {
            string registrationno = txtBookCode.Text;
            da.SelectCommand = new SqlCommand("SELECT BookCode FROM Book WHERE BookCode=@bookcode", cs);
            da.SelectCommand.Parameters.Add("@bookcode", SqlDbType.VarChar).Value = txtBookCode.Text;
            cs.Open();
            SqlDataReader dy = da.SelectCommand.ExecuteReader();
            if (dy.Read()==true)
            {
                BookCodeExist = true;
                
            }
            cs.Close();
        }

        public void DataGridViewRowCount()      //Count of the books of the table
        {
            int rowcount = dataGridView2.Rows.Count;
            lblCount.Text = rowcount.ToString();

        }

       
    }
}

 

       

      