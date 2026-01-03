using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjectDetails.Helper;
using ProjectDetails.Models;
using ProjectDetails.ViewModels;

namespace ProjectDetails.Services
{
    public class CategoryService
    {
        private readonly AppDBContext _context;
        private readonly IMapper _mapper;

        public CategoryService(AppDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }



        public async Task<List<Category_ViewModel>> GetAllCategories()
        {
            //var categories = await _context.Category_Tbl.Where(p => p.StatusId !=255).ToListAsync();
            //var data = _mapper.Map<List<Category_ViewModel>>(categories);
            //return data;

            try
            {
                var data = await _context.Category_Tbl.Where(a=>a.StatusId !=255).ToListAsync();
                var result = _mapper.Map<List<Category_ViewModel>>(data);
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

        }


        public async Task<Category_ViewModel> GetCategoryById(int id)
        {
            var category = await _context.Category_Tbl.Where(a => a.StatusId != 255).FirstOrDefaultAsync();
            if (category == null)
            {
                return null;
            }
            var data = _mapper.Map<Category_ViewModel>(category);
            return data;
        }


        public async Task<Category_ViewModel> CreateCategory(Category_ViewModel category)
        {
            if (category == null)
            {
                return null;
            }

            var data = _mapper.Map<Category_Tbl>(category);
            await _context.Category_Tbl.AddAsync(data);
            await _context.SaveChangesAsync();
            return await GetCategoryById(data.CategoryId);
        }


        public async Task<Category_ViewModel> UpdateCategory(int id, Category_ViewModel category)
        {
            if (category == null)
            {
                return null;
            }
            var existingCategory = await _context.Category_Tbl.Where(a => a.StatusId != 255 && a.CategoryId==category.CategoryId).FirstOrDefaultAsync();
            if (existingCategory == null)
            {
                return null;
            }            


            var data = _mapper.Map<Category_Tbl>(category);
            _context.Entry(existingCategory).CurrentValues.SetValues(data);
            await _context.SaveChangesAsync();
            return await GetCategoryById(id);
        }

        public async Task<bool> DeleteCategory(int id)
        {
            var existingCategory = await _context.Category_Tbl.Where(a => a.StatusId != 255).FirstOrDefaultAsync();
            if (existingCategory == null)
            {
                return false;
            }
            existingCategory.StatusId = 255; // Soft delete by setting StatusId to 255
            existingCategory.CreatedDate = DateTime.Now;
            _context.Category_Tbl.Update(existingCategory);
            await _context.SaveChangesAsync();
            return true;
        }



        public async Task<List<Category_ViewModel>>SortedByCategory(string sortOrder)
        {
            var categories = from c in _context.Category_Tbl
                             where c.StatusId != 255
                             select c;

            switch (sortOrder.ToLower())
            {
                case "name_desc":
                    categories = categories.OrderByDescending(c => c.CategoryName);
                    break;
                case "name_asc":
                    categories = categories.OrderBy(c => c.CategoryName);
                    break;
                case "date_desc":
                    categories = categories.OrderByDescending(c => c.CreatedDate);
                    break;
                case "date_asc":
                    categories = categories.OrderBy(c => c.CreatedDate);
                    break;
                default:
                    categories = categories.OrderBy(c => c.CategoryName);
                    break;
            }

            var categoryList = await categories.ToListAsync();
            return _mapper.Map<List<Category_ViewModel>>(categoryList);
        }


        public async Task<List<Category_ViewModel>> FilterBySameCategory(int CategoryTypeID)
        {

            var data = await _context.Category_Tbl
                        .Where(c => c.StatusId != 255 && c.CategoryId == CategoryTypeID)
                        .ToListAsync();
            return _mapper.Map<List<Category_ViewModel>>(data);

        }



    }
}
