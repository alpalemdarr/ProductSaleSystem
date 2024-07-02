using Microsoft.EntityFrameworkCore;
using ProductSaleSystem.Data;
using ProductSaleSystem.Models;

namespace ProductSaleSystem.Repositories
{
    public class ProductRepository:IProductRepository
    {
        private readonly DataContext _dataContext;
        public ProductRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public void AddProduct(ProductDTO productDTO)
        {
            Product product = new Product
            {
                ProductName = productDTO.ProductName,
                ProductStock = productDTO.ProductStock,
                ImagePath = productDTO.ImagePath,
                SellingPrice = productDTO.SellingPrice,
                PurchasePrice = productDTO.PurchasePrice,
            };
            _dataContext.Products.Add(product);
            _dataContext.SaveChanges();
        }
        public List<ProductDTO> GetAllProducts()
        {
            var products = _dataContext.Products.ToList();
            var productDTO = products.Select(products => new ProductDTO
            {
                Id=products.Id,
                ProductName = products.ProductName,
                PurchasePrice = products.PurchasePrice,
                SellingPrice = products.SellingPrice,
                ProductStock = products.ProductStock,
                ImagePath = products.ImagePath,

            }).ToList();
            return productDTO;
        }
        public void DeleteProduct(ProductDTO productDTO)
        {
            var product = _dataContext.Products.FirstOrDefault(m => m.Id == productDTO.Id);
            if (product != null)
            {
                _dataContext.Products.Remove(product);
                _dataContext.SaveChanges();
            }
        }
        public void UpdateProduct(ProductDTO productDTO)
        {
            var product = _dataContext.Products.FirstOrDefault(m => m.Id == productDTO.Id);
            if (product != null)
            {
                product.ProductName = productDTO.ProductName;
                product.ProductStock = productDTO.ProductStock;
                product.ImagePath = productDTO.ImagePath;
                product.SellingPrice = productDTO.SellingPrice;
                product.PurchasePrice = productDTO.PurchasePrice;
                _dataContext.SaveChanges();
            }
        }
        
        public ProductDTO GetProductById(int  id)
        {
            var product = _dataContext.Products.FirstOrDefault(b => b.Id == id);

            if (product == null)
            {
                return null; 
            }

            return new ProductDTO
            {
                Id = product.Id,
                ProductName = product.ProductName,
                PurchasePrice = product.PurchasePrice,
                SellingPrice = product.SellingPrice,
                ProductStock = product.ProductStock,
                ImagePath = product.ImagePath,
            };
        }
    }
}
