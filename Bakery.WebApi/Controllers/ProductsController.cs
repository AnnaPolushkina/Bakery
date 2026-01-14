using Bakery.Core.Services;
using Bakery.WebApi.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Bakery.WebApi.Controllers
{
    [ApiController]
    [Route("products")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _productService.GetAllAsync();

            var productDtos = products.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                CanBeDeactivated = true

            });


            return Ok(productDtos);
        }

        [HttpPost("{id}/deactivate")]
        public async Task<IActionResult> DeactivateProduct(Guid id)
        {
            try
            {
                await _productService.DeactivateAsync(id);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

    }
}

