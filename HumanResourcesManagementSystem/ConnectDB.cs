using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Org.BouncyCastle.Tls;

namespace HumanResourcesManagementSystem
{
    class ConnectDB
    {
        public static MySqlConnection GetConnection()
        {
            string sql = "datasource=localhost;port=3306;username=root;password=;database=hrms";
            MySqlConnection con = new MySqlConnection(sql);
            try
            {
                con.Open();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("MySql Connecgtion! \n " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return con;
        }



        private void LogFailedLogin(string username)
        {
            string connectionString = "datasource=localhost;port=3306;username=root;password=;database=hrms"; // Modify this with your MySQL connection details
            string query = "INSERT INTO log (id, logIn) VALUES (id, log)";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@login_time", DateTime.Now);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error logging failed login: " + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }









        public static void saveUpdateDeleteData(string sql, string action)
        {
            string query = sql;
            MySqlConnection con = GetConnection();
            MySqlCommand cmd = new MySqlCommand(query, con);
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show(action);
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error!!\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            con.Close();
        }


        public static void saveLog(string sql)
        {
            string query = sql;
            MySqlConnection con = GetConnection();
            MySqlCommand cmd = new MySqlCommand(query, con);
            try
            {
                cmd.ExecuteNonQuery();
                //MessageBox.Show(action);
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error!!\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            con.Close();
        }




        public static void dgvViewing(string sqlQuery, DataGridView dgv)
        {
            string sql = sqlQuery;
            MySqlConnection con = GetConnection();
            MySqlCommand cmd = new MySqlCommand(sql, con);
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
            DataTable tbl = new DataTable();
            adp.Fill(tbl);
            dgv.DataSource = tbl;
            dgv.Columns[0].Visible = false;


            con.Close();

        }

        public static bool GetUserPassword(string username, string password)
        {
            bool checker = false;
            string sql = "SELECT COUNT(*) FROM users WHERE Username=@Username AND Password=@Password";

            using (MySqlConnection con = GetConnection())
            {
              /*  MySqlCommand cmd = new MySqlCommand(sql, con);
                cmd.Parameters.Add(new MySqlParameter("@Username"));//, username.Text));
                cmd.Parameters.Add(new MySqlParameter("@Password"));//, password.Text)); */

                MySqlCommand cmd = new MySqlCommand(sql, con);
                cmd.Parameters.Add(new MySqlParameter("@Username", username));
                cmd.Parameters.Add(new MySqlParameter("@Password", password));

                int count = Convert.ToInt32(cmd.ExecuteScalar());
                if (count > 0)
                {
                    checker = true;
                }
                else
                {
                    MessageBox.Show("LOGIN FAILED");
                }
            }
            return checker;

        }

        public static bool IsEmployeeIdTaken(string employeeId)
        {
            bool isTaken = false;
            string sql = "SELECT COUNT(*) FROM employees WHERE employee_id = @EmployeeId";

            using (MySqlConnection con = GetConnection())
            {
                using (MySqlCommand cmd = new MySqlCommand(sql, con))
                {
                    cmd.Parameters.Add(new MySqlParameter("@EmployeeId", employeeId));


                    int count = Convert.ToInt32(cmd.ExecuteScalar());

                    if (count > 0)
                    {
                        isTaken = true;
                    }
                }
            }

            return isTaken;
        }
        public static bool HasEmployeeAlreadyClockedInOrOut(string employeeID, string timeInOut, string amPm, DateTime date)
        {


            using (MySqlConnection con = GetConnection())
            {

                string sql = "SELECT COUNT(*) FROM `attendance` WHERE `Employee_ID` = @EmployeeID AND `Time_In_Time_Out` = @TimeInOut AND `AM_PM` = @AmPm AND `Date` = @Date";

                using (MySqlCommand cmd = new MySqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@EmployeeID", employeeID);
                    cmd.Parameters.AddWithValue("@TimeInOut", timeInOut);
                    cmd.Parameters.AddWithValue("@AmPm", amPm);
                    cmd.Parameters.AddWithValue("@Date", date.ToString("MM-dd-yyyy"));

                    int count = Convert.ToInt32(cmd.ExecuteScalar());

                    return count > 0;
                }
            }
        }


        public static int GetUserIdByUsername(string username)
        {
            int userId = 1; // Default value if user is not found
            string query = "SELECT id FROM `users` WHERE `Username` = @Username";

            using (MySqlConnection conn = GetConnection())
            {
                using (MySqlCommand command = new MySqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@Username", username);

                    try
                    {
                        conn.Open();
                        object result = command.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            userId = Convert.ToInt32(result);
                        }
                    }
                    catch (Exception ex)
                    {
                        // Handle exceptions (e.g., log, throw, etc.)
                        Console.WriteLine("Error: " + ex.Message);
                    }
                }
            }
            return userId;
        }


        public static Boolean AccountVerification(string username, string password)
        {
            Boolean verify = false;
            string query = "SELECT * FROM users WHERE Username='" + username + "' and Password ='" + password + "'";
            MySqlConnection con = ConnectDB.GetConnection();
            MySqlCommand cmd = new MySqlCommand(query, con);
            try
            {
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    verify = true;
                    instantiate.log.id = Convert.ToInt32(dataReader["id"]);
                }
                dataReader.Close();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error!!\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            con.Close();
            return verify;
        }
    }
}
