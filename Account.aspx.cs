using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EC1ContinuousAssessment_2005734
{
    public partial class Account : System.Web.UI.Page
    {
        private string connectionString = "Data Source=OMERTA\\SQLEXPRESS;Initial Catalog=AdagioMgmt;Integrated Security=True;";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Username"] != null)
            {
                string username = Session["Username"].ToString();

                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Retrieves user information from the database
                    using (var command = new SqlCommand("SELECT * FROM Users WHERE Username = @Username", connection))
                    {
                        command.Parameters.AddWithValue("@Username", username);

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                lblUsername.Text = reader["Username"].ToString();
                                DateTime dateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]);
                                lblDateOfBirth.Text = dateOfBirth.ToString("MM/dd/yyyy");
                                lblEmail.Text = reader["Email"].ToString();

                                // Checks if the user is an admin
                                bool isAdmin = IsAdmin(username);

                                // Shows or hides the "View Purchases" and "Manage Users" buttons based on user role
                                btnPurchaseHistory.Visible = !isAdmin;
                                btnManageUsers.Visible = isAdmin;
                            }
                        }
                    }

                    connection.Close();
                }
            }
        }

        private bool IsAdmin(string username)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Checks if the user has the "admin" role in Users table
                using (var command = new SqlCommand("SELECT COUNT(*) FROM Users WHERE Username = @Username AND UserType = 'admin'", connection))
                {
                    command.Parameters.AddWithValue("@Username", username);

                    int count = (int)command.ExecuteScalar();

                    return count > 0;
                }
            }
        }


        protected void SignOut_Click(object sender, EventArgs e)
        {
            // Clears the session variable
            Session["Username"] = null;

            // Then redirects to the login page
            Response.Redirect("Login.aspx");
        }

        protected void PurchaseHistory_Click(object sender, EventArgs e)
        {
            Response.Redirect("PurchaseHistory.aspx");
        }

        protected void ManageUsers_Click(object sender, EventArgs e)
        {
            Response.Redirect("ManageUsers.aspx");
        }
    }

}