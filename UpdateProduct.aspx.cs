using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EC1ContinuousAssessment_2005734
{
    public partial class UpdateProduct : System.Web.UI.Page
    {
        private string connectionString = "Data Source=OMERTA\\SQLEXPRESS;Initial Catalog=AdagioMgmt;Integrated Security=True;";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtProductID.Enabled = false;

                if (Session["SelectedProduct"] != null && Session["SelectedProduct"] is Product product)
                {
                    txtProductID.Text = product.ProductID.ToString();
                    txtProductName.Text = product.ProductName;
                    txtPrice.Text = product.Price.ToString();
                    txtDescription.Text = product.Description;
                    txtImageUrl.Text = product.ImageUrl;
                }
                else
                {
                    Response.Redirect("ErrorPage.aspx");
                }
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
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

                    // Checks if the product with the provided ID exists
                    using (var command = new SqlCommand("SELECT * FROM Product WHERE ProductID = @ProductId", connection))
                    {
                        command.Parameters.AddWithValue("@ProductId", productId);

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                using (var updateCommand = new SqlCommand(
                                    "UPDATE Product SET ProductName = @ProductName, Price = @Price, Description = @Description, ImageUrl = @ImageUrl WHERE ProductID = @ProductId",
                                    connection))
                                {
                                    updateCommand.Parameters.AddWithValue("@ProductName", productName);
                                    updateCommand.Parameters.AddWithValue("@Price", price);
                                    updateCommand.Parameters.AddWithValue("@Description", description);
                                    updateCommand.Parameters.AddWithValue("@ImageUrl", imageUrl);
                                    updateCommand.Parameters.AddWithValue("@ProductId", productId);

                                    reader.Close();

                                    updateCommand.ExecuteNonQuery();

                                    // Redirect to products page to show update
                                    Response.Redirect("Products.aspx");
                                }
                            }
                            else
                            {
                                Response.Redirect("ErrorPage.aspx");
                            }
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