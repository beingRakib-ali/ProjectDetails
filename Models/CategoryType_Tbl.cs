using System.ComponentModel.DataAnnotations;

namespace ProjectDetails.Models
{
    public class CategoryType_Tbl
    {
        [Key]
        public int categoryTypeID { get; set; }
        public int categoryID { get; set; }
        public string categoryTypeName { get; set; } = null!;
        public string categoryTypeDescription { get; set; } = null;
        public int StatusId { get; set; } = 0;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public int CreatedBy { get; set; }
        public string keyEntry1 { get; set; } = null!;
        public string keyEntry2 { get; set; } = null!;
        public string keyEntry3 { get; set; } = null!;
    }
}
