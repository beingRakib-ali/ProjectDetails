using System.ComponentModel.DataAnnotations;

namespace ProjectDetails.Models
{
    public class Blogs_Tbl
    {
        [Key]
        public int BlogID { get; set; }                // Identity column, auto-generated
        public int BlogCategoryID { get; set; }       // Must be sent in POST
        public int ProductID { get; set; }            // Must be sent in POST
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }
        public int StatusID { get; set; } = 0;
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
