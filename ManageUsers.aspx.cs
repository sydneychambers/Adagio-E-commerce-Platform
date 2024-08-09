using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EC1ContinuousAssessment_2005734
{
    public partial class ManageUsers : System.Web.UI.Page
    {
        private string connectionString = "Data Source=OMERTA\\SQLEXPRESS;Initial Catalog=AdagioMgmt;Integrated Security=True;";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulateUsers();
            }
        }

        protected void Return_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Account.aspx");
        }

        private void PopulateUsers()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string usersQuery = "SELECT Username, Email, DateOfBirth, UserType FROM Users WHERE UserType = 'customer'";

                using (var command = new SqlCommand(usersQuery, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        userRepeater.DataSource = reader;
                        userRepeater.DataBind();
                    }
                }

                connection.Close();
            }
        }


        protected void EditUsers_Click(object sender, EventArgs e)
        {
            Button btnEditUsers = (Button)sender;
            string usernameToEdit = btnEditUsers.CommandArgument;

            Session["SelectedUser"] = GetUser(usernameToEdit);

            Response.Redirect("EditUsers.aspx");
        }

        private User GetUser(string username)
        {
            User user = null;

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string userQuery = "SELECT * FROM Users WHERE Username = @Username";

                using (var command = new SqlCommand(userQuery, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            user = new User(
                                reader["Username"].ToString(),
                                reader["Email"].ToString(),
                                Convert.ToDateTime(reader["DateOfBirth"]),
                                reader["UserType"].ToString()
                            );
                        }
                    }
                }

                connection.Close();

                return user;
            }
        }


        protected bool HasUsers
        {
            get { return userRepeater.Items.Count > 0; }
        }
    }
}
