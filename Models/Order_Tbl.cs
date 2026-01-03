using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectDetails.Models
{
    public class Order_Tbl
    {
        [Key]
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public string Name { get; set; }
        public string? Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public string? ZipCode { get; set; }
        public DateTime OrderDate { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public int OrderStatus { get; set; }
        //public int ProductStatus { get; set; }
        public int StatusID { get; set; } 
        public int CreatedBy { get; set; }

    }
}
