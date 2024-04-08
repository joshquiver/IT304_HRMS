using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace HumanResourcesManagementSystem
{

    public partial class LoginForm : Form
    {

        public LoginForm()
        {
            InitializeComponent();
        }

        private void login_btn_Click(object sender, EventArgs e)
        {
            if (login_username.Text == "" || login_password.Text == "")
            {
                MessageBox.Show("Please fill all blank fields", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    bool isAuthenticated = ConnectDB.GetUserPassword(login_username.Text, login_password.Text);

                    bool userID = ConnectDB.AccountVerification(login_username.Text, login_password.Text);

                    if (isAuthenticated)
                    {
                        // Log successful login attempt
                        ConnectDB.saveLog("INSERT INTO `log`(`id`, `log_message`, `log_time`, `log_failures`) VALUES ('"+ userID +"', 'Successful login for user: " + login_username.Text + "', NOW(), '0')");

                        MessageBox.Show("Login successfully!", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        MainForm mForm = new MainForm();
                        mForm.Show();
                        this.Hide();
                    }
                    else
                    {
                        // Log failed login attempt
                        ConnectDB.saveLog("INSERT INTO `log`(`id`, `log_message`, `log_time`, `log_failures`) VALUES ('" + userID + "', 'Failed login attempt for user: " + login_username.Text + "', NOW(), '1')");

                        MessageBox.Show("Incorrect Username/Password", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }




        private void login_showPass_CheckedChanged(object sender, EventArgs e)
        {
            login_password.PasswordChar = login_showPass.Checked ? '\0' : '*';
        }

        private void exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}

