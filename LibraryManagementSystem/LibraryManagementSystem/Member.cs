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
    public partial class Member : Form
    {
        SqlConnection cs = new SqlConnection(Properties.Settings.Default.Connection); 
        SqlDataAdapter da = new SqlDataAdapter();

        //If member has books to return,that member status=1.If not member status=0

        bool dateChanged;
        bool roleChanged;
        bool FieldEmpty;
        int lendCount = 0;
        string selectedRole;
        int payment=0;
        int positionID;
        decimal x;
        int RegNoExist;
        string position;
        int issuedBookCount;
        int memberCount;

        public Member()
        {
            InitializeComponent();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            CheckMemberAvailability();           

           if ((txtRegistrationNo.Text.Trim() == "") || (txtFname.Text.Trim() == "") || (txtLname.Text.Trim() == "") || (txtNIC.Text.Trim() == "") || (txtAddress.Text.Trim() == "") || (txtPhone.Text.Trim() == "") || (txtEmail.Text.Trim() == "") || (dateChanged == false) || (cmbPosition.SelectedIndex==0))
            {
               MessageBox.Show("Please fill all the fields!","Warning",MessageBoxButtons.OK,MessageBoxIcon.Warning);
           }
            else
           {
               if (RegNoExist == 1)
               {
                   MessageBox.Show("Registration Number already exist!","Warning",MessageBoxButtons.OK,MessageBoxIcon.Warning);
               }
               else
               {
                   da.InsertCommand = new SqlCommand("INSERT INTO Member VALUES(@positionid,@registrationno,@fname,@lname,@nic,@phoneno,@email,@address,@registereddate)", cs);

                   da.InsertCommand.Parameters.Add("@registrationno", SqlDbType.VarChar).Value = txtRegistrationNo.Text.Trim();
                   da.InsertCommand.Parameters.Add("@fname", SqlDbType.VarChar).Value = txtFname.Text.Trim();
                   da.InsertCommand.Parameters.Add("@lname", SqlDbType.VarChar).Value = txtLname.Text.Trim();
                   da.InsertCommand.Parameters.Add("@nic", SqlDbType.Int).Value = txtNIC.Text.Trim();
                   da.InsertCommand.Parameters.Add("@phoneno", SqlDbType.Int).Value = txtPhone.Text.Trim();
                   da.InsertCommand.Parameters.Add("@positionid", SqlDbType.Int).Value = positionID;
                   da.InsertCommand.Parameters.Add("@email", SqlDbType.VarChar).Value = txtEmail.Text.Trim();
                   da.InsertCommand.Parameters.Add("@address", SqlDbType.VarChar).Value = txtAddress.Text.Trim();
                   da.InsertCommand.Parameters.Add("@registereddate", SqlDbType.Date).Value = dateTimePickerRegister.Value.Date;

                   cs.Open();
                   try
                   {
                       da.InsertCommand.ExecuteNonQuery();
                       MessageBox.Show("Successfuly Registered!","Successed",MessageBoxButtons.OK,MessageBoxIcon.Information);

                       DataGridViewData();
                       FieldsClear();                     
                   }
                   catch (FormatException)
                   {
                       int nicno;
                       int phoneno;
                     
                       if (!int.TryParse(txtNIC.Text.Trim(), out nicno))
                       {
                           MessageBox.Show("NIC should be only numbers!", "Warning",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                       }

                       else if (!int.TryParse(txtPhone.Text.Trim(), out phoneno))
                       {
                           MessageBox.Show("Phone number should be only numbers!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                       }
                       else
                       {
                           MessageBox.Show("Please try again!","Alert",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                       }                  
                   }
                   finally
                   {
                       cs.Close();
                   }
               }
           }           
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }

        private void StudentRegistration_Load(object sender, EventArgs e)
        {
            GetPositionList();
            DataGridViewData();          
            txtMemberID.Hide();                 
            cmbPosition.SelectedIndex = 0;
            dataGridView1.Columns[0].Visible = false;
           
        }

        private void dateTimePickerRegister_ValueChanged(object sender, EventArgs e)
        {
            dateChanged = true;
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string Query = "";
            if (txtSearch.Text != string.Empty)
                Query += "[Registration No] like '%" + txtSearch.Text.Trim() + "%' and ";
            Query += "1 = 1";
            (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = Query;

            dataGridViewRowCount();           
        }

        public void DataGridViewData()  //Refresh datagridview after inserting,updating and deleting
        {            
            da.SelectCommand = new SqlCommand("SELECT M.MemberID as [Member ID], M.RegistrationNo as [Registration No],M.FirstName as [First Name],M.LastName as [Last Name],M.NIC,M.PhoneNo as [Phone No],M.Email as [E-mail],M.Address,M.RegisteredDate as [Registered Date],P.Position FROM Member M JOIN Position P ON(M.PositionID=P.PositionID)", cs);
            DataTable ds = new DataTable();
            da.Fill(ds);
            dataGridView1.DataSource = ds;

            dataGridViewRowCount();
        }       

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
           

            txtMemberID.Text = this.dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txtRegistrationNo.Text = this.dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtFname.Text = this.dataGridView1.CurrentRow.Cells[2].Value.ToString();
            txtLname.Text = this.dataGridView1.CurrentRow.Cells[3].Value.ToString();         
            txtNIC.Text = this.dataGridView1.CurrentRow.Cells[4].Value.ToString();
           txtPhone.Text = this.dataGridView1.CurrentRow.Cells[5].Value.ToString();
           txtEmail.Text = this.dataGridView1.CurrentRow.Cells[6].Value.ToString();
            txtAddress.Text = this.dataGridView1.CurrentRow.Cells[7].Value.ToString();
            dateTimePickerRegister.Text = this.dataGridView1.CurrentRow.Cells[8].Value.ToString();
            cmbPosition.Text = this.dataGridView1.CurrentRow.Cells[9].Value.ToString();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            GetPositionID();
            if ((txtMemberID.Text=="") || (txtRegistrationNo.Text.Trim() == "") || (txtFname.Text.Trim() == "") || (txtLname.Text.Trim() == "") || (txtNIC.Text.Trim() == "") || (txtAddress.Text.Trim() == "") || (txtPhone.Text.Trim() == "") || (txtEmail.Text.Trim() == "") || (dateChanged == false) || (cmbPosition.SelectedIndex==0))
            {
                MessageBox.Show("Please select member!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                    da.UpdateCommand = new SqlCommand("UPDATE Member SET RegistrationNo='" + txtRegistrationNo.Text + "',FirstName='" + txtFname.Text + "',LastName='" + txtLname.Text + "',NIC='" + txtNIC.Text + "',PhoneNo='" + txtPhone.Text + "',Email='" + txtEmail.Text + "',Address='" + txtAddress.Text + "',PositionID='" + positionID + "' WHERE MemberID='" + txtMemberID.Text + "'", cs);
                    cs.Open();
                    try
                    {
                        da.UpdateCommand.ExecuteNonQuery();
                        MessageBox.Show("Staff member details Updated!","Successed",MessageBoxButtons.OK,MessageBoxIcon.Information);

                        DataGridViewData();
                        FieldsClear();
                                           
                    }
                    catch (FormatException)
                    {
                        int nicno;
                        int phoneno;
                    
                        if (!int.TryParse(txtNIC.Text.Trim(), out nicno))
                        {
                            MessageBox.Show("NIC should be only numbers!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else if (!int.TryParse(txtPhone.Text.Trim(), out phoneno))
                        {
                            MessageBox.Show("Phone number should be only numbers!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            MessageBox.Show("Please try again!","Alert",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                        }                     
                    }
                    finally
                    {
                        cs.Close();
                    }
                
               }        
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            IssuedBookCount();

            if (txtMemberID.Text.Trim() == "")
            {
                MessageBox.Show("Please select member!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (issuedBookCount > 0)
                {                    
                    MessageBox.Show("This member cannot removed!\nThis member has " + issuedBookCount.ToString() + " books to return.","Information",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                }
                else
                {
                     
                    DialogResult dr;
                    dr = MessageBox.Show("Are you sure? \nThere is no undo after deleting!", "Confirm deletion.", MessageBoxButtons.YesNo,MessageBoxIcon.Question);
                    if (dr == DialogResult.Yes)
                    {
                        da.DeleteCommand = new SqlCommand("DELETE FROM Member WHERE MemberID=@memberid", cs);
                        da.DeleteCommand.Parameters.Add("@memberid", SqlDbType.VarChar).Value = txtMemberID.Text;

                        cs.Open();

                        da.DeleteCommand.ExecuteNonQuery();

                        MessageBox.Show("Permanently Deleted!","Successed",MessageBoxButtons.OK,MessageBoxIcon.Information);
                        DataGridViewData();
                        FieldsClear();
                       
                    }
                    else
                    {
                        MessageBox.Show("Deletion Cancelled!","Alert",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                        btnRegister.Enabled = true; 

                    }
                    cs.Close();
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            FieldsClear();
        }

        public void FieldsClear()
        {
            dateTimePickerRegister.Value = new DateTime(2021, 01, 01);
            dateChanged = false;         
            cmbPosition.SelectedIndex = 0;

            foreach (Control c in Controls)    //Clear all textBoxes
            {
                if (c is TextBox)
                {
                    c.Text = "";
                }
            }
        }
      
        private void cmbRole_Validating(object sender, CancelEventArgs e)
        {
            GetPositionID();
        }

        public void GetPositionList()
        {

            da.SelectCommand = new SqlCommand("SELECT * FROM Position", cs);   //Get position list for the combo box
            cs.Open();
            SqlDataReader dr = da.SelectCommand.ExecuteReader();

            while (dr.Read())
            {
                cmbPosition.Items.Add(dr["Position"]);
            }
            cs.Close();

            cmbPosition.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        public void GetPositionID()     //Get the position id of the relevant member position 
        {
            string position = cmbPosition.SelectedItem.ToString();
            da.SelectCommand = new SqlCommand("SELECT PositionID FROM Position WHERE Position=@position", cs);
            da.SelectCommand.Parameters.Add("@position", SqlDbType.VarChar).Value = cmbPosition.SelectedItem.ToString();
            cs.Open();

            SqlDataReader dr = da.SelectCommand.ExecuteReader();
            if (dr.Read())
            {
                string x = dr.GetValue(0).ToString();
                positionID = int.Parse(x);               
            }
            cs.Close();
        }

        public void IssuedBookCount()   //Count the number of books that issued for the each member at the current time
        {
            string memberid = txtMemberID.Text;
            da.SelectCommand = new SqlCommand("SELECT COUNT(MemberID) FROM BooksIssuing WHERE IssuingStatus=0 and MemberID=@memberid", cs);
            da.SelectCommand.Parameters.Add("@memberid", SqlDbType.VarChar).Value = txtMemberID.Text;
            cs.Open();
            issuedBookCount = Convert.ToInt32(da.SelectCommand.ExecuteScalar());
          
            cs.Close();
        }
        public void CheckMemberAvailability()  //Check whether the member already registered or not
        {
            string registrationno = txtRegistrationNo.Text;  
            da.SelectCommand = new SqlCommand("SELECT COUNT(RegistrationNo) FROM Member WHERE RegistrationNo=@registrationno", cs);
            da.SelectCommand.Parameters.Add("@registrationno", SqlDbType.VarChar).Value = txtRegistrationNo.Text;
            cs.Open();
          
            SqlDataReader dy = da.SelectCommand.ExecuteReader();
            if (dy.Read())
            {
                string x = dy.GetValue(0).ToString();
                RegNoExist = int.Parse(x);
              
            }
            cs.Close();
        }
        public void dataGridViewRowCount()  //Count of the members of the table
        {
            int rowcount = dataGridView1.Rows.Count;
            lblCount.Text = rowcount.ToString();
        }

        private void txtRegistrationNo_Validating(object sender, CancelEventArgs e)
        {
            CheckMemberAvailability();
            if (RegNoExist == 1)
                {
                    MessageBox.Show("Registration Number already exist", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
               
        }
    }
}
