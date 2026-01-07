using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TemplateCode.Models
{
    class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public Product(int id, string productName, decimal productPrice)
        {
            Id = id;
            Name = productName;
            Price = productPrice;
        }

        public void CreateProductData()
        {

        }

        public List<Product> GetAllProducts()
        {
            return new List<Product>();
        }
    }
}
