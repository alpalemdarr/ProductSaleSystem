using System.ComponentModel.DataAnnotations;

namespace ProductSaleSystem.Data
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string ProductName { get; set; }

        public int PurchasePrice { get; set; }

        public int SellingPrice { get; set; }
        public int ProductStock { get; set; }
        
        public string? ImagePath { get; set; }
    }
}
