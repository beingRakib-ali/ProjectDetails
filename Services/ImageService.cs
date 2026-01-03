//using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.Http;
//using ProjectDetails.Helper;
//using ProjectDetails.Models;

//public class ProductService
//{
//    private readonly AppDBContext _context;
//    private readonly IWebHostEnvironment _env;

//    public ProductService(AppDBContext context, IWebHostEnvironment env)
//    {
//        _context = context;
//        _env = env;
//    }

//    public async Task<Product_Tbl> CreateProduct(Product_Tbl product, List<IFormFile> images)
//    {
//        // 1️⃣ Product Save
//        _context.Product_Tbl.Add(product);
//        await _context.SaveChangesAsync();

//        // 2️⃣ Image Folder
//        string folderPath = Path.Combine(_env.WebRootPath, "uploads", "products");

//        if (!Directory.Exists(folderPath))
//            Directory.CreateDirectory(folderPath);

//        // 3️⃣ Image Save
//        foreach (var img in images)
//        {
//            string fileName = Guid.NewGuid() + Path.GetExtension(img.FileName);
//            string filePath = Path.Combine(folderPath, fileName);

//            using var stream = new FileStream(filePath, FileMode.Create);
//            await img.CopyToAsync(stream);

//            product.Images.Add(new ProductImage_Tbl
//            {
//                ProductId = product.ProductId,
//                ImageUrl = "/uploads/products/" + fileName
//            });
//        }

//        await _context.SaveChangesAsync();
//        return product;
//    }
//}
