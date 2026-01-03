using System.ComponentModel.DataAnnotations;

namespace ProjectDetails.Models
{
    public class Blogs_Tbl
    {
        [Key]
        public int BlogID { get; set; }
        public int CategoryID { get; set; }
        public int ProductID { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? ImagePath { get; set; }
        public int StatusID { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
    }
}
