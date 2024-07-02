using ProductSaleSystem.Models;

namespace ProductSaleSystem.Repositories
{
    public interface IProductRepository
    {
        void AddProduct(ProductDTO productDTO);
        void UpdateProduct(ProductDTO productDTO);
        void DeleteProduct(ProductDTO productDTO);  
        List<ProductDTO> GetAllProducts();
        ProductDTO GetProductById(int id);
    }
}
