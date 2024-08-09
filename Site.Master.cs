using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.SqlClient;

namespace EC1ContinuousAssessment_2005734
{
    public partial class SiteMaster : MasterPage
    {
        private string connectionString = "Data Source=OMERTA\\SQLEXPRESS;Initial Catalog=AdagioMgmt;Integrated Security=True;";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Username"] is string username)
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Finds the user in AspNetUsers table
                    using (var command = new SqlCommand("SELECT Id FROM AspNetUsers WHERE UserName = @Username", connection))
                    {
                        command.Parameters.AddWithValue("@Username", username);

                        string userId = command.ExecuteScalar()?.ToString();

                        if (userId != null)
                        {
                            // Checks if the user is an admin
                            bool isAdmin = IsAdmin(userId);

                            // Sets the link to "Account.aspx"
                            accountLink.NavigateUrl = "~/Account.aspx";

                            // Shows or hides the shopping cart link based on user role
                            shoppingCartLink.Visible = !isAdmin;

                            // Sets the NavigateUrl for shoppingCartLink based on user role
                            if (!isAdmin)
                            {
                                shoppingCartLink.NavigateUrl = "~/ShopCart.aspx";
                            }
                        }
                        else
                        {
                            accountLink.NavigateUrl = "~/Login.aspx";
                            shoppingCartLink.Visible = false;
                        }
                    }
                }
            }
            else
            {
                // User is not logged in; set the link to "Login.aspx"
                accountLink.NavigateUrl = "~/Login.aspx";
                // Hide the shopping cart link
                shoppingCartLink.Visible = false;
            }
        }


        private bool IsAdmin(string userId)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Check if the user has the "admin" role in AspNetUserRoles table
                using (var command = new SqlCommand("SELECT COUNT(*) FROM AspNetUserRoles WHERE UserId = @UserId AND RoleId = '1'", connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);

                    int count = (int)command.ExecuteScalar();

                    return count > 0;
                }

                connection.Close();
            }
        }



    }
}