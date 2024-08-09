using System;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.Net.Mail;

namespace EC1ContinuousAssessment_2005734
{
    public partial class Registration : System.Web.UI.Page
    {
        private string connectionString = "Data Source=OMERTA\\SQLEXPRESS;Initial Catalog=AdagioMgmt;Integrated Security=True;";

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Register_Click(object sender, EventArgs e)
        {
            Label6.Text = "Registration complete!";
            Label6.Visible = true;

            using (var context = new AdagioDBContext())
            {
                string username = TextBox1.Text;

                // Checks password format
                if (!Regex.IsMatch(TextBox2.Text, "^(?=.*[A-Z])(?=.*\\d).*$"))
                {
                    // If password format is not valid
                    Label6.Text = "Password must contain at least one capital letter and one number.";
                    Label6.Visible = true;
                    return;
                }

                // Checks if the username already exists
                var existingUser = context.Users.FirstOrDefault(u => u.UserName == username);

                if (existingUser != null)
                {
                    // If username already exists in database, show an error message
                    Label6.Text = "Username already exists. Please choose a different username.";
                    Label6.ForeColor = System.Drawing.Color.Red;
                    Label6.Visible = true;
                    return;
                }

                // Validates the date of birth
                if (!DateTime.TryParse(TextBox4.Text, out DateTime dateOfBirth))
                {
                    // If the date format is invalid, show an error message
                    Label6.Text = "Invalid date format. Please use the format 'MM/dd/yyyy'.";
                    Label6.ForeColor = System.Drawing.Color.Red;
                    Label6.Visible = true;
                    return;
                }

                int age = DateTime.Now.Year - dateOfBirth.Year;
                if (dateOfBirth > DateTime.Now.AddYears(-age)) age--;

                if (age < 18)
                {
                    // If the user is under 18, show an error message
                    Label6.Text = "You must be at least 18 years old to register.";
                    Label6.ForeColor = System.Drawing.Color.Red;
                    Label6.Visible = true;
                    return;
                }

                // Checks if the email already exists
                var existingEmail = context.Users.FirstOrDefault(u => u.Email == TextBox5.Text);

                if (existingEmail != null)
                {
                    // If that email already exists, show an error message
                    Label6.Text = "An account using this email already exists. Please use a different email address.";
                    Label6.ForeColor = System.Drawing.Color.Red;
                    Label6.Visible = true;
                    return;
                }

                if (!IsValidEmail(TextBox5.Text))
                {
                    // If the email format is invalid, show an error message
                    Label6.Text = "Invalid email format.";
                    Label6.ForeColor = System.Drawing.Color.Red;
                    Label6.Visible = true;
                    return;
                }

                var userManager = new UserManager<AdagioUser>(new UserStore<AdagioUser>(context));

                var user = new AdagioUser
                {
                    UserName = username,
                    Email = TextBox5.Text
                };

                IdentityResult result = userManager.Create(user, TextBox2.Text);

                if (result.Succeeded)
                {
                    Session["Username"] = user.UserName;

                    // Assign 'customer' role to the new user after registration
                    userManager.AddToRole(user.Id, "customer");
                    using (var connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        try
                        {
                            using (var insertCommand = new SqlCommand("INSERT INTO Users (Username, Email, DateOfBirth, userType) " +
                                "VALUES (@Username, @Email, @DateOfBirth, @userType)", connection))
                            {
                                insertCommand.Parameters.AddWithValue("@Username", username);
                                insertCommand.Parameters.AddWithValue("@Email", TextBox5.Text);
                                insertCommand.Parameters.AddWithValue("@DateOfBirth", dateOfBirth);
                                insertCommand.Parameters.AddWithValue("@userType", "customer");

                                insertCommand.ExecuteNonQuery();
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error inserting data into Users table: {ex.Message}");
                        }

                        connection.Close();

                        Response.Redirect("Account.aspx");
                    }
                }
                else
                {
                    // If registration fails, display error message
                    Label6.Text = string.Join("<br/>", result.Errors);
                    Label6.ForeColor = System.Drawing.Color.Red;
                    Label6.Visible = true;
                }
            }
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
