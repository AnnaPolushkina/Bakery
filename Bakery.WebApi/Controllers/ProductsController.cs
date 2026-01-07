using Bakery.Core.Entities;
using Bakery.WebApi.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Bakery.WebApi.Controllers
{
    [ApiController]
    [Route("products")]
    public class ProductsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetProducts()
        {
            var products = new List<Product>
            {
                new Product("Bread", 2.50m),
                new Product("Croissant", 1.80m),
                new Product("Cake", 15.00m)
            };

            var productDtos = products.Select(p => new ProductDto
            {
                Name = p.Name,
                Price = p.Price
            });

            return Ok(productDtos);
        }
    }
}


