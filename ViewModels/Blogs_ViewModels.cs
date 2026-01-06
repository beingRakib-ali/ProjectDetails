namespace ProjectDetails.ViewModels
{
    public class Blogs_ViewModels
    {
        public int BlogID { get; set; }
        public int BlogCategoryID { get; set; }
        public int CategoryID { get; set; }
        public int ProductID { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }
        public List<string> Images { get; set; }
        public string? Tags { get; set; }

        //public IFormFile? ImageFile { get; set; }   
        //public string? ImagePath { get; set; }     

        //public int StatusID { get; set; }
        public DateTime CreatedDate { get; set; }
    }


    //public class Blogs_ViewModels
    //{
    //    public int BlogCategoryID { get; set; }
    //    public int ProductID { get; set; }
    //    public string Name { get; set; }
    //    public string? Description { get; set; }
    //    public int StatusID { get; set; }
    //    public int CreatedBy { get; set; }
    //    public List<IFormFile> Images { get; set; }
    //}



}
