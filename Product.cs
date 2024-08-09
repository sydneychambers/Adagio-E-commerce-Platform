using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EC1ContinuousAssessment_2005734
{
    public class Product
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }

        public Product(int productId, string productName, decimal price, string description, string imageUrl)
        {
            ProductID = productId;
            ProductName = productName;
            Price = price;
            Description = description;
            ImageUrl = imageUrl;
        }
    }
}