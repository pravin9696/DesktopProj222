using DesktopProj222.Modals;
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
    public partial class frmRegistration : Form
    {
        private int empID;
        public frmRegistration()
        {
            InitializeComponent();
        }

        private void frmRegistration_Load(object sender, EventArgs e)
        {
            string constring = Program.Constr;
            SqlConnection con = new SqlConnection(constring);
            SqlDataAdapter adp = new SqlDataAdapter("select * from tblEmployee",con);

            DataSet ds = new DataSet();

            adp.Fill(ds, "tblEmployee");

            //DataTable dt=new DataTable();
            //adp.Fill(dt);

            dataGridView1.DataSource = ds.Tables["tblEmployee"];
            dataGridView1.Columns[0].Visible = false;


        }

        private void btnNew_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
             var data= (DataRowView) dataGridView1.SelectedRows[0].DataBoundItem;
            empID =int.Parse( data["id"].ToString());
            txtName.Text = data[1].ToString();
            txtAddress.Text = data["Address"].ToString();
            txtContact.Text = data["contact"].ToString();
            txtEmail.Text = data["email"].ToString();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //----------------------------------------
            //if (txtEmail.Text == "" || txtName.Text == "" || txtContact.Text == "" || txtAddress.Text == "")
            //{
            //    MessageBox.Show("please select record in GridView");
            //}
            //else
            //{
            //    DB_desktop_demoEntities dbo = new DB_desktop_demoEntities();

            //    var emp = dbo.tblEmployees.FirstOrDefault(x => x.id == empID);
            //    if (emp != null)
            //    {
            //        emp.name = txtName.Text;
            //        emp.contact = txtContact.Text;
            //        emp.email = txtEmail.Text;
            //        emp.Address = txtAddress.Text;

            //        int n = dbo.SaveChanges();
            //        if (n > 0)
            //        {
            //            MessageBox.Show("record updated successfully...");
            //        }
            //        else
            //        {
            //            MessageBox.Show("not updated!!!!");
            //        }

            //    }




            //    frmRegistration_Load(sender, e);

            //--------------------------------------------

            string constr = Program.Constr;
            SqlConnection con=new SqlConnection(constr);

            SqlDataAdapter adp = new SqlDataAdapter("select * from tblEmployee", con);

            SqlCommandBuilder  cmb = new SqlCommandBuilder(adp);

            DataTable dt = new DataTable();
            adp.Fill(dt);
            foreach (DataRow row in dt.Rows)
            {
                if (int.Parse(row["id"].ToString())==empID)
                {
                    row["name"] = txtName.Text;
                    row["contact"] = txtContact.Text;
                    row["email"]= txtEmail.Text;
                    row["Address"]= txtAddress.Text;
                }
            }
            int n = adp.Update(dt);
            if (n > 0)
            {
                MessageBox.Show("record updated successfully...");
                frmRegistration_Load(sender, e);
            }
            else
            {
                MessageBox.Show("not updated!!!!");
            }

        }
    }
    
}
