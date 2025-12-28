using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectDetails.Services;
using ProjectDetails.ViewModels;

namespace ProjectDetails.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryService _categoryService;
        public CategoryController(CategoryService categoryService)
        {

            _categoryService = categoryService;
            
        }

        [HttpGet("GetAll_Category")]
        public async Task<ActionResult<List<Category_ViewModel>>> Get() {

            var data = await _categoryService.GetAllCategories();

            return Ok(data);
        }

        [HttpGet("GetCategoryById/{id}")]
        public async Task<ActionResult<Category_ViewModel>> GetCategoryById(int id)
        {

            var data = await _categoryService.GetCategoryById(id);

            return Ok(data);
        }

        [HttpPost]
        public async Task<ActionResult<Category_ViewModel>> Post(Category_ViewModel category)
        {

            var data = await _categoryService.CreateCategory(category);

            return Ok(data);
        }


        [HttpPut("GetCategoryById/{id}")]
        public async Task<ActionResult<Category_ViewModel>> Put(int id, Category_ViewModel category)
        {

            var data = await _categoryService.UpdateCategory(id, category);

            return Ok(data);
        }


        [HttpPut("DeleteCategoryById/{id}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {

            var data = await _categoryService.DeleteCategory(id);

            return Ok(data);
        }


    }
}
