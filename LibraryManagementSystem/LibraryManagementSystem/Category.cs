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
    public partial class Category : Form
    {
       SqlConnection cs = new SqlConnection(Properties.Settings.Default.Connection);
        SqlDataAdapter da = new SqlDataAdapter();
        DataSet ds = new DataSet();

        int categoryCount=0;
        bool catNameExist;

        public Category()
        {
            InitializeComponent();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {

            CatNameExist();


            if (txtCategory.Text.Trim()== "")
            {
               
                MessageBox.Show("Please fill all the fields!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }
                else
                {
                    if (catNameExist==true)
                    {
                      
                        MessageBox.Show("This category already exist!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        
                        txtCategory.Clear();
                    }
                    else
                    {
                        
                        da.InsertCommand = new SqlCommand("INSERT INTO Category VALUES(@categoryname)", cs);
                        da.InsertCommand.Parameters.Add("@categoryname", SqlDbType.VarChar).Value = txtCategory.Text.Trim();

                        cs.Open();

                        da.InsertCommand.ExecuteNonQuery();
                        MessageBox.Show("Successfuly Inserted!", "Successed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                        AutoRefreshGridView();
                        FieldClear();

                        cs.Close();

                    }
                }
       
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            
            string Query = "";
            if (txtSearch.Text != string.Empty)
                Query += "[Category] like '%" + txtSearch.Text.Trim() + "%' and ";
            Query += "1 = 1";
            (dataGridView4.DataSource as DataTable).DefaultView.RowFilter = Query;

            dataGridViewRowCount();
        }

        private void Category_Load(object sender, EventArgs e)
        {
           AutoRefreshGridView();
           this.dataGridView4.Columns[0].Visible = false;
        txtCategoryID.Hide();
           label3.Hide();
        }


      private void dataGridView4_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
          try{
           txtCategoryID.Text = this.dataGridView4.CurrentRow.Cells[0].Value.ToString();
            txtCategory.Text = this.dataGridView4.CurrentRow.Cells[1].Value.ToString();
             
              }
            catch (NullReferenceException)
            {
                MessageBox.Show("There is no details to diaplay!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
         
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            CatNameExist();

            if (txtCategory.Text.Trim() == "")
            {
                MessageBox.Show("Please select a category!","Warning",MessageBoxButtons.OK,MessageBoxIcon.Warning);

            }
            else
            {
                if (catNameExist == true)
                {
                    MessageBox.Show("This category already exist!","Warning",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    FieldClear();
                }
                else
                {
                    da.UpdateCommand = new SqlCommand("UPDATE Category SET CategoryName='" + txtCategory.Text + "' WHERE CategoryID='" + txtCategoryID.Text + "'", cs);
                    cs.Open();
                    da.UpdateCommand.ExecuteNonQuery();
                    cs.Close();
                    MessageBox.Show("Category details Updated!","Successed",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    AutoRefreshGridView();
                    FieldClear();
                }
            }
               
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void AutoRefreshGridView()       //Refresh the datagridview after data inserting,updating and deleting
        {
            da.SelectCommand = new SqlCommand("SELECT CategoryID as [ID],CategoryName as [Category] FROM Category WHERE CategoryID!=0", cs);
            DataTable ds = new DataTable();
            da.Fill(ds);
            dataGridView4.DataSource = ds;

            dataGridViewRowCount();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            FieldClear();   
        }

       
        public void FieldClear()
        {
            foreach (Control c in Controls)     //Clear all text fields
            {
                if (c is TextBox)
                {
                    c.Text = "";
                }
            }
        }

        public void CatNameExist()      //Check that category name exist before insert and update the category table
        {
            string categoryname = txtCategory.Text.Trim();
            da.SelectCommand = new SqlCommand("SELECT CategoryName FROM Category  WHERE CategoryName=@categoryname", cs);
            da.SelectCommand.Parameters.Add("@categoryname", SqlDbType.VarChar).Value = txtCategory.Text.Trim();
            cs.Open();

            SqlDataReader dx = da.SelectCommand.ExecuteReader();
            if (dx.Read() == true)
            {
                catNameExist = true;
            }
            else
            {
                catNameExist = false;
            }
            cs.Close();
        }

        public void dataGridViewRowCount()      //Get count of the categories
        {
            int rowcount = dataGridView4.Rows.Count;
            lblCount.Text = rowcount.ToString();
        }

    }
}
