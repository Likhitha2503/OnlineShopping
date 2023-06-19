using OnlineShopping_Web.Models.DTO;

namespace OnlineShopping_Web.Services.IServices
{
    public interface IProductService
    {
        Task<T> GetAllAsync<T>();
        Task<T> GetAsync<T>(int id);
        Task<T> CreateAsync<T>(ProductDto dto);
        Task<T> UpdateAsync<T>(ProductDto dto);
        Task<T> DeleteAsync<T>(int id);
    }
}
