

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Northwind.Business.Abstract;
using Northwind.Core.Models.Request;
using Northwind.Core.Models.Request.Category;
using Northwind.Core.Models.Response;
using Northwind.Core.Models.Response.Category;
using Northwind.DataAccess.Repositories.Abstract;
using Northwind.Entities.Concrete;

namespace Northwind.Business.Concrete
{
    public class CategoryService(ICategoryRepository categoryRepository, IMapper mapper) : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository = categoryRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<PaginatedResponse<CategoryResponseModel>> GetAllCategoriesAsync(PaginatedRequest paginated)
        {
            var categories = await _categoryRepository.GetAllAsync2(
                paginated
            );

            return _mapper.Map<PaginatedResponse<CategoryResponseModel>>(categories);
        }
        public async Task<CategoryResponseModel> GetCategoryAsync(int id)
        {


            var category = await _categoryRepository.GetAsync(filter: c => c.CategoryID == id,include:p=>p.Include(p=>p.Products));
            return  _mapper.Map<CategoryResponseModel>(category);
        }
        public async Task<List<CategoryResponseModel>> AddCategoryAsync(List<CategoryRequestModel> request)
        {
            List<Category> categories = [];


            foreach (var item in request)
            {
                Category category = new();

                category.Name = item.CategoryName;
                category.Slug = GenerateSlug(item.CategoryName);

                if(item.CategoryID != 0)
                {
                    category.CategoryID = item.CategoryID;
                    await _categoryRepository.UpdateAsync(category);
                }else
                {
                   var addedCategory = await _categoryRepository.AddAsync(category);
                    category.CategoryID = addedCategory.CategoryID;
                }

                if (item.List !=null)
                {
                    foreach (var subCategory in item.List)
                    {
                       Category subCategories = new();
                        subCategories.Name = subCategory.SubCategoryName;
                        subCategories.Slug = GenerateSlug(subCategory.SubCategoryName);
                        subCategories.Parent_ID = category.CategoryID;

                       await _categoryRepository.AddAsync(subCategories);
                    }
                }
            }
            var allCategory = await _categoryRepository.GetAllAsync();
            return _mapper.Map<List<CategoryResponseModel>>(allCategory);
        }
      
        public async Task<CategoryResponseModel> UpdateCategoryAsync(CategoryUpdateRequestModel category)
        {
           var updatedCategory =  _mapper.Map<Category>(category);
           return  _mapper.Map<CategoryResponseModel>(await _categoryRepository.UpdateAsync(updatedCategory));
        }

        public async Task<int> DeleteCategoryAsync(int id)
        {   
           var category = await _categoryRepository.GetAsync(filter: c => c.CategoryID == id);
            var isDeleted = await _categoryRepository.DeleteAsync(category);
           return isDeleted;
        }


        private string GenerateSlug(string categoryName)
        {
            return categoryName
                .ToLower()
                .Replace(" ", "-")
                .Replace("--", "-")
                .Trim('-');
        }
    }
}
