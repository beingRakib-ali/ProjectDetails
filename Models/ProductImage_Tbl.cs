using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProjectDetails.Models
{
    public class ProductImage_Tbl
    {
        [Key]
        public int ProductImageId { get; set; }
        public int ProductId { get; set; }
        public string ImagePath { get; set; }
        public int StatusID { get; set; } = 0;
        public DateTime CreatedDate { get; set; } = DateTime.Now;

    
    }
}
