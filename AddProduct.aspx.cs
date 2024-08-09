using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EC1ContinuousAssessment_2005734
{
    public partial class AddProduct : System.Web.UI.Page
    {
        private string connectionString = "Data Source=OMERTA\\SQLEXPRESS;Initial Catalog=AdagioMgmt;Integrated Security=True;";

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtProductID.Text, out int productId) &&
                decimal.TryParse(txtPrice.Text, out decimal price))
            {
                string productName = txtProductName.Text;
                string description = txtDescription.Text;
                string imageUrl = txtImageUrl.Text;

                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Checking to see if the product with the entered ID already exists in DB
                    using (var command = new SqlCommand("SELECT COUNT(*) FROM Product WHERE ProductID = @ProductId", connection))
                    {
                        command.Parameters.AddWithValue("@ProductId", productId);

                        int productCount = (int)command.ExecuteScalar();

                        if (productCount == 0)
                        {
                            // If product doesn't exist, add a new one
                            using (var insertCommand = new SqlCommand(
                                "INSERT INTO Product (ProductID, ProductName, Price, Description, ImageUrl) " +
                                "VALUES (@ProductId, @ProductName, @Price, @Description, @ImageUrl)", connection))
                            {
                                insertCommand.Parameters.AddWithValue("@ProductId", productId);
                                insertCommand.Parameters.AddWithValue("@ProductName", productName);
                                insertCommand.Parameters.AddWithValue("@Price", price);
                                insertCommand.Parameters.AddWithValue("@Description", description);
                                insertCommand.Parameters.AddWithValue("@ImageUrl", imageUrl);

                                insertCommand.ExecuteNonQuery();

                                // Redirects to the products page to show the new product
                                Response.Redirect("Products.aspx");
                            }
                        }
                        else
                        {
                            DisplayErrorMessage(errorMessageLabel, "Product with the same ID already exists.");
                        }
                    }

                    connection.Close();
                }
            }
            else
            {
                DisplayErrorMessage(errorMessageLabel, "Please enter valid values for Product ID and Price.");
            }
        }

        private void DisplayErrorMessage(Label errorMessageLabel, string message)
        {
            errorMessageLabel.Text = message;
            errorMessageLabel.Visible = true;
        }
    }
}