﻿using Microsoft.AspNetCore.Mvc;
using Northwind.Business.Abstract;
using Northwind.Core.Models.Request;
using Northwind.Core.Models.Request.Category;

namespace Northwind.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController(ICategoryService categoryService) : Controller
    {
        private readonly ICategoryService _categoryService = categoryService;

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery ]PaginatedRequest paginatedRequest)
        {
            var categories = await _categoryService.GetAllCategoriesAsync(paginatedRequest);
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var category = await _categoryService.GetCategoryAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CategoryRequestModel category)
        {
            var addedCategory = await _categoryService.AddCategoryAsync(category);
            return CreatedAtAction("Get", new { id = addedCategory.CategoryID }, addedCategory);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] CategoryUpdateRequestModel category)
        {
            var updatedCategory = await _categoryService.UpdateCategoryAsync(category);
            return Ok(updatedCategory);
        }

        [HttpDelete("{id}")]
        public async Task<int> Delete(int id)
        {
            return await _categoryService.DeleteCategoryAsync(id);
        }


    }
}