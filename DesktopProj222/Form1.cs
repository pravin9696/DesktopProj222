using DesktopProj222.Modals;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DesktopProj222
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DB_desktop_demoEntities dbo = new DB_desktop_demoEntities();
            var emps = dbo.tblEmployees.ToList();
            dataGridView1.DataSource = emps;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var emp = dataGridView1.SelectedRows[0].DataBoundItem as tblEmployee;
            txtID.Text = emp.id.ToString();
            txtName.Text = emp.name.ToString();
            txtContact.Text = emp.contact.ToString();
            txtEmail.Text= emp.email.ToString();
            txtAddress.Text = emp.Address.ToString();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DB_desktop_demoEntities dbo = new DB_desktop_demoEntities();
            tblEmployee emp = new tblEmployee();
            emp.name = txtName.Text;
            emp.contact = txtContact.Text;
            emp.email = txtEmail.Text;
            emp.Address=txtAddress.Text;
            dbo.tblEmployees.Add(emp);
            if (dbo.SaveChanges()>0)
            {
                MessageBox.Show("inserted new record successfully");
            }
            else
            {
                MessageBox.Show("not inserted!!");
            }
            Form1_Load(sender, e);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtID.Text.Trim()))
            {
                MessageBox.Show("enter emp id to delete record");
                return;
            }
            int id = int.Parse(txtID.Text);
            DB_desktop_demoEntities dbo = new DB_desktop_demoEntities();
            var emp = dbo.tblEmployees.FirstOrDefault(x => x.id == id);
            if (emp != null) {
                dbo.tblEmployees.Remove(emp);
                dbo.SaveChanges();
                MessageBox.Show("record deleted successfully");
            }
            else
            {
                MessageBox.Show("record not deleted!!!");
            }
            Form1_Load(sender: sender, e);
        }
    }
}
