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

    }
   
}
