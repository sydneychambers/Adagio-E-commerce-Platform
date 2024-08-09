using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;


namespace EC1ContinuousAssessment_2005734
{
    public partial class ShopCart : System.Web.UI.Page
    {
        private string connectionString = "Data Source=OMERTA\\SQLEXPRESS;Initial Catalog=AdagioMgmt;Integrated Security=True;";

        protected List<ShoppingCart> sessionCart = new List<ShoppingCart>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                sessionCart = GetShoppingCart();

                cartRepeater.DataSource = sessionCart;
                cartRepeater.DataBind();

                // Populates the dropdown lists with quantity values
                foreach (RepeaterItem item in cartRepeater.Items)
                {
                    if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                    {
                        var ddlQuantity = (DropDownList)item.FindControl("ddlQuantity");
                        ddlQuantity.EnableViewState = true;
                        var quantity = sessionCart[item.ItemIndex].Quantity;

                        for (int i = 1; i <= 10; i++)
                        {
                            ddlQuantity.Items.Add(i.ToString());
                        }

                        // Sets the selected index to the current quantity - 1 (in alignment with zero-based indexing)
                        ddlQuantity.SelectedIndex = quantity - 1;
                    }
                }

                btnClearCart.Visible = false;
                btnCheckout.Visible = false;

                if (Session["Username"] is string username)
                {
                    using (var context = new AdagioDBContext())
                    {
                        var userManager = new UserManager<AdagioUser>(new UserStore<AdagioUser>(context));

                        // Get the user by username
                        var user = userManager.FindByName(username);

                        if (user != null)
                        {
                            string userId = user.Id;

                            if (IsCustomer(userId) && sessionCart.Any())
                            {
                                // User is a customer and there are items in the cart, show the buttons
                                btnClearCart.Visible = true;
                                btnCheckout.Visible = true;
                            }
                            else if (IsAdmin(userId))
                            {
                                // User is an admin, hide the buttons
                                btnClearCart.Visible = false;
                                btnCheckout.Visible = false;
                            }
                            else
                            {
                                // No user is logged in, so hide the buttons
                                btnClearCart.Visible = false;
                                btnCheckout.Visible = false;
                            }
                        }
                    }
                }

            }
        }

        private List<ShoppingCart> GetShoppingCart()
        {
            // Pulls the current username from the session variable
            if (Session["Username"] is string username)
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Queries the database to get shopping cart items associated with the current username
                    using (var command = new SqlCommand(
                        "SELECT * FROM ShoppingCartView WHERE Username = @Username", connection))
                    {
                        command.Parameters.AddWithValue("@Username", username);

                        using (var reader = command.ExecuteReader())
                        {
                            var cartItems = new List<ShoppingCart>();

                            while (reader.Read())
                            {
                                // Creates ShoppingCart objects and adds them to the list
                                var cartItem = new ShoppingCart(
                                    reader["Username"].ToString(),
                                    Convert.ToInt32(reader["ProductID"]),
                                    Convert.ToInt32(reader["Quantity"]),
                                    reader["ProductName"].ToString(),
                                    Convert.ToDecimal(reader["Price"])
                                );

                                cartItems.Add(cartItem);
                            }

                            sessionCart = cartItems;
                        }

                        command.ExecuteReader().Close();

                    }
                }
            }
            else
            {
                // If the username is not available in the session, create an empty list.
                sessionCart = new List<ShoppingCart>();

                // No user is logged in, hide the buttons
                btnClearCart.Visible = false;
                btnCheckout.Visible = false;
            }

            return sessionCart;
        }

        protected void RemoveFromCart_Click(object sender, EventArgs e)
        {
            Button btnRemove = (Button)sender;
            string storedCommandArgument = btnRemove.Attributes["data-commandargument"];
            int productIDToRemove = Convert.ToInt32(storedCommandArgument);

            if (Session["Username"] is string username)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sqlDeleteCommand = "DELETE FROM ShoppingCart WHERE Username = @Username AND ProductID = @ProductID";

                    using (SqlCommand command = new SqlCommand(sqlDeleteCommand, connection))
                    {
                        command.Parameters.AddWithValue("@Username", username);
                        command.Parameters.AddWithValue("@ProductID", productIDToRemove);

                        command.ExecuteNonQuery();
                    }

                    connection.Close();
                }
            }

            Response.Redirect("ShopCart.aspx");
        }


        protected string ShowGrandTotal()
        {
            decimal grandTotal = 0;

            if (sessionCart != null && sessionCart.Any())
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    foreach (var cartItem in sessionCart)
                    {
                        // Gets product information from the Product table in the database
                        string fetchProductQuery = "SELECT Price FROM Product WHERE ProductID = @ProductID";
                        using (SqlCommand fetchProductCommand = new SqlCommand(fetchProductQuery, connection))
                        {
                            fetchProductCommand.Parameters.AddWithValue("@ProductID", cartItem.ProductID);

                            using (SqlDataReader productReader = fetchProductCommand.ExecuteReader())
                            {
                                if (productReader.Read())
                                {
                                    decimal productPrice = (decimal)productReader["Price"];

                                    // Multiply the product price by the quantity and add to the grand total
                                    grandTotal += productPrice * cartItem.Quantity;
                                }

                                productReader.Close();
                            }
                        }
                    }

                    connection.Close();
                }
            }

            return grandTotal.ToString("0.00");
        }

        protected void Checkout_Click(object sender, EventArgs e)
        {
            // Checks if the user is logged in
            if (Session["Username"] is string username)
            {
                // Generates a unique OrderID
                int orderID = GenerateUniqueOrderID();
                Debug.WriteLine($"Generated OrderID: {orderID}");

                // Retrieves the current user's shopping cart items
                sessionCart = GetShoppingCart();
                Debug.WriteLine($"Retrieved {sessionCart.Count} items from the shopping cart");

                if (sessionCart.Count > 0)
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        // Creates a new checkout record for each item in the shopping cart
                        foreach (var cartItem in sessionCart)
                        {
                            decimal subtotal = cartItem.Quantity * cartItem.Price;

                            string insertCheckoutQuery = "INSERT INTO Checkout (OrderID, Username, OrderDateTime, ProductID, Quantity, Subtotal) " +
                                                         "VALUES (@OrderID, @Username, @OrderDateTime, @ProductID, @Quantity, @Subtotal);";

                            using (SqlCommand insertCheckoutCommand = new SqlCommand(insertCheckoutQuery, connection))
                            {
                                insertCheckoutCommand.Parameters.AddWithValue("@OrderID", orderID);
                                insertCheckoutCommand.Parameters.AddWithValue("@Username", username);
                                insertCheckoutCommand.Parameters.AddWithValue("@OrderDateTime", DateTime.Now);
                                insertCheckoutCommand.Parameters.AddWithValue("@ProductID", cartItem.ProductID);
                                insertCheckoutCommand.Parameters.AddWithValue("@Quantity", cartItem.Quantity);
                                insertCheckoutCommand.Parameters.AddWithValue("@Subtotal", subtotal);

                                insertCheckoutCommand.ExecuteNonQuery();
                            }
                        }

                        // Clear the user's shopping cart after successful checkout
                        ClearShoppingCart(username);

                        connection.Close();
                        Response.Redirect("~/ConfirmationPage.aspx");
                    }
                }
                else
                {
                    Response.Redirect("~/ErrorPage.aspx");
                }
            }
            else
            {
                // If no user is not logged in, redirect to the login page
                Response.Redirect("~/Login.aspx");
            }
        }

        private int GenerateUniqueOrderID()
        {
            int orderID;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                do
                {
                    orderID = GenerateRandomOrderID();

                    string checkExistingOrderIDQuery = "SELECT COUNT(*) FROM Checkout WHERE OrderID = @OrderID";

                    using (SqlCommand checkExistingOrderIDCommand = new SqlCommand(checkExistingOrderIDQuery, connection))
                    {
                        checkExistingOrderIDCommand.Parameters.AddWithValue("@OrderID", orderID);

                        int existingOrderCount = (int)checkExistingOrderIDCommand.ExecuteScalar();

                        if (existingOrderCount == 0)
                        {
                            return orderID;
                        }
                    }

                    // Retries until a unique OrderID is generated
                } while (true);

                connection.Close();
            }
        }

        private int GenerateRandomOrderID()
        {
            Random random = new Random();
            return random.Next(10000, 99999); 
        }


        protected void ClearCart_Click(object sender, EventArgs e)
        {
            if (Session["Username"] is string username)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sqlDeleteCommand = "DELETE FROM ShoppingCart WHERE Username = @Username";

                    using (SqlCommand command = new SqlCommand(sqlDeleteCommand, connection))
                    {
                        command.Parameters.AddWithValue("@Username", username);

                        command.ExecuteNonQuery();
                    }

                    connection.Close();
                }
            }

            sessionCart = new List<ShoppingCart>();

            Response.Redirect("ShopCart.aspx");
        }

        protected void UpdateQuantity_Click(object sender, EventArgs e)
        {
            DropDownList ddlQuantity = (DropDownList)sender;
            RepeaterItem item = (RepeaterItem)ddlQuantity.NamingContainer;

            int productID = Convert.ToInt32(ddlQuantity.Attributes["data-commandargument"]);

            // Gets the new quantity value from the dropdown
            int newQuantity = Convert.ToInt32(ddlQuantity.SelectedValue);

            string username = Session["Username"] as string;

            UpdateQuantity(productID, newQuantity, username);

            Response.Redirect("ShopCart.aspx");
        }

        private void UpdateQuantity(int productID, int newQuantity, string username)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // CheckS if the product with the provided ID and username exists in the shopping cart table
                    string checkProductQuery = "SELECT * FROM ShoppingCart WHERE ProductID = @ProductID AND Username = @Username";
                    using (SqlCommand checkProductCommand = new SqlCommand(checkProductQuery, connection))
                    {
                        checkProductCommand.Parameters.AddWithValue("@ProductID", productID);
                        checkProductCommand.Parameters.AddWithValue("@Username", username);

                        using (SqlDataReader productReader = checkProductCommand.ExecuteReader())
                        {
                            if (productReader.Read())
                            {
                                productReader.Close();

                                string updateQuantityQuery = "UPDATE ShoppingCart SET Quantity = @Quantity WHERE ProductID = @ProductID AND Username = @Username";
                                using (SqlCommand updateQuantityCommand = new SqlCommand(updateQuantityQuery, connection))
                                {
                                    updateQuantityCommand.Parameters.AddWithValue("@ProductID", productID);
                                    updateQuantityCommand.Parameters.AddWithValue("@Quantity", newQuantity);
                                    updateQuantityCommand.Parameters.AddWithValue("@Username", username);

                                    updateQuantityCommand.ExecuteNonQuery();
                                }
                            }
                            else
                            {
                                // In case the product was not found in the shopping cart for the specific user
                                throw new Exception($"Product with ID {productID} not found in the shopping cart.");
                            }
                        }
                    }

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating quantity for product ID {productID}: {ex.Message}", ex);
            }
        }

        private bool IsCustomer(string userId)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Check if the user has the "customer" role in AspNetUserRoles table
                using (var command = new SqlCommand("SELECT COUNT(*) FROM AspNetUserRoles WHERE UserId = @UserId AND RoleId = '2'", connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);

                    int count = (int)command.ExecuteScalar();

                    return count > 0;
                }

                connection.Close();
            }
        }

        private bool IsAdmin(string userId)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Checks if the user has the "admin" role in AspNetUserRoles table
                using (var command = new SqlCommand("SELECT COUNT(*) FROM AspNetUserRoles WHERE UserId = @UserId AND RoleId = '1'", connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);

                    int count = (int)command.ExecuteScalar();

                    return count > 0;
                }

                connection.Close();
            }
        }


        private void ClearShoppingCart(string username)
        {
            if (!string.IsNullOrEmpty(username))
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string deleteCartItemsQuery = "DELETE FROM ShoppingCart WHERE Username = @Username";

                    using (var deleteCartItemsCommand = new SqlCommand(deleteCartItemsQuery, connection))
                    {
                        deleteCartItemsCommand.Parameters.AddWithValue("@Username", username);

                        deleteCartItemsCommand.ExecuteNonQuery();
                    }

                    connection.Close();
                }
            }
        }

    }
}