using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace EC1ContinuousAssessment_2005734
{
    public partial class Products : System.Web.UI.Page
    {
        private string connectionString = "Data Source=OMERTA\\SQLEXPRESS;Initial Catalog=AdagioMgmt;Integrated Security=True;";
        protected List<Product> products = new List<Product>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Gets products from the database
                    string fetchProductsQuery = "SELECT * FROM Product";
                    using (SqlCommand fetchProductsCommand = new SqlCommand(fetchProductsQuery, connection))
                    {
                        using (SqlDataReader reader = fetchProductsCommand.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Product product = new Product(
                                    (int)reader["ProductID"],
                                    reader["ProductName"].ToString(),
                                    (decimal)reader["Price"],
                                    reader["Description"].ToString(),
                                    reader["ImageUrl"].ToString()
                                );

                                products.Add(product);
                            }
                        }

                        productRepeater.DataSource = products;
                        productRepeater.DataBind();

                        // Populates the dropdown lists with quantity values
                        foreach (RepeaterItem item in productRepeater.Items)
                        {
                            if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                            {
                                DropDownList ddlQuantity = (DropDownList)item.FindControl("ddlQuantity");

                                for (int i = 1; i <= 10; i++)
                                {
                                    ddlQuantity.Items.Add(new ListItem(i.ToString(), i.ToString()));
                                }
                            }
                        }

                        if (Session["Username"] != null)
                        {
                            string username = Session["Username"].ToString();
                            
                            // Checks if the user is an admin
                            using (var command = new SqlCommand("SELECT UserType FROM Users WHERE Username = @Username", connection))
                            {
                                command.Parameters.AddWithValue("@Username", username);

                                var userType = command.ExecuteScalar() as string;

                                if (userType != null && userType == "admin")
                                {
                                    // User is an admin, make the button visible
                                    btnAddProduct.Visible = true;
                                }
                                else if (userType != null && userType == "customer")
                                {
                                    // User is a customer, hide the button
                                    btnAddProduct.Visible = false;
                                }
                            }
                        }
                        else
                        {
                            btnAddProduct.Visible = false;
                        }

                    }

                    connection.Close();
                }
            }
        }

        protected void productRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Button btnUpdateProduct = (Button)e.Item.FindControl("btnUpdateProduct");
                Button btnDeleteProduct = (Button)e.Item.FindControl("btnDeleteProduct");
                Button btnAddToCart = (Button)e.Item.FindControl("btnAddToCart");
                DropDownList ddlQuantity = (DropDownList)e.Item.FindControl("ddlQuantity");

                if (btnUpdateProduct != null && btnDeleteProduct != null)
                {
                    if (Session["Username"] != null)
                    {
                        string username = Session["Username"].ToString();

                        using (var connection = new SqlConnection(connectionString))
                        {
                            connection.Open();

                            // Checks if the user is an admin
                            using (var command = new SqlCommand("SELECT UserType FROM Users WHERE Username = @Username", connection))
                            {
                                command.Parameters.AddWithValue("@Username", username);

                                var userType = command.ExecuteScalar() as string;

                                if (userType != null && userType == "admin")
                                {
                                    // User is an admin, make the buttons visible
                                    btnUpdateProduct.Visible = true;
                                    btnDeleteProduct.Visible = true;
                                    btnAddToCart.Visible = false;
                                    ddlQuantity.Visible = false;
                                }
                                else if (userType != null && userType == "customer")
                                {
                                    // User is a customer, hide the buttons
                                    btnUpdateProduct.Visible = false;
                                    btnDeleteProduct.Visible = false;
                                    btnAddToCart.Visible = true;
                                    ddlQuantity.Visible = true;
                                }
                            }

                            connection.Close();
                        }
                    }
                    else
                    {
                        // Buttons are disabled by default when there is no user login
                        btnUpdateProduct.Visible = false;
                        btnDeleteProduct.Visible = false;
                        btnAddToCart.Visible = true;
                        ddlQuantity.Visible = true;
                    }
                }
            }
        }

        protected void AddToCart_Click(object sender, EventArgs e)
        {
            Button btnAddToCart = (Button)sender;
            RepeaterItem item = (RepeaterItem)btnAddToCart.NamingContainer;
            Label errorMessageLabel = (Label)item.FindControl("errorMessageLabel");
            Label productIDLabel = (Label)item.FindControl("productIDLabel");
            int productid = Convert.ToInt32(productIDLabel.Text);
            DropDownList ddlQuantity = (DropDownList)item.FindControl("ddlQuantity");
            string username;

            if (Session["Username"] == null)
            {
                DisplayErrorMessage(errorMessageLabel, "You must be logged in to add products to the cart");
                return;
            }
            else
            {
                // Gets the username from the session variable
                string sessionUsername = Session["Username"] as string;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Searches the database for the username
                    string searchUsernameQuery = "SELECT UserName FROM AspNetUsers WHERE UserName = @Username";
                    using (SqlCommand searchUsernameCommand = new SqlCommand(searchUsernameQuery, connection))
                    {
                        searchUsernameCommand.Parameters.AddWithValue("@Username", sessionUsername);

                        using (SqlDataReader reader = searchUsernameCommand.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                username = reader["UserName"].ToString();
                            }
                            else
                            {
                                DisplayErrorMessage(errorMessageLabel, "User does not exist.");
                                return;
                            }
                        }
                    }
                }
            }

            if (ddlQuantity != null)
            {
                string storedCommandArgument = ddlQuantity.Attributes["data-commandargument"];
                if (!string.IsNullOrEmpty(storedCommandArgument) && storedCommandArgument.Equals("quantity_" + productid))
                {
                    if (int.TryParse(ddlQuantity.SelectedValue, out int quantity))
                    {
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            connection.Open();

                            string checkExistingProductQuery = "SELECT Quantity FROM ShoppingCart WHERE Username = @Username AND ProductID = @ProductID";

                            using (SqlCommand checkExistingProductCommand = new SqlCommand(checkExistingProductQuery, connection))
                            {
                                checkExistingProductCommand.Parameters.AddWithValue("@Username", username);
                                checkExistingProductCommand.Parameters.AddWithValue("@ProductID", productid);

                                using (SqlDataReader existingProductReader = checkExistingProductCommand.ExecuteReader())
                                {
                                    if (existingProductReader.Read())
                                    {
                                        // If product already exists in the cart, update the quantity
                                        int existingQuantity = (int)existingProductReader["Quantity"];
                                        quantity += existingQuantity;

                                        // Updates the quantity in the ShoppingCart table
                                        string updateQuantityQuery = "UPDATE ShoppingCart SET Quantity = @Quantity WHERE Username = @Username AND ProductID = @ProductID";

                                        using (SqlCommand updateQuantityCommand = new SqlCommand(updateQuantityQuery, connection))
                                        {
                                            updateQuantityCommand.Parameters.AddWithValue("@Username", username);
                                            updateQuantityCommand.Parameters.AddWithValue("@ProductID", productid);
                                            updateQuantityCommand.Parameters.AddWithValue("@Quantity", quantity);

                                            updateQuantityCommand.ExecuteNonQuery(); // Save changes to DB
                                        }
                                    }
                                    else
                                    {
                                        existingProductReader.Close();

                                        // If product does not exist in the cart, insert a new record
                                        string insertProductQuery = "INSERT INTO ShoppingCart (Username, ProductID, Quantity) VALUES (@Username, @ProductID, @Quantity)";

                                        using (SqlCommand insertProductCommand = new SqlCommand(insertProductQuery, connection))
                                        {
                                            insertProductCommand.Parameters.AddWithValue("@Username", username);
                                            insertProductCommand.Parameters.AddWithValue("@ProductID", productid);
                                            insertProductCommand.Parameters.AddWithValue("@Quantity", quantity);
                                            insertProductCommand.ExecuteNonQuery();
                                        }
                                    }
                                }
                            }

                            connection.Close();

                            // Display a success message
                            errorMessageLabel.Text = "Added to Cart!";
                            errorMessageLabel.ForeColor = System.Drawing.Color.Green;
                        }

                    }
                    else
                    {
                        DisplayErrorMessage(errorMessageLabel, "Quantity must be a valid number.");
                    }
                }
                else
                {
                    DisplayErrorMessage(errorMessageLabel, "Please provide a quantity.");
                }
            }
        }

        protected void btnAddProductToDB_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddProduct.aspx");
        }

        protected void btnUpdateProduct_Click(object sender, EventArgs e)
        {
            if (sender is Button btn)
            {
                if (btn.CommandName == "UpdateProduct")
                {
                    // Retrieves the product ID from the CommandArgument
                    if (int.TryParse(btn.CommandArgument, out int productID))
                    {
                        // Gets the product from the database using the productID
                        Product product = GetProductFromDatabase(productID);

                        Session["SelectedProduct"] = product;

                        Response.Redirect("UpdateProduct.aspx");
                    }
                }
            }
        }

        protected void btnDeleteProduct_Click(object sender, EventArgs e)
        {
            Button btnDeleteProduct = (Button)sender;
            RepeaterItem item = (RepeaterItem)btnDeleteProduct.NamingContainer;
            Label errorMessageLabel = (Label)item.FindControl("errorMessageLabel");

            if (sender is Button btn)
            {
                if (btn.CommandName == "DeleteProduct")
                {
                    string confirmMessage = "Are you sure you want to delete this product?";

                    // Sets the onclick attribute of the button to bring up the confirmation prompt
                    btn.OnClientClick = "return confirm('" + confirmMessage + "');";

                    // Gets the product ID from the CommandArgument
                    int productID;
                    if (int.TryParse(btn.CommandArgument, out productID))
                    {
                        // Deletes the product by ID
                        bool prodDelete = DeleteProductFromDatabase(productID);

                        if (prodDelete)
                        {
                            errorMessageLabel.Text = "Product deleted successfully.";
                            Response.Redirect("Products.aspx");
                        }
                        else
                        {
                            errorMessageLabel.Text = "Failed to delete the product. Please try again.";
                        }
                    }
                    else
                    {
                        // If CommandArgument doesn't contain a valid product ID...
                        errorMessageLabel.Text = "Invalid product ID.";
                    }
                }
            }
        }

        private bool DeleteProductFromDatabase(int productId)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Checks if the product with the provided ID exists
                    using (var command = new SqlCommand("SELECT * FROM Product WHERE ProductID = @ProductId", connection))
                    {
                        command.Parameters.AddWithValue("@ProductId", productId);

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                using (var deleteCommand = new SqlCommand(
                                    "DELETE FROM Product WHERE ProductID = @ProductId", connection))
                                {
                                    deleteCommand.Parameters.AddWithValue("@ProductId", productId);
                                    deleteCommand.ExecuteNonQuery(); 

                                    return true; // Returns true if product was deleted successfully
                                }
                            }
                            else
                            {
                                return false; // Returns false when the product with the given ID was not found in DB
                            }
                        }
                    }

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private Product GetProductFromDatabase(int productID)
        {
            Product product = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM Product WHERE ProductID = @ProductID", connection))
                {
                    command.Parameters.AddWithValue("@ProductID", productID);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            product = new Product(
                            (int)reader["ProductID"],
                            reader["ProductName"].ToString(),
                            (decimal)reader["Price"],
                            reader["Description"].ToString(),
                            reader["ImageUrl"].ToString()
                            );
                        }
                    }
                }

                connection.Close();
            }

            return product;
        }

        private void DisplayErrorMessage(Label errorMessageLabel, string message)
        {
            errorMessageLabel.Text = message;
            errorMessageLabel.Visible = true;
        }

    }
}