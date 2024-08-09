using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EC1ContinuousAssessment_2005734
{
    public partial class PurchaseHistory : System.Web.UI.Page
    {
        private string connectionString = "Data Source=OMERTA\\SQLEXPRESS;Initial Catalog=AdagioMgmt;Integrated Security=True;";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Username"] is string username)
                {
                    // Populate the OrderIDs DropDownList on page load
                    PopulateOrderID(username);

                    // Retrieve user purchase history for the first OrderID by default
                    GetPurchaseHistory(username, GetFirstOrderID(username));
                }
                else
                {
                    Response.Redirect("~/Login.aspx");
                }
            }
        }
        protected void OrderID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Session["Username"] is string username)
            {
                // Retrieve user purchase history for the selected OrderID
                int selectedOrderID = Convert.ToInt32(OrderID.SelectedValue);
                GetPurchaseHistory(username, selectedOrderID);
            }
        }

        protected void Return_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Account.aspx");
        }

        private void PopulateOrderID(string username)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string orderIDsQuery = "SELECT DISTINCT OrderID FROM Checkout WHERE Username = @Username";

                using (var command = new SqlCommand(orderIDsQuery, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);

                    using (var reader = command.ExecuteReader())
                    {
                        OrderID.DataSource = reader;
                        OrderID.DataTextField = "OrderID";
                        OrderID.DataValueField = "OrderID";
                        OrderID.DataBind();
                    }
                }

                connection.Close();
            }
        }

        private int GetFirstOrderID(string username)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string firstOrderIDQuery = "SELECT TOP 1 OrderID FROM Checkout WHERE Username = @Username";

                using (var command = new SqlCommand(firstOrderIDQuery, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    return Convert.ToInt32(command.ExecuteScalar());
                }

                connection.Close();
            }
        }

        private void GetPurchaseHistory(string username, int orderID)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string purchaseHistoryQuery = "SELECT OrderID, OrderDateTime, ProductName, Quantity, Subtotal " +
                                              "FROM CheckoutView " +
                                              "WHERE Username = @Username AND OrderID = @OrderID";

                using (var command = new SqlCommand(purchaseHistoryQuery, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@OrderID", orderID);

                    using (var reader = command.ExecuteReader())
                    {
                        purchaseHistoryRepeater.DataSource = reader;
                        purchaseHistoryRepeater.DataBind();
                    }
                }

                connection.Close();
            }
        }

        protected bool HasPurchaseHistory
        {
            get { return purchaseHistoryRepeater.Items.Count > 0; }
        }
    }
}