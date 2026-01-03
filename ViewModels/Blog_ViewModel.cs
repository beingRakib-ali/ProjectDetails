namespace ProjectDetails.ViewModels
{
    public class Blog_ViewModel
    {
        public int BlogID { get; set; }
        public int CategoryID { get; set; }
        public int ProductID { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }

        public IFormFile? ImageFile { get; set; }   
        public string? ImagePath { get; set; }     

        public int StatusID { get; set; }
        public int CreatedBy { get; set; }
    }



    public class BlogDetails_ViewModel
    {
        public int BlogID { get; set; }
        public int CategoryID { get; set; }
        public int ProductID { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }

        public IFormFile? ImageFile { get; set; }

        public int StatusID { get; set; }
        public int CreatedBy { get; set; }
    }


}
