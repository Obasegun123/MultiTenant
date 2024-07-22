using MultiTenant.Model;
using MultiTenant.Service.ProductService.DTO;

namespace MultiTenant.Service.ProductService
{
    public interface IProductService
    {
        IEnumerable<Product> GetAllProducts();
        Product GetProductById(int id);
        Product CreateProduct(CreateProductRequest request);
        bool DeleteProduct(int id);
    }
}
