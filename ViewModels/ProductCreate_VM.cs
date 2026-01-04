using System.ComponentModel.DataAnnotations;

namespace ProjectDetails.ViewModels
{
    public class ProductCreate_VM
    {

        public int ProductId { get; set; }
        public int CategoryID { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string? MaterialName { get; set; }
        public string? MaterialCode { get; set; }
        public string? SKU { get; set; }
        public string? Tags { get; set; }
        //public int? Review { get; set; }
        public DateTime? ExpireDate { get; set; }
        public int CreatedBy { get; set; }
        //public List<string> ImageUrls { get; set; }
        //public string ProductName { get; set; }
        //public string Description { get; set; }
        //public decimal Price { get; set; }
        //public int Stock { get; set; }
        //public DateTime CreatedDate { get; set; } = DateTime.Now;

        public List<IFormFile> Images { get; set; }
    }
}
