using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace EC1ContinuousAssessment_2005734
{
    public partial class Login : System.Web.UI.Page
    {
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string enteredUsername = TextBox1.Text;
            string enteredPassword = TextBox2.Text;

            using (var context = new AdagioDBContext())
            {
                var userManager = new UserManager<AdagioUser>(new UserStore<AdagioUser>(context));

                var user = userManager.FindByName(enteredUsername);

                if (user != null && userManager.CheckPassword(user, enteredPassword))
                {
                    // If logging in was successful
                    Session["Username"] = enteredUsername;
                    Response.Redirect("Account.aspx");
                }
                else
                {
                    // In case of invalid credentials
                    Label3.Text = "Invalid username or password.";
                    Label3.Visible = true;
                }
            }
        }
    }
}
