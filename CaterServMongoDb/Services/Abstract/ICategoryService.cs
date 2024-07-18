﻿using CaterServMongoDb.Dtos.CategoryDtos;

namespace CaterServMongoDb.Services.Abstract
{
    public interface ICategoryService
    {
        Task<List<ResultCategoryDto>> GetAllCategories();

        Task<ResultCategoryDto> GetCategoryById(string id);

        Task UpdateCategory(UpdateCategoryDto categoryDto);

        Task CreateCategory(CreateCategoryDto categoryDto);

        Task DeleteCategory(string id);

    }
}
