using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjectDetails.Helper;
using ProjectDetails.Models;
using ProjectDetails.ViewModels;

namespace ProjectDetails.Services
{
    public class BlogService
    {
        private readonly AppDBContext _db;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public BlogService(AppDBContext db, IMapper mapper, IWebHostEnvironment env)
        {
            _db = db;
            _mapper = mapper;
            _env = env;
        }



        public async Task<List<Blogs_ViewModels>> GetAllBlogs()
        {
            var data = await (from b in _db.Blogs_Tbl.Where(a => a.StatusID != 255)
                              join c in _db.BlogCategory_Tbl.Where(a => a.StatusId != 255)
                              on b.BlogCategoryID equals c.CategoryId
                              select new
                              {
                                  b.BlogID,
                                  b.Name,
                                  b.Description,
                                  b.StatusID,
                                  //b.CreatedBy,
                                  //b.CreatedDate,
                                  b.ProductID,
                                  BlogCategoryName = c.CategoryName,
                                  BlogCategoryDescription = c.CategoryDescription
                              }).ToListAsync();

            if (data == null || !data.Any())
                return new List<Blogs_ViewModels>();

            // Map anonymous object to your ViewModel
            var result = _mapper.Map<List<Blogs_ViewModels>>(data);

            return result;
        }




        public async Task<Blogs_ViewModels> GetBlogById(int BlogID)
        {
            var blog = await _db.Blogs_Tbl.Where(a => a.StatusID != 255 && a.BlogID == BlogID).FirstOrDefaultAsync();
            if (blog == null)
            {
                return null;
            }
            var data = _mapper.Map<Blogs_ViewModels>(blog);
            return data;
        }

        //public async Task<Blog_ViewModel> CreateBlog(Blog_ViewModel blog)
        //{
        //    if (blog == null)
        //        return null;

        //    string imagePath = null;

        //    // IMAGE UPLOAD
        //    if (blog.ImageFile != null && blog.ImageFile.Length > 0)
        //    {
        //        var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/BlogImages");

        //        if (!Directory.Exists(folderPath))
        //            Directory.CreateDirectory(folderPath);

        //        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(blog.ImageFile.FileName);
        //        var fullPath = Path.Combine(folderPath, fileName);

        //        using (var stream = new FileStream(fullPath, FileMode.Create))
        //        {
        //            await blog.ImageFile.CopyToAsync(stream);
        //        }

        //        imagePath = "/BlogImages/" + fileName; // saved in DB
        //    }

        //    var data = _mapper.Map<Blogs_Tbl>(blog);
        //    data.ImagePath = imagePath;
        //    data.CreatedDate = DateTime.Now;

        //    await _db.Blogs_Tbl.AddAsync(data);
        //    await _db.SaveChangesAsync();

        //    var result = _mapper.Map<Blog_ViewModel>(data);
        //    return result;
        //}


        //public async Task<Blog_ViewModel> CreateBlog(Blog_ViewModel blog)
        //{
        //    if (blog == null)
        //        return null;

        //    string imagePath = null;

        //    if (blog.ImageFile != null && blog.ImageFile.Length > 0)
        //    {
        //        if (string.IsNullOrEmpty(_env.WebRootPath))
        //            throw new Exception("WebRootPath is NULL. wwwroot folder missing.");

        //        var folderPath = Path.Combine(_env.WebRootPath, "BlogImages");

        //        if (!Directory.Exists(folderPath))
        //            Directory.CreateDirectory(folderPath);

        //        var fileName = Guid.NewGuid() + Path.GetExtension(blog.ImageFile.FileName);
        //        var fullPath = Path.Combine(folderPath, fileName);

        //        using (var stream = new FileStream(fullPath, FileMode.Create))
        //        {
        //            await blog.ImageFile.CopyToAsync(stream);
        //        }

        //        imagePath = "/BlogImages/" + fileName;
        //    }

        //    var data = new Blogs_Tbl
        //    {
        //        CategoryID = blog.CategoryID,
        //        ProductID = blog.ProductID,
        //        Name = blog.Name,
        //        Description = blog.Description,
        //        ImagePath = imagePath,
        //        StatusID = blog.StatusID,
        //        CreatedBy = blog.CreatedBy,
        //        CreatedDate = DateTime.Now
        //    };

        //    await _db.Blogs_Tbl.AddAsync(data);
        //    await _db.SaveChangesAsync();

        //    return new Blog_ViewModel
        //    {
        //        BlogID = data.BlogID,
        //        CategoryID = data.CategoryID,
        //        ProductID = data.ProductID,
        //        Name = data.Name,
        //        Description = data.Description,
        //        ImagePath = data.ImagePath,
        //        StatusID = data.StatusID,
        //        CreatedBy = data.CreatedBy
        //    };
        //}





        //public async Task<Blog_ViewModel> UpdateBlog(Blog_ViewModel blog)
        //{
        //    var existingBlog = await _db.Blogs_Tbl
        //        .FirstOrDefaultAsync(x => x.BlogID == blog.BlogID && x.StatusID != 255);

        //    if (existingBlog == null)
        //        return null;

        //    // IMAGE UPDATE
        //    if (blog.ImageFile != null && blog.ImageFile.Length > 0)
        //    {
        //        // delete old image
        //        if (!string.IsNullOrEmpty(existingBlog.ImagePath))
        //        {
        //            var oldPath = Path.Combine(
        //                Directory.GetCurrentDirectory(),
        //                "wwwroot",
        //                existingBlog.ImagePath.TrimStart('/')
        //            );

        //            if (File.Exists(oldPath))
        //                File.Delete(oldPath);
        //        }

        //        // save new image
        //        var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/BlogImages");

        //        if (!Directory.Exists(folderPath))
        //            Directory.CreateDirectory(folderPath);

        //        var fileName = Guid.NewGuid() + Path.GetExtension(blog.ImageFile.FileName);
        //        var newPath = Path.Combine(folderPath, fileName);

        //        using (var stream = new FileStream(newPath, FileMode.Create))
        //        {
        //            await blog.ImageFile.CopyToAsync(stream);
        //        }

        //        existingBlog.ImagePath = "/BlogImages/" + fileName;
        //    }

        //    // UPDATE FIELDS
        //    existingBlog.CategoryID = blog.CategoryID;
        //    existingBlog.ProductID = blog.ProductID;
        //    existingBlog.Name = blog.Name;
        //    existingBlog.Description = blog.Description;
        //    existingBlog.StatusID = blog.StatusID;
        //    existingBlog.CreatedBy = blog.CreatedBy;

        //    await _db.SaveChangesAsync();

        //    return _mapper.Map<Blog_ViewModel>(existingBlog);
        //}


        public async Task<bool> DeleteBlog(int BlogID)
        {
            var existingBlog = await _db.Blogs_Tbl
                .FirstOrDefaultAsync(x => x.BlogID == BlogID && x.StatusID != 255);

            if (existingBlog == null)
                return false;

            existingBlog.StatusID = 255;
            await _db.SaveChangesAsync();

            return true;

        }

        public async Task<List<BlogCategory_ViewModel>> Get_AllBlogCategory()
        {
            var data = await _db.BlogCategory_Tbl.Where(a => a.StatusId != 255).ToListAsync();
            if (data == null)
            {
                return null;
            }
            var result = _mapper.Map<List<BlogCategory_ViewModel>>(data);
            return result;
        }

        public async Task<BlogCategory_ViewModel> Get_BlogCategoryByID(int BlogID)
        {
            var data = await _db.BlogCategory_Tbl.Where(a => a.StatusId != 255 && a.CategoryId == BlogID).FirstOrDefaultAsync();
            if (data == null)
            {
                return null;
            }
            var result = _mapper.Map<BlogCategory_ViewModel>(data);
            return result;

        }

        public async Task<BlogCategory_ViewModel>CreateBlogCategory(BlogCategory_ViewModel blogCategory)
        {
            if (blogCategory == null)
            {
                return null;
            }
            var data = _mapper.Map<BlogCategory_Tbl>(blogCategory);
            await _db.BlogCategory_Tbl.AddAsync(data);
            await _db.SaveChangesAsync();
            return await Get_BlogCategoryByID(data.CategoryId);
        }

        public async Task<BlogCategory_ViewModel> UpdateBlogCategory(BlogCategory_ViewModel model)
        {
            var data = await _db.BlogCategory_Tbl.Where(a => a.StatusId != 255 && a.CategoryId == model.CategoryId).FirstOrDefaultAsync();
            if (data == null)
            {
                return null;
            }
            var result = _mapper.Map<BlogCategory_Tbl>(model);
            _db.Entry(data).CurrentValues.SetValues(result);
            await _db.SaveChangesAsync();
            return await Get_BlogCategoryByID(result.CategoryId);
        }

        public async Task<bool> DeleteBlogCategory(int BlogCategory)
        {
            var data = await _db.BlogCategory_Tbl.Where(a => a.StatusId != 255 && a.CategoryId == BlogCategory).FirstOrDefaultAsync();
            if (data == null)
            {
                return false;
            }
            data.StatusId = 255;
            await _db.SaveChangesAsync();
            return true;

        }


        public async Task<List<object>> getCategoryWiseBlogList(int CategoryID)
        {
            var data = await (
                from b in _db.Blogs_Tbl
                join bc in _db.BlogCategory_Tbl
                    on b.BlogCategoryID equals bc.CategoryId
                where b.StatusID != 255
                      && bc.StatusId != 255
                      && b.BlogCategoryID == CategoryID
                select new
                {
                    b.BlogID,
                    b.Name,
                    b.Description,
                    b.BlogCategoryID,
                    b.ProductID,
                    CategoryName = bc.CategoryName
                }
            ).ToListAsync<object>();

            return data;
        }







    }
}
