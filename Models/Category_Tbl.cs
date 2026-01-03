using System.ComponentModel.DataAnnotations;

namespace ProjectDetails.Models
{
    public class Category_Tbl
    {
        [Key]
        public int CategoryId { get; set; }
        [Required]
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; } = null!;
        public int CategoryTypeID { get; set; }
        public int StatusId { get; set; } = 0;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public int CreatedBy { get; set; }

        public string keyEntry1 { get; set; } = null!;
        public string keyEntry2 { get; set; } = null!;
        public string keyEntry3 { get; set;} = null!;
        public string keyEntry4 { get; set; } = null!;
        public string keyEntry5 { get; set; } = null!;



    }
}
