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
using System.IO;
using MySql.Data.MySqlClient;
using System.Data.Common;

namespace HumanResourcesManagementSystem
{
    public partial class AddEmployee : UserControl
    {

        public AddEmployee()
        {
            InitializeComponent();
         

        }

        public void RefreshData()
        {
            ConnectDB.dgvViewing("SELECT * FROM employees WHERE delete_date IS NULL", dgvEmplooyeesData);
        }

        private void addEmployee_addBtn_Click(object sender, EventArgs e)
        {

            if (addEmployee_id.Text == ""
      || addEmployee_fullName.Text == ""
      || addEmployee_gender.Text == ""
      || addEmployee_phoneNum.Text == ""
      || addEmployee_position.Text == ""
      || addEmployee_status.Text == ""
      || addEmployee_picture.Image == null)
            {
                MessageBox.Show("Please fill all blank fields", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                string employeeId = addEmployee_id.Text;

                // Check if the employee_id already exists
                if (IsEmployeeIdTaken(employeeId))
                {
                    MessageBox.Show("Employee ID is already taken. Please choose a different one.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    
                    string path = Path.Combine(@"C:\Users\Angelo\Desktop\HumanResourcesManagementSystem\HumanResourcesManagementSystem\Directory" + employeeId + ".jpg");

                    string directoryPath = Path.GetDirectoryName(path);

                    if (!Directory.Exists(directoryPath))
                    {
                        Directory.CreateDirectory(directoryPath);
                    }

                    File.Copy(addEmployee_picture.ImageLocation, path, true);

                    DateTime today = DateTime.Today;

                    ConnectDB.saveLog("INSERT INTO `log`(`id`, `log_message`, `log_time`, `log_failures`) VALUES (" + employeeId + ", 'Successfully Add new User: " + employeeId + "', NOW(), '0')");

                    ConnectDB.saveUpdateDeleteData("INSERT INTO employees( employee_id, full_name, gender, contact_number, position, image, insert_date, salary,  status) VALUES('" + employeeId + "','" + addEmployee_fullName.Text + "', '" + addEmployee_gender.Text + "', '" + addEmployee_phoneNum.Text + "', '" + addEmployee_position.Text + "', '" + path + "', '" + today.ToString("MM-dd-yyyy") + "', '" + 0 + "', '" + addEmployee_status.Text + "')", "Added Successfully");
                    RefreshData();
                    clearFields();
                }
            }
        }
        private bool IsEmployeeIdTaken(string employeeId)
        {
            // Assuming ConnectDB class has a method like IsEmployeeIdTaken(string employeeId) that checks if the employee_id already exists
            // Replace it with the actual method you have in your ConnectDB class
            return ConnectDB.IsEmployeeIdTaken(employeeId);
        }

        public void clearFields()
        {
            addEmployee_id.Text = "";
            addEmployee_fullName.Text = "";
            addEmployee_gender.SelectedIndex = -1;
            addEmployee_phoneNum.Text = "";
            addEmployee_position.SelectedIndex = -1;
            addEmployee_status.SelectedIndex = -1;
            addEmployee_picture.Image = null;
        }

        private void addEmployee_updateBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(addEmployee_id.Text)
         || string.IsNullOrWhiteSpace(addEmployee_fullName.Text)
         || string.IsNullOrWhiteSpace(addEmployee_gender.Text)
         || string.IsNullOrWhiteSpace(addEmployee_phoneNum.Text)
         || string.IsNullOrWhiteSpace(addEmployee_position.Text)
         || string.IsNullOrWhiteSpace(addEmployee_status.Text))
       
            {
                MessageBox.Show("Please fill all blank fields", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DialogResult check = MessageBox.Show($"Are you sure you want to UPDATE Employee ID: {addEmployee_id.Text.Trim()}?",
                    "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                if (check == DialogResult.Yes)
                {
                    try
                    {
                        
                        DateTime today = DateTime.Today;
                        string id = addEmployee_id.Text;
                        ConnectDB.saveUpdateDeleteData("UPDATE employees SET full_name = '" + addEmployee_fullName.Text + "',gender = '" + addEmployee_gender.Text + "', contact_number = '" + addEmployee_phoneNum.Text + "', position = '" + addEmployee_position.Text + "', update_date ='" + today.ToString("MM-dd-yyyy") +"', status = '" + addEmployee_status.Text + "' WHERE employee_id = '" + id + "'", "Updated Successfully");
                        
                        RefreshData();

                        clearFields();
                        
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error: {ex.Message}", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Cancelled.", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void addEmployee_clearBtn_Click(object sender, EventArgs e)
        {
            clearFields();

        }

        private void addEmployee_deleteBtn_Click(object sender, EventArgs e)
        {
            if (addEmployee_id.Text == ""
        || addEmployee_fullName.Text == ""
        || addEmployee_gender.Text == ""
        || addEmployee_phoneNum.Text == ""
        || addEmployee_position.Text == ""
        || addEmployee_status.Text == "")
            {
                MessageBox.Show("Please fill all blank fields"
                    , "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DialogResult check = MessageBox.Show("Are you sure you want to DELETE " +
                    "Employee ID: " + addEmployee_id.Text.Trim() + "?", "Confirmation Message"
                    , MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                if (check == DialogResult.Yes)
                {
                    try
                    {
                        DateTime today = DateTime.Today;

                        ConnectDB.saveLog("INSERT INTO `log`(`id`, `log_message`, `log_time`, `log_failures`) VALUES (" + addEmployee_id.Text + ", 'Successfully DELETED EMPLOYEE In for user: " + addEmployee_id.Text + "', NOW(), '0')");
                        ConnectDB.saveUpdateDeleteData("UPDATE employees SET delete_date =  '" + today.ToString("MM-dd-yyyy") + "' WHERE employee_id = '" + addEmployee_id.Text + "'", "Deleted Successfully");

                        RefreshData();

                        clearFields();
                    
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex
                        , "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Cancelled."
                        , "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void addEmployee_importBtn_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "Image Files (*.jpg; *.png)|*.jpg;*.png";
                string imagePath = "";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    imagePath = dialog.FileName;
                    addEmployee_picture.ImageLocation = imagePath;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex, "Error Message"
                    , MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddEmployee_Load(object sender, EventArgs e)
        {
            RefreshData();
            dgvEmplooyeesData.Columns[10].Visible = false;
        }

        private void dgvEmplooyeesData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                DataGridViewRow row = dgvEmplooyeesData.Rows[e.RowIndex];
                addEmployee_id.Text = row.Cells[1].Value.ToString();
                addEmployee_fullName.Text = row.Cells[2].Value.ToString();
                addEmployee_gender.Text = row.Cells[3].Value.ToString();
                addEmployee_phoneNum.Text = row.Cells[4].Value.ToString();
                addEmployee_position.Text = row.Cells[5].Value.ToString();

                string imagePath = row.Cells[6].Value.ToString();

                if (!string.IsNullOrEmpty(imagePath) && File.Exists(imagePath))
                {
                    try
                    {
                        addEmployee_picture.Image = Image.FromFile(imagePath);
                    }
                    catch (OutOfMemoryException ex)
                    {
                        // Handle OutOfMemoryException if the image cannot be loaded
                        MessageBox.Show("Error loading image: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        addEmployee_picture.Image = null;
                    }
                }
                else
                {
                    addEmployee_picture.Image = null;
                }


                addEmployee_status.Text = row.Cells[11].Value.ToString();
            }
        }

        private void addEmployee_picture_Click(object sender, EventArgs e)
        {

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            ConnectDB.dgvViewing("SELECT * FROM employees WHERE Full_Name like '%" + tbxSearch.Text + "%'", dgvEmplooyeesData);

        }
    }

}
