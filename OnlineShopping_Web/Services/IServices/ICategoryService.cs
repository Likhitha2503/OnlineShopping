using OnlineShopping_Web.Models.DTO;

namespace OnlineShopping_Web.Services.IServices
{
    public interface ICategoryService
    {
        Task<T> GetAllAsync<T>();
        Task<T> GetAsync<T>(int id);
        Task<T> CreateAsync<T>(CategoryDto dto);
        Task<T> UpdateAsync<T>(CategoryDto dto);
        Task<T> DeleteAsync<T>(int id);
    }
}
