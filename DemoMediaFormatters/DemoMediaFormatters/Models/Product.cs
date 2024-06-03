using System.Text.Json.Serialization;

namespace DemoMediaFormatters.Models {
    public class Product {
        public Product(int id, string name, string category, decimal price)
        {
            ProductId = id;
            ProductName = name;
            ProductCategory = category;
            ProductPrice = price;
        }

        public Product()
        {

        }

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductCategory { get; set; }
        [JsonIgnore]
        public decimal ProductPrice { get; set; }
    }
}

