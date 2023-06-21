using OnlineShopping_Web.Models.DTO;

namespace OnlineShopping_Web.Services.IServices
{
    public interface IProductService
    {
        Task<T> GetAllAsync<T>(string token);
        Task<T> GetAsync<T>(int id, string token);
        Task<T> CreateAsync<T>(ProductDto dto, string token);
        Task<T> UpdateAsync<T>(ProductDto dto, string token);
        Task<T> DeleteAsync<T>(int id, string token);
    }
}
