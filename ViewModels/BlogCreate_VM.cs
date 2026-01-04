using System.ComponentModel.DataAnnotations;

namespace ProjectDetails.ViewModels
{
    public class BlogCreate_VM
    {
        [Required]
        public int BlogCategoryID { get; set; }       // Required
        [Required]
        public int ProductID { get; set; }            // Required
        [Required]
        public string Name { get; set; }              // Required
        public string? Description { get; set; }      // Optional
        public int StatusID { get; set; } = 0;
        public int CreatedBy { get; set; }
        [Required]
        public List<IFormFile> Images { get; set; }   // Required: multiple images
    }
}

