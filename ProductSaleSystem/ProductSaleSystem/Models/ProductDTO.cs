namespace ProductSaleSystem.Models
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string ProductName { get; set; }

        public int PurchasePrice {  get; set; }

        public int SellingPrice {  get; set; }
        public int ProductStock { get; set; }
        public IFormFile? FormFile { get; set; }
        public string? ImagePath { get; set; }
    }
}
