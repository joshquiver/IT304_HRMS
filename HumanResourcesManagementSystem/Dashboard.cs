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
    public partial class Dashboard : UserControl
    {
        MySqlConnection connect = ConnectDB.GetConnection();

        public Dashboard()
        {
            InitializeComponent();

            displayTE();
            displayAE();
            displayIE();
        }

        public void RefreshData()
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)RefreshData);
                return;
            }

            displayTE();
            displayAE();
            displayIE();
        }
        public class TotalEmployees
        {
            public static int GetTotalEmployees()
            {
                int count = 0;
                string selectData = "SELECT COUNT(id) FROM employees WHERE delete_date IS NULL";

                using (MySqlConnection con = ConnectDB.GetConnection())
                {
                    try
                    {
                        using (MySqlCommand cmd = new MySqlCommand(selectData, con))
                        {
                            count = Convert.ToInt32(cmd.ExecuteScalar());
                        }
                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show("Error: " + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                return count;
            }
        }
        public void displayTE()
        {
            int totalEmployees = TotalEmployees.GetTotalEmployees();
            dashboard_TE.Text = totalEmployees.ToString();
        }
        public class ActiveEmployees
        {
            public static int GetTotalEmployees()
            {
                int count = 0;
                string selectData = "SELECT COUNT(id) FROM employees WHERE status = 'Active' AND delete_date IS NULL";

                using (MySqlConnection con = ConnectDB.GetConnection())
                {
                    try
                    {
                        using (MySqlCommand cmd = new MySqlCommand(selectData, con))
                        {
                            count = Convert.ToInt32(cmd.ExecuteScalar());
                        }
                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show("Error: " + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                return count;
            }
        }
        public void displayAE()
        {
            int activeEmployees = ActiveEmployees.GetTotalEmployees();
            dashboard_AE.Text = activeEmployees.ToString();
        }
        public class InactiveEmployees
        {
            public static int GetTotalEmployees()
            {
                int count = 0;
                string selectData = "SELECT COUNT(id) FROM employees WHERE status = 'Inactive' AND delete_date IS NULL";

                using (MySqlConnection con = ConnectDB.GetConnection())
                {
                    try
                    {
                        using (MySqlCommand cmd = new MySqlCommand(selectData, con))
                        {
                            count = Convert.ToInt32(cmd.ExecuteScalar());
                        }
                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show("Error: " + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                return count;
            }
        }
        public void displayIE()
        {
            int inactiveEmployees = InactiveEmployees.GetTotalEmployees();
            dashboard_IE.Text = inactiveEmployees.ToString();
        }

    }
}

