using System.ComponentModel.DataAnnotations;

namespace ProjectDetails.ViewModels
{
    public class BlogUpdate_VM
    {
        [Required]
        public int BlogID { get; set; }                  
        public int BlogCategoryID { get; set; }
        public int ProductID { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public int StatusID { get; set; } = 0;
        public int CreatedBy { get; set; }               
        public List<IFormFile>? Images { get; set; }
    }
}
