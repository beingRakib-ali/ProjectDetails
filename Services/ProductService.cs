using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ProjectDetails.DTOs;
using ProjectDetails.Helper;
using ProjectDetails.Models;
using ProjectDetails.ViewModels;

public class ProductService
{
    private readonly AppDBContext _context;
    private readonly IMapper _mapper;
    private readonly IWebHostEnvironment _env;

    public ProductService(AppDBContext context, IWebHostEnvironment env,IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
        _env = env;
    }

    public async Task<Product_Tbl> CreateProduct(ProductCreate_VM model)
    {
        var product = new Product_Tbl
        {
            ProductName = model.ProductName,
            Description = model.Description,
            Price = model.Price,
            Stock = model.Stock,
            CreatedDate = DateTime.Now
        };

        _context.Product_Tbl.Add(product);
        await _context.SaveChangesAsync(); // ProductId পাওয়া যাবে

        string folderPath = Path.Combine(_env.WebRootPath, "uploads", "products");
        if (!Directory.Exists(folderPath))
            Directory.CreateDirectory(folderPath);

        foreach (var img in model.Images)
        {
            string fileName = Guid.NewGuid() + Path.GetExtension(img.FileName);
            string filePath = Path.Combine(folderPath, fileName);

            using var stream = new FileStream(filePath, FileMode.Create);
            await img.CopyToAsync(stream);

            _context.ProductImage_Tbl.Add(new ProductImage_Tbl
            {
                ProductId = product.ProductId,
                ImagePath = "/uploads/products/" + fileName
            });
        }

        await _context.SaveChangesAsync();
        return product;
    }





    public async Task<object> GetProductById(int id)
    {
        var product = await _context.Product_Tbl
            .Where(p => p.ProductId == id && p.StatusID != 255)
            .Select(p => new
            {
                productId = p.ProductId,
                productName = p.ProductName,
                description = p.Description,
                price = p.Price,
                stock = p.Stock,
                images = _context.ProductImage_Tbl
                            .Where(i => i.ProductId == p.ProductId)
                            .Select(i => i.ImagePath)
                            .ToList()
            })
            .FirstOrDefaultAsync();

        return product;
    }




    public async Task<List<ProductDTO>> GetAllProducts()
    {
        var products = await _context.Product_Tbl.Where(a=>a.StatusID !=255)
            .Select(p => new ProductDTO
            {
                ProductId = p.ProductId,
                ProductName = p.ProductName,
                Description = p.Description,
                Price = p.Price,
                Stock = p.Stock,
                Images = _context.ProductImage_Tbl
                            .Where(i => i.StatusID !=255 && i.ProductId == p.ProductId)
                            .Select(i => i.ImagePath)
                            .ToList()
            })
            .ToListAsync();

        return products;
    }





    public async Task<Product_Tbl> UpdateProduct(ProductUpdate_VM model)
    {
        // 1️⃣ Product খুঁজে বের করা
        var product = await _context.Product_Tbl.FirstOrDefaultAsync(p => p.ProductId == model.ProductId);

        if (product == null)
            throw new Exception("Product not found");

        // 2️⃣ Update basic info
        product.ProductName = model.ProductName;
        product.Description = model.Description;
        product.Price = model.Price;
        product.Stock = model.Stock;
        product.MaterialName = model.MaterialName;
        product.MaterialCode = model.MaterialCode;
        product.SKU = model.SKU;
        product.Tags = model.Tags;
        product.Review = model.Review;
        product.CreatedBy = model.CreatedBy;
        product.ExpireDate = model.ExpireDate;


        await _context.SaveChangesAsync();

        // 3️⃣ নতুন images save করা
        if (model.Images != null && model.Images.Count > 0)
        {
            string folderPath = Path.Combine(_env.WebRootPath, "uploads", "products");
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            foreach (var img in model.Images)
            {
                string fileName = Guid.NewGuid() + Path.GetExtension(img.FileName);
                string filePath = Path.Combine(folderPath, fileName);

                using var stream = new FileStream(filePath, FileMode.Create);
                await img.CopyToAsync(stream);

                var productImage = new ProductImage_Tbl
                {
                    ProductId = product.ProductId,
                    ImagePath = "/uploads/products/" + fileName,
                    CreatedDate = DateTime.Now
                };

                _context.ProductImage_Tbl.Add(productImage);
            }

            await _context.SaveChangesAsync();
        }

        return product;
    }




    //public async Task<bool> DeleteProduct(int productId)
    //{
    //    // 1️⃣ Product খুঁজে বের করা
    //    var product = await _context.Product_Tbl.FirstOrDefaultAsync(p => p.ProductId == productId);
    //    if (product == null)
    //        throw new Exception("Product not found");

    //    // 2️⃣ Related images fetch করা
    //    var images = await _context.ProductImage_Tbl
    //                    .Where(i => i.ProductId == productId)
    //                    .ToListAsync();

    //    // 3️⃣ Images file system থেকে delete করা
    //    foreach (var img in images)
    //    {
    //        var filePath = Path.Combine(_env.WebRootPath, img.ImagePath.TrimStart('/').Replace("/", Path.DirectorySeparatorChar.ToString()));
    //        if (File.Exists(filePath))
    //            File.Delete(filePath);
    //    }

    //    // 4️⃣ Images table থেকে delete করা
    //    _context.ProductImage_Tbl.RemoveRange(images);

    //    // 5️⃣ Product table থেকে delete করা
    //    _context.Product_Tbl.Remove(product);

    //    await _context.SaveChangesAsync();

    //    return true;
    //}


    public async Task<bool> SoftDeleteProduct(int productId)
    {
        var product = await _context.Product_Tbl.Where(a=>a.StatusID != 255).FirstOrDefaultAsync(p => p.ProductId == productId);
        if (product == null)
            throw new Exception("Product not found");

        // 1️⃣ Product soft delete
        product.StatusID = 255;

        // 2️⃣ Related images soft delete
        var images = await _context.ProductImage_Tbl
                            .Where(i => i.ProductId == productId)
                            .ToListAsync();

        foreach (var img in images)
        {
            img.StatusID = 255;
        }

        //await _context.ProductImage_Tbl.Update(images);
        await _context.SaveChangesAsync();
        return true;
    }



    public async Task<bool> DeleteProductImage(int productId, int productImageId)
    {
        // 1️⃣ Specific image fetch
        var img = await _context.ProductImage_Tbl
            .FirstOrDefaultAsync(i =>
                i.ProductId == productId &&
                i.ProductImageId == productImageId &&
                i.StatusID != 255);

        if (img == null)
            throw new Exception("Image not found");

        // 2️⃣ File system থেকে image delete
        var filePath = Path.Combine(
            _env.WebRootPath,
            img.ImagePath.TrimStart('/')
                .Replace("/", Path.DirectorySeparatorChar.ToString())
        );

        if (File.Exists(filePath))
            File.Delete(filePath);

        // 3️⃣ Soft delete (StatusID = 255)
        img.StatusID = 255;

        await _context.SaveChangesAsync();
        return true;
    }


    public async Task<bool> DeleteImage(int productImageId)
    {
        // 1️⃣ Image খোঁজা (global)
        var img = await _context.ProductImage_Tbl
            .FirstOrDefaultAsync(i =>
                i.ProductImageId == productImageId &&
                i.StatusID != 255);

        if (img == null)
            throw new Exception("Image not found");

        // 2️⃣ Server থেকে file delete (OPTIONAL)
        var filePath = Path.Combine(
            _env.WebRootPath,
            img.ImagePath
                .TrimStart('/')
                .Replace("/", Path.DirectorySeparatorChar.ToString())
        );

        //if (File.Exists(filePath))
        //    File.Delete(filePath);

        // 3️⃣ Soft delete in DB
        img.StatusID = 255;
        //img.DeletedDate = DateTime.Now;   // ⭐ auto-clean job এর জন্য গুরুত্বপূর্ণ

        await _context.SaveChangesAsync();
        return true;
    }






    public async Task<List<object>> GetCategoryWiseProductList(int categoryId)
    {
        var products = await _context.Product_Tbl
            .Where(p => p.StatusID != 255 && p.CategoryId == categoryId)
            .Select(p => new
            {
                productId = p.ProductId,
                productName = p.ProductName,
                description = p.Description,
                price = p.Price,
                stock = p.Stock,
                images = _context.ProductImage_Tbl
                            .Where(i => i.ProductId == p.ProductId && i.StatusID != 255)
                            .Select(i => i.ImagePath)
                            .ToList()
            })
            .ToListAsync();

        //return products.Cast<object>().ToList();
        return products.ToList<object>();


    }


    public async Task<List<Product_VM>> GetProductsByPrice(int CategoryID,decimal minPrice, decimal maxPrice,int CmdID)
    {

        if (CmdID == 0)
        {
            var data = await _context.Product_Tbl
           .Where(p => p.StatusID != 255 && p.Price >= minPrice && p.Price <= maxPrice)
           .Select(p => new Product_VM
           {
               ProductId = p.ProductId,
               ProductName = p.ProductName,
               Description = p.Description,
               Price = p.Price,
               Stock = p.Stock,

               ImageUrls = _context.ProductImage_Tbl
                           .Where(i => i.ProductId == p.ProductId && i.StatusID != 255)
                           .Select(i => i.ImagePath)
                           .ToList()
           })
           .ToListAsync();

            return data;
        }
       

        else
        {
            var data = await _context.Product_Tbl
                .Where(a => a.StatusID != 255 && a.CategoryId == CategoryID && a.Price>= minPrice && a.Price <= maxPrice)
                .Select(p => new Product_VM
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                    Description = p.Description,
                    Price = p.Price,
                    Stock = p.Stock,

                    ImageUrls = _context.ProductImage_Tbl
                           .Where(i => i.ProductId == p.ProductId && i.StatusID != 255)
                           .Select(i => i.ImagePath)
                           .ToList()
                }).ToListAsync();

            return data;

        }
    }







}
