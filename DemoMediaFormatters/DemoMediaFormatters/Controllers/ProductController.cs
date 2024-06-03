using DemoMediaFormatters.Models;
using Microsoft.AspNetCore.Mvc;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;
namespace DemoMediaFormatters.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Produces("application/xml")]
    //[Consumes("application/json")]
    public class ProductController : Controller
    {
        private static readonly List<Product> Products = new List<Product>() {
            new Product(){ProductId = 1, ProductCategory = "Toy", ProductName = "Product 1", ProductPrice = 10000},
            new Product(){ProductId = 2, ProductCategory = "Phone", ProductName = "Product 2", ProductPrice = 20000},
            new Product(){ProductId = 3, ProductCategory = "Electric device", ProductName = "Product 3", ProductPrice = 103000},
            new Product(){ProductId = 4, ProductCategory = "Fashion", ProductName = "Product 4", ProductPrice = 100040},
            new Product(){ProductId = 5, ProductCategory = "Food", ProductName = "Product 5", ProductPrice = 50000},
            new Product(){ProductId = 6, ProductCategory = "Vegetable", ProductName = "Product 6", ProductPrice = 14000},
            new Product(){ProductId = 7, ProductCategory = "Comics", ProductName = "Product 7", ProductPrice = 21000},
            new Product(){ProductId = 8, ProductCategory = "Games", ProductName = "Product 8", ProductPrice = 56000},
            new Product(){ProductId = 9, ProductCategory = "Animals", ProductName = "Product 9", ProductPrice = 121000},
            new Product(){ProductId = 10, ProductCategory = "Furnitures", ProductName = "Product 10", ProductPrice = 1130000},
        };


        [HttpGet("GetAllProducts")]

        public ActionResult<IEnumerable<Product>> GetAllProducts()
        {
            try
            {
                return  Ok(Products);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpPost("AddProduct")]

        public ActionResult<string> AddProduct(Product product)
        {
            try
            {
                Products.Add(product);
                return Ok(Products);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
