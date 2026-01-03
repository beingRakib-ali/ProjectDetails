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
        public async Task<IActionResult> CreateBlog([FromForm] BlogDetails_ViewModel vm)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // 🔹 Ensure wwwroot exists (VERY IMPORTANT)
            if (string.IsNullOrEmpty(_env.WebRootPath))
                _env.WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");

            var blog = new Blogs_Tbl
            {
                CategoryID = vm.CategoryID,
                ProductID = vm.ProductID,
                Name = vm.Name,
                Description = vm.Description,
                StatusID = vm.StatusID,
                CreatedBy = vm.CreatedBy,
                CreatedDate = DateTime.Now
            };

            // 🔹 Image Upload
            if (vm.ImageFile != null && vm.ImageFile.Length > 0)
            {
                string folder = Path.Combine(_env.WebRootPath, "uploads", "blogs");
                Directory.CreateDirectory(folder);

                string fileName = Guid.NewGuid() + Path.GetExtension(vm.ImageFile.FileName);
                string fullPath = Path.Combine(folder, fileName);

                using var stream = new FileStream(fullPath, FileMode.Create);
                await vm.ImageFile.CopyToAsync(stream);

                blog.ImagePath = "/uploads/blogs/" + fileName;
            }

            _context.Blogs_Tbl.Add(blog);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                Message = "Blog created successfully",
                blog.BlogID,
                blog.Name,
                blog.ImagePath
            });
        }



        [HttpPut]
        [Route("UpdateBlog")]
        public async Task<IActionResult> UpdateBlog([FromForm] Blog_ViewModel model)
        {
            var result = await _blogService.UpdateBlog(model);
            return Ok(result);
        }


        [HttpDelete]
        [Route("DeleteBlogById")]
        public async Task<IActionResult> DeleteBlog(int BlogID)
        {
            var result = await _blogService.DeleteBlog(BlogID);
            return Ok(result);
        }



    }
}
