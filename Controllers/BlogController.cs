using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectDetails.Helper;
using ProjectDetails.Models;
using ProjectDetails.Services;
using ProjectDetails.ViewModels;

namespace ProjectDetails.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly AppDBContext _context;
        private readonly BlogService _blogService;
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;
        public BlogController(AppDBContext context,BlogService blogService,IMapper mapper, IWebHostEnvironment env) 
        {
            _context = context;
            _blogService = blogService;
            _mapper = mapper;
            _env = env;

        }


        [HttpGet("GetAll_Blogs")]
        public async Task<IActionResult> Get()
        {
            var data = await _blogService.GetAllBlogs();
            return Ok(data);
        }


        [HttpGet("GetBlogById")]
        public async Task<IActionResult> GetBlogById(int BlogID)
        {
            var data = await _blogService.GetBlogById(BlogID);
            return Ok(data);
        }



        //[HttpPost("CreateBlog")]
        //public async Task<IActionResult> CreateBlog([FromForm] Blog_ViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    var result = await _blogService.CreateBlog(model);
        //    return Ok(result);
        //}

        //[HttpPost("CreateBlog")]
        //public async Task<IActionResult> CreateBlog([FromForm] Blog_ViewModel model)
        //{
        //    var result = await _blogService.CreateBlog(model);
        //    return Ok(result);
        //}

        [HttpPost("CreateBlog")]
        public async Task<IActionResult> CreateBlog([FromForm] BlogCreate_VM vm)
        {
            var blog = new Blogs_Tbl
            {
                BlogCategoryID = vm.BlogCategoryID,
                ProductID = vm.ProductID,
                Name = vm.Name,
                Description = vm.Description,
                StatusID = vm.StatusID,
                CreatedBy = vm.CreatedBy,
                CreatedDate = DateTime.Now
            };

            try
            {
                _context.Blogs_Tbl.Add(blog);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException?.Message ?? ex.Message);
            } // BlogID auto-generated

            string folder = Path.Combine(_env.WebRootPath, "uploads", "BlogImages");
            Directory.CreateDirectory(folder);

            List<string> imageList = new();

            foreach (var img in vm.Images)
            {
                string fileName = Guid.NewGuid() + Path.GetExtension(img.FileName);
                string filePath = Path.Combine(folder, fileName);

                using var stream = new FileStream(filePath, FileMode.Create);
                await img.CopyToAsync(stream);

                string dbPath = "/uploads/BlogImages/" + fileName;

                _context.BlogImage_Tbl.Add(new BlogImage_Tbl
                {
                    BlogId = blog.BlogID,
                    ImagePath = dbPath
                });

                imageList.Add(dbPath);
            }

            await _context.SaveChangesAsync();

            return Ok(new
            {
                Message = "Blog created successfully",
                blog.BlogID,
                blog.Name,
                Images = imageList
            });
        }



        [HttpPut("UpdateBlog")]
        public async Task<IActionResult> UpdateBlog([FromForm] BlogUpdate_VM vm)
        {
            // 1️⃣ Find existing blog
            var blog = await _context.Blogs_Tbl.FindAsync(vm.BlogID);
            if (blog == null)
                return NotFound("Blog not found");

            // 2️⃣ Update basic details
            blog.BlogCategoryID = vm.BlogCategoryID;
            blog.ProductID = vm.ProductID;
            blog.Name = vm.Name;
            blog.Description = vm.Description;
            blog.StatusID = vm.StatusID;
            blog.CreatedBy = vm.CreatedBy;
            blog.CreatedDate = DateTime.Now;

            // 3️⃣ Handle new images if any
            List<string> newImages = new List<string>();
            if (vm.Images != null && vm.Images.Count > 0)
            {
                string folder = Path.Combine(_env.WebRootPath, "uploads", "BlogImages");
                Directory.CreateDirectory(folder);

                foreach (var img in vm.Images)
                {
                    string fileName = Guid.NewGuid() + Path.GetExtension(img.FileName);
                    string filePath = Path.Combine(folder, fileName);

                    using var stream = new FileStream(filePath, FileMode.Create);
                    await img.CopyToAsync(stream);

                    string dbPath = "/uploads/BlogImages/" + fileName;

                    _context.BlogImage_Tbl.Add(new BlogImage_Tbl
                    {
                        BlogId = blog.BlogID,
                        ImagePath = dbPath
                    });

                    newImages.Add(dbPath);
                }
            }

            // 4️⃣ Save changes
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException?.Message ?? ex.Message);
            }

            // 5️⃣ Return response
            return Ok(new
            {
                Message = "Blog updated successfully",
                blog.BlogID,
                blog.Name,
                Images = newImages
            });
        }



        //[HttpPost("CreateBlog")]
        //public async Task<IActionResult> CreateBlog([FromForm] BlogDetails_ViewModel vm)
        //{
        //    if (vm.ImageFile == null || vm.ImageFile.Count == 0)
        //        return BadRequest("No image uploaded");

        //    var blogs = new Blogs_Tbl
        //    {
        //        BlogCategoryID = vm.BlogCategoryID,
        //        ProductID = vm.ProductID,
        //        Name = vm.Name,
        //        Description = vm.Description,
        //        StatusID = vm.StatusID,
        //        CreatedBy = vm.CreatedBy,
        //        CreatedDate = DateTime.Now
        //    };

        //    _context.Blogs_Tbl.Add(blogs);
        //    await _context.SaveChangesAsync();

        //    if (string.IsNullOrEmpty(_env.WebRootPath))
        //        _env.WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");

        //    string folder = Path.Combine(_env.WebRootPath, "uploads", "BlogImages");
        //    Directory.CreateDirectory(folder);

        //    List<string> imageList = new();

        //    foreach (var img in vm.ImageFile)
        //    {
        //        string fileName = Guid.NewGuid() + Path.GetExtension(img.FileName);
        //        string filePath = Path.Combine(folder, fileName);

        //        using var stream = new FileStream(filePath, FileMode.Create);
        //        await img.CopyToAsync(stream);

        //        string dbPath = "/uploads/BlogImages/" + fileName;

        //        _context.BlogImage_Tbl.Add(new BlogImage_Tbl
        //        {
        //            BlogId = blogs.BlogID,
        //            ImagePath = dbPath
        //        });

        //        imageList.Add(dbPath);
        //    }

        //    await _context.SaveChangesAsync();

        //    return Ok(new
        //    {
        //        Message = "Blogs created successfully",
        //        blogs.BlogID,
        //        blogs.Name,
        //        Images = imageList
        //    });
        //}



        //[HttpPost("CreateBlog")]
        //public async Task<IActionResult> CreateBlog([FromForm] BlogDetails_ViewModel vm)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    // 🔹 Ensure wwwroot exists (VERY IMPORTANT)
        //    if (string.IsNullOrEmpty(_env.WebRootPath))
        //        _env.WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");

        //    var blog = new Blogs_Tbl
        //    {
        //        CategoryID = vm.CategoryID,
        //        ProductID = vm.ProductID,
        //        Name = vm.Name,
        //        Description = vm.Description,
        //        StatusID = vm.StatusID,
        //        CreatedBy = vm.CreatedBy,
        //        CreatedDate = DateTime.Now
        //    };

        //    // 🔹 Image Upload
        //    if (vm.ImageFile != null && vm.ImageFile.Length > 0)
        //    {
        //        string folder = Path.Combine(_env.WebRootPath, "uploads", "blogs");
        //        Directory.CreateDirectory(folder);

        //        string fileName = Guid.NewGuid() + Path.GetExtension(vm.ImageFile.FileName);
        //        string fullPath = Path.Combine(folder, fileName);

        //        using var stream = new FileStream(fullPath, FileMode.Create);
        //        await vm.ImageFile.CopyToAsync(stream);

        //        blog.ImagePath = "/uploads/blogs/" + fileName;
        //    }

        //    _context.Blogs_Tbl.Add(blog);
        //    await _context.SaveChangesAsync();

        //    return Ok(new
        //    {
        //        Message = "Blog created successfully",
        //        blog.BlogID,
        //        blog.Name,
        //        blog.ImagePath
        //    });
        //}



        //[HttpPut]
        //[Route("UpdateBlog")]
        //public async Task<IActionResult> UpdateBlog([FromForm] Blog_ViewModel model)
        //{
        //    var result = await _blogService.UpdateBlog(model);
        //    return Ok(result);
        //}


        [HttpDelete]
        [Route("DeleteBlogById")]
        public async Task<IActionResult> DeleteBlog(int BlogID)
        {
            var result = await _blogService.DeleteBlog(BlogID);
            return Ok(result);
        }



        [HttpGet("Get_AllBlogsCategory")]
        public async Task<IActionResult> Get_AllBlogsCategory()
        {
            var data = await _blogService.Get_AllBlogCategory();
            return Ok(data);
        }

        [HttpGet("Get_BlogCategoryByID")]
        public async Task<IActionResult> Get_BlogCategoryByID(int BlogCategoryID)
        {
            var data = await _blogService.Get_BlogCategoryByID(BlogCategoryID);
            return Ok(data);
        }

        [HttpPost("Create_BlogCategory")]
        public async Task<IActionResult> Create_BlogCategory(BlogCategory_ViewModel model)
        {
            var data = await _blogService.CreateBlogCategory(model);
            return Ok(data);
        }

        [HttpPut("Update_BlogCategory")]
        public async Task<IActionResult> Update_BlogCategory(BlogCategory_ViewModel model)
        {
            var data = await _blogService.UpdateBlogCategory(model);
            return Ok(data);
        }

        [HttpGet("SortedByBlogCategoryID")]
        public async Task<IActionResult> getCategoryWiseBlogList(int BlogCategoryID)
        {
            var data = await _blogService.getCategoryWiseBlogList(BlogCategoryID);
            return Ok(data);
        }



        [HttpDelete("Delete_BlogCategory")]
        public async Task<IActionResult> Delete_BlogCategory(int BlogCategoryID)
        {
            var data = await _blogService.DeleteBlogCategory(BlogCategoryID);
            return Ok(data);
        }











    }
}
