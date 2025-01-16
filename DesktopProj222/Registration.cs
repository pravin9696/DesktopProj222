using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DesktopProj222
{
    public partial class Registration : Form
    {
        public int empid;
        public Registration()
        {
            InitializeComponent();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            btnSave.Enabled = true;
            txtName.Text = "";
            txtContact.Text = "";
            txtEmail.Text = "";
            txtAddress.Text = "";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            String str = Program.Constr;

            string nm = txtName.Text;
            string cnt = txtContact.Text;
            string email = txtEmail.Text;
            string add = txtAddress.Text;
            SqlConnection con=null;
            try
            {

            con = new SqlConnection(str);

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "insert into tblEmployee values(@name,@contact,@email,@address)";
            cmd.Parameters.AddWithValue("@name", nm);
            cmd.Parameters.AddWithValue("@address", add);
            cmd.Parameters.AddWithValue("@contact", cnt);
            cmd.Parameters.AddWithValue("@email", email);

            con.Open();
            int n=cmd.ExecuteNonQuery();
            if (n > 0) {
                MessageBox.Show("Record inserted Successfully");
            }
            else
            {
                MessageBox.Show("Record not inserted!!!");

            }
           

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                con.Close();
            }
            btnSave.Enabled = false;

            Registration_Load(sender,e);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string str= Program.Constr;

            string nm = txtName.Text;
            int id;


            SqlConnection con = new SqlConnection(str);

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "select * from tblEmployee where name=@nm";
            cmd.Parameters.AddWithValue("@nm", nm);
            con.Open();
           SqlDataReader rdr= cmd.ExecuteReader();
            if (rdr.Read())
            {
                id = (int)rdr["id"];
                txtContact.Text = rdr["contact"].ToString();
                txtEmail.Text = rdr["email"].ToString();
                txtAddress.Text = rdr["Address"].ToString();
                btnUpdate.Enabled = true;
            }
            else
            {
                txtContact.Text = "";
                txtEmail.Text = "";
                txtAddress.Text = "";
                btnUpdate.Enabled = false;
                MessageBox.Show("Record not found!!!");
            }
            
        }

        private void Registration_Load(object sender, EventArgs e)
        {
            string str = Program.Constr;
            SqlConnection con = new SqlConnection(str);

            SqlDataAdapter adp = new SqlDataAdapter("select * from tblEmployee",con);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            dataGridView1.DataSource= dt;
           // dataGridView1.Columns[0].Visible = false;
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {         
         

                var selectData =(DataRowView) dataGridView1.SelectedRows[0].DataBoundItem;
                empid = int.Parse(selectData["id"].ToString());
                txtName.Text = selectData["name"].ToString();
                txtAddress.Text = selectData["Address"].ToString();
                txtContact.Text = selectData["contact"].ToString();
                txtEmail.Text= selectData["email"].ToString();
            
            }
            catch (Exception ex)
            {
                Console.WriteLine("exception occured"+ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string str = Program.Constr;
            SqlConnection con = new SqlConnection(str);

            SqlDataAdapter adp = new SqlDataAdapter("select * from tblEmployee", con);
            DataTable dt = new DataTable();
            SqlCommandBuilder cmdb=new SqlCommandBuilder(adp);
            adp.Fill(dt);
            
            foreach (DataRow dr in dt.Rows)
            {
                if (int.Parse(dr[0].ToString())==empid)
                {
                    dr["name"] = txtName.Text;
                    dr["contact"] = txtContact.Text;
                    dr["email"] = txtEmail.Text;
                    dr["Address"] = txtAddress.Text;
                    int n = adp.Update(dt);
                    if (n > 0)
                    {
                        MessageBox.Show("record updated successfully...");
                    }
                    else
                    {
                        MessageBox.Show("not updated !!!!");
                    }
                   
                }    
            }
            Registration_Load(sender, e);
        }
    }
}
