using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectDetails.Models
{
    public class Product_Tbl
    {

      
        [Key]
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public int StatusID { get; set; } = 0;
        public int Stock { get; set; }
        public string? MaterialName { get; set; }
        public string? MaterialCode { get; set; }
        public string? SKU { get; set; }
        public string? Tags { get; set; }
        public int? Review { get; set; }
        public DateTime? ExpireDate { get; set; }
        public int CreatedBy { get; set; }




    }
}
//public List<ProductImage_Tbl> Images { get; set; }
