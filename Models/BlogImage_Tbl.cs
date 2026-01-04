using System.ComponentModel.DataAnnotations;

namespace ProjectDetails.Models
{
    public class BlogImage_Tbl
    {
        [Key]
        public int BlogImageId { get; set; }
        public int BlogId { get; set; }
        public string ImagePath { get; set; }
        public int StatusID { get; set; } = 0;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
