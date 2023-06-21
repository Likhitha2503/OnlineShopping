﻿using OnlineShopping_Web.Models.DTO;

namespace OnlineShopping_Web.Services.IServices
{
    public interface ICategoryService
    {
        Task<T> GetAllAsync<T>(string token);
        Task<T> GetAsync<T>(int id, string token);
        Task<T> CreateAsync<T>(CategoryDto dto, string token);
        Task<T> UpdateAsync<T>(CategoryDto dto, string token);
        Task<T> DeleteAsync<T>(int id, string token);
    }
}
