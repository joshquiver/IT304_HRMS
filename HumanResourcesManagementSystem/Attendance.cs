using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace HumanResourcesManagementSystem
{
    public partial class Attendance : UserControl
    {
        public Attendance()
        {
            InitializeComponent();

        }
        public void RefreshData()
        {
            ConnectDB.dgvViewing("SELECT id, Employee_ID, Full_Name FROM employees", dgvAttendance);
        }
        private void Attendance_Load(object sender, EventArgs e)
        {
            UpdateDateTime();
            RefreshData();

            // You can also use a timer to update the time at regular intervals
            Timer timer = new Timer();
            timer.Interval = 1000; // 1000 milliseconds = 1 second
            timer.Tick += timer1_Tick;
            timer.Start();
            dtpDate.Visible = false;
            tbxSearchDate.Visible = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            UpdateDateTime();
        }
        private void UpdateDateTime()
        {
            DateTime currentDateTime = DateTime.Now;

            labelDateTime.Text = currentDateTime.ToString("MM-dd-yyyy hh:mm:ss tt");
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Timeout_btn_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Time_btn_Click(object sender, EventArgs e)
        {
            if (Time_btn.Text == "BACK")
            {
                RefreshData();
                Time_btn.Text = "TIME IN/TIME OUT";
                label2.Visible = true;
                label3.Visible = true;
                cbxTime.Visible = true;
                cbxAmPm.Visible = true;
                dtpDate.Visible = false;
                tbxSearchDate.Visible = false;
                label1.Text = "Employee's Attendance";
            }
            else if (cbxTime.Text == "" || cbxAmPm.Text == "")
            {
                MessageBox.Show("Please fill all blank fields", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DateTime today = DateTime.Today;
                string employeeID = lblID.Text;
                string timeInOut = cbxTime.Text;
                string amPm = cbxAmPm.Text;

                if (ConnectDB.HasEmployeeAlreadyClockedInOrOut(employeeID, timeInOut, amPm, today))
                {
                    MessageBox.Show("Employee has already Time in or out with the same AM/PM and date.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    ConnectDB.saveLog("INSERT INTO `log`(`id`, `log_message`, `log_time`, `log_failures`) VALUES ("+ employeeID +", 'Successfully saved Time In for user: " + employeeID + "', NOW(), '0')");

                    ConnectDB.saveUpdateDeleteData("INSERT INTO `attendance`(`Employee_ID`, `Time_In_Time_Out`, `AM_PM`, `Time`, `Date`) VALUES ('" + employeeID + "','" + timeInOut + "','" + amPm + "','" + DateTime.Now.ToString("hh:mm tt") + "','" + today.ToString("MM-dd-yyyy") + "')", "Saved Successfully");
                }
            }
        }
        
        private void cbxTime_TextChanged(object sender, EventArgs e)
        {
            Time_btn.Text = cbxTime.Text;
            Time_btn.BackColor = cbxTime.Text == "TIME OUT" ? Color.Red : Color.LimeGreen;
        }

        private void lblID_Click(object sender, EventArgs e)
        {

        }

        private void dgvAttendance_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                DataGridViewRow row = dgvAttendance.Rows[e.RowIndex];
                lblID.Text = row.Cells[1].Value.ToString();
                lblName.Text = row.Cells[2].Value.ToString();

            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ConnectDB.dgvViewing("SELECT employees.id, employees.Employee_ID, employees.Full_Name, attendance.Time_In_Time_Out, attendance.AM_PM, attendance.Time, attendance.Date FROM employees INNER JOIN attendance ON employees.Employee_ID = attendance.Employee_ID", dgvAttendance);
            Time_btn.Text = "BACK";
            label2.Visible = false;
            label3.Visible = false;
            cbxTime.Visible = false;
            cbxAmPm.Visible = false;
            dtpDate.Visible = true;
            tbxSearchDate.Visible = true;
            label1.Text = "Attendance History";
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            ConnectDB.dgvViewing("SELECT `id`, `Employee_ID`, `Full_Name` FROM employees WHERE Full_Name like '%" + tbxSearch.Text + "%'", dgvAttendance);

        }

        private void tbxSearchDate_Click(object sender, EventArgs e)
        {
            string selectedDate = dtpDate.Value.ToString("MM-dd-yyyy");

            string query = "SELECT employees.id, employees.Employee_ID, employees.Full_Name, attendance.Time_In_Time_Out, attendance.AM_PM, attendance.Time, attendance.Date FROM employees INNER JOIN attendance ON employees.Employee_ID = attendance.Employee_ID WHERE Date = '" + selectedDate + "'";

            ConnectDB.dgvViewing(query, dgvAttendance);
        }
    }
}
