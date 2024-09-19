

using AutoMapper;
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
           return _mapper.Map<PaginatedResponse<CategoryResponseModel>>(await _categoryRepository.GetAllAsync(paginated,null,c=>c.Products));

          
        }
        public async Task<CategoryResponseModel> GetCategoryAsync(int id)
        {
            return  _mapper.Map<CategoryResponseModel>(await _categoryRepository.GetAsync(c=>c.CategoryID==id,c=>c.Products));
        }
        public async Task<CategoryResponseModel> AddCategoryAsync(CategoryRequestModel category)
        {
            var categoryEntity = _mapper.Map<Category>(category);
            var addedCategory = await _categoryRepository.AddAsync(categoryEntity);
            var responseDto = _mapper.Map<CategoryResponseModel>(addedCategory);
            return responseDto;
        }
      
        public async Task<CategoryResponseModel> UpdateCategoryAsync(CategoryUpdateRequestModel category)
        {
           var updatedCategory =  _mapper.Map<Category>(category);
           return  _mapper.Map<CategoryResponseModel>(await _categoryRepository.UpdateAsync(updatedCategory));
        }

        public async Task<int> DeleteCategoryAsync(int id)
        {
           return await _categoryRepository.DeleteAsync(id);
        }
    }
}
