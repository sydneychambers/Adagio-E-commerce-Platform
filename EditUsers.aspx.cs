using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EC1ContinuousAssessment_2005734
{
    public partial class EditUsers : System.Web.UI.Page
    {
        private string connectionString = "Data Source=OMERTA\\SQLEXPRESS;Initial Catalog=AdagioMgmt;Integrated Security=True;";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["SelectedUser"] != null && Session["SelectedUser"] is User user)
            {
                txtUsername.Text = user.Username;
                txtEmail.Text = user.Email;
                txtDateOfBirth.Text = user.DateOfBirth.ToString("yyyy-MM-dd");
                txtUserType.Text = user.UserType;
            }
            else
            {
                Response.Redirect("ErrorPage.aspx");
            }
        }

        protected void UpdateUser_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string email = txtEmail.Text;
            string userType = txtUserType.Text;
            DateTime dateOfBirth = DateTime.Parse(txtDateOfBirth.Text);

            // Redirects to manage users page to show the updated user
            Response.Redirect("ManageUsers.aspx");
        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("ManageUsers.aspx");
        }
        private void DisplayErrorMessage(Label errorMessageLabel, string message)
        {
            errorMessageLabel.Text = message;
            errorMessageLabel.Visible = true;
        }
    }
}