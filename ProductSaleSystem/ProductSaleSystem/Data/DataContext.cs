using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ProductSaleSystem.Data;

namespace ProductSaleSystem.Data
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        

        public DbSet<Product> Products { get; set; }
    }
}
