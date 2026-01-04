using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectDetails.Helper;
using ProjectDetails.Models;
using ProjectDetails.ViewModels;


[ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly AppDBContext _context;
        private readonly ProductService _service;
        private readonly IWebHostEnvironment _env;

        public ProductController(AppDBContext context, IWebHostEnvironment env,ProductService service)
        {
            _context = context;
            _service = service;
            _env = env;
        }

        [HttpPost("CreateProduct")]
        public async Task<IActionResult> CreateProduct([FromForm] ProductCreate_VM vm)
        {
            // 1️⃣ Product Save
            var product = new Product_Tbl
            {
                ProductName = vm.ProductName,
                Description = vm.Description,
                Price = vm.Price,
                Stock = vm.Stock,
                CategoryId = vm.CategoryID,
                StatusID = 0, // Active
                SKU = vm.SKU,
                Tags = vm.Tags,
                MaterialName = vm.MaterialName,
                MaterialCode = vm.MaterialCode,
                //Review = vm.Review,
                ExpireDate = vm.ExpireDate,
                //CreatedBy = vm.CreatedBy,

            };

            _context.Product_Tbl.Add(product);
            await _context.SaveChangesAsync(); // ProductId তৈরি হবে

            // 2️⃣ Folder
            string folder = Path.Combine(_env.WebRootPath, "uploads", "products");
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            List<string> imageList = new List<string>();

            // 3️⃣ Image Save
            foreach (var img in vm.Images)
            {
                string fileName = Guid.NewGuid() + Path.GetExtension(img.FileName);
                string filePath = Path.Combine(folder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await img.CopyToAsync(stream);
                }

                string dbPath = "/uploads/products/" + fileName;

                _context.ProductImage_Tbl.Add(new ProductImage_Tbl
                {
                    ProductId = product.ProductId,
                    ImagePath = dbPath
                });

                imageList.Add(dbPath);
            }

            await _context.SaveChangesAsync();

            // 4️⃣ SIMPLE RESPONSE (Entity ফেরত না)
            return Ok(new
            {
                Message = "Product created successfully",
                product.ProductId,
                product.ProductName,
                Images = imageList
            });
        }



    [HttpGet("GetProduct/{id}")]
    public async Task<IActionResult> GetProduct(int id)
    {
        var product = await _service.GetProductById(id);

        if (product == null)
            return NotFound("Product not found");

        return Ok(product);
    }



    [HttpGet("GetAllProducts")]
    public async Task<IActionResult> GetAllProducts()
    {
        var products = await _service.GetAllProducts();
        return Ok(products);
    }



    [HttpPut("UpdateProduct")]
    public async Task<IActionResult> UpdateProduct([FromForm] ProductUpdate_VM model)
    {
        try
        {
            var updatedProduct = await _service.UpdateProduct(model);

         
            var images = await _context.ProductImage_Tbl
                            .Where(i => i.ProductId == updatedProduct.ProductId)
                            .Select(i => i.ImagePath)
                            .ToListAsync();

            return Ok(new
            {
                message = "Product updated successfully",
                productId = updatedProduct.ProductId,
                productName = updatedProduct.ProductName,
                images = images
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }


    //[HttpDelete("DeleteProduct/{productId}")]
    //public async Task<IActionResult> DeleteProduct(int productId)
    //{
    //    try
    //    {
    //        await _service.DeleteProduct(productId);
    //        return Ok(new { message = "Product deleted successfully" });
    //    }
    //    catch (Exception ex)
    //    {
    //        return BadRequest(new { message = ex.Message });
    //    }
    //}

    // Soft delete product
    [HttpDelete("SoftDeleteProduct/{productId}")]
    public async Task<IActionResult> SoftDeleteProduct(int productId)
    {
        try
        {
            await _service.SoftDeleteProduct(productId);
            return Ok(new { message = "Product soft deleted successfully" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    // Delete only images
    [HttpDelete("DeleteProductImages")]
    public async Task<IActionResult> DeleteProductImages(int productId,int ProductImageId)
    {
        try
        {
            await _service.DeleteProductImage(productId, ProductImageId);
            return Ok(new { message = "Product images deleted successfully" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }



    [HttpDelete("DeleteImage/{ProductImageId}")]
    public async Task<IActionResult> DeleteImage(int ProductImageId)
    {
        try
        {
            await _service.DeleteImage(ProductImageId);
            return Ok(new { message = "Product images deleted successfully" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }



    [HttpGet("GetCategoryWiseProductList/{categoryId}")]
    public async Task<IActionResult> GetCategoryWiseProductList(int categoryId)
    {
        var products = await _service.GetCategoryWiseProductList(categoryId);
        return Ok(products);
    }



    [HttpGet("GetProductsByPriceRange")]
    public async Task<ActionResult<List<Product_VM>>> GetProductsByPriceRange(int CategoryID, decimal minPrice, decimal maxPrice, int CmdID)
    {
        if (minPrice < 0 || maxPrice < 0 || minPrice > maxPrice)
            return BadRequest("Invalid price range");

        var products = await _service.GetProductsByPrice(CategoryID,minPrice, maxPrice,CmdID);
        return Ok(products);
    }






}






