using Microsoft.AspNetCore.Mvc;
using MultiTenant.Service.ProductService;
using MultiTenant.Service.ProductService.DTO;

namespace MultiTenant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService; // inject the products service
        }
        [HttpGet]
        public IActionResult Get()
        {
            var list = _productService.GetAllProducts();
            return Ok(list);
        }

        // Create a new product
        [HttpPost]
        public IActionResult Post(CreateProductRequest request)
        {
            var result = _productService.CreateProduct(request);
            return Ok(result);
        }

        // Delete a product by id
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _productService.DeleteProduct(id);
            return Ok(result);
        }

    }
}
