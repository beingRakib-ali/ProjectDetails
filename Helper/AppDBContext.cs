using Microsoft.EntityFrameworkCore;
using ProjectDetails.Models;

namespace ProjectDetails.Helper
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
        }

        public DbSet<paymentDetails> paymentDetails { get; set; }
        public DbSet<Category_Tbl> Category_Tbl { get; set; }

        public DbSet<Product_Tbl> Product_Tbl { get; set; }
        public DbSet<ProductImage_Tbl> ProductImage_Tbl { get; set; }
        public DbSet<Order_Tbl> Order_Tbl { get; set; }
        public DbSet<Blogs_Tbl> Blogs_Tbl { get; set; }

    }

}
