using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EC1ContinuousAssessment_2005734
{
    public partial class ConfirmationPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void ShopAgain_Click(object sender, EventArgs e)
        {
            Response.Redirect("Products.aspx");
        }

    }
}