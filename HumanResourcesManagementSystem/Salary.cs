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
using MySql.Data.MySqlClient;

namespace HumanResourcesManagementSystem
{
    public partial class Salary : UserControl
    {

        public Salary()
        {
            InitializeComponent();
            disableFields();
        }

        public void RefreshData()
        {
            ConnectDB.dgvViewing("SELECT * FROM employees WHERE status = 'Active' AND delete_date IS NULL", dgvSalary);
            dgvSalary.Columns[10].Visible = false;

            disableFields();

        }

        public void disableFields()
        {
            salary_employeeID.Enabled = false;
            salary_name.Enabled = false;
            salary_position.Enabled = false;
        }

       

        private void salary_updateBtn_Click(object sender, EventArgs e)
        {
            if (salary_employeeID.Text == ""
        || salary_name.Text == ""
        || salary_position.Text == ""
        || salary_salary.Text == "")
            {
                MessageBox.Show("Please fill all blank fields", "Error Message"
                    , MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DialogResult check = MessageBox.Show("Are you sure you want to UPDATE Employee ID: "
                    + salary_employeeID.Text.Trim() + "?", "Confirmation Message"
                    , MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (check == DialogResult.Yes)
                {
                   
                    try
                    {
                           
                        DateTime today = DateTime.Today;
                        
                        string id= salary_employeeID.Text;

                        ConnectDB.saveLog("INSERT INTO `log`(`id`, `log_message`, `log_time`, `log_failures`) VALUES (" + id + ", 'Successfully UPDATE SALARY  In for userID: " + id + "', NOW(), '0')");
                        ConnectDB.saveUpdateDeleteData("UPDATE employees SET salary = '" + salary_salary.Text + "',update_date = '" + today.ToString("MM-dd-yyyy") + "' WHERE employee_id ='" + id + "'", "Updated Successfully");

                        RefreshData();

                        clearFields();
                        
                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show("Error: " + ex.Message, "Error Message"
                            , MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    
                }
                else
                {
                    MessageBox.Show("Cancelled", "Information Message"
                    , MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        public void clearFields()
        {
            salary_employeeID.Text = "";
            salary_name.Text = "";
            salary_position.Text = "";
            salary_salary.Text = "";
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                DataGridViewRow row = dgvSalary.Rows[e.RowIndex];
                salary_employeeID.Text = row.Cells[1].Value.ToString();
                salary_name.Text = row.Cells[2].Value.ToString();
                salary_position.Text = row.Cells[4].Value.ToString();
                salary_salary.Text = row.Cells[7].Value.ToString();
            }
        }

        private void salary_clearBtn_Click(object sender, EventArgs e)
        {
            clearFields();
        }

        private void Salary_Load(object sender, EventArgs e)
        {
           
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            ConnectDB.dgvViewing("SELECT * FROM employees WHERE Full_Name like '%" + tbxSearch.Text + "%'", dgvSalary);

        }
    }
    
}
