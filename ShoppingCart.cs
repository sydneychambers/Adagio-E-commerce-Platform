using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EC1ContinuousAssessment_2005734
{
    public class ShoppingCart
    {
        public string Username { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }

        public ShoppingCart(string username, int productID, int quantity, string productName, decimal price)
        {
            Username = username;
            ProductID = productID;
            Quantity = quantity;
            ProductName = productName;
            Price = price;
        }
    }
}