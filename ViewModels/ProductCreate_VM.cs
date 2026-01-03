using System.ComponentModel.DataAnnotations;

namespace ProjectDetails.ViewModels
{
    public class ProductCreate_VM
    {

        //[Required]
        //public string ProductName { get; set; }

        //[Required]
        //public string Description { get; set; }

        //public decimal Price { get; set; }
        //public int Stock { get; set; }

        //public List<IFormFile> Images { get; set; }


        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public List<IFormFile> Images { get; set; }
    }
}
