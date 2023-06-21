using OnlineShopping_Web.Models.DTO;

namespace OnlineShopping_Web.Services.IServices
{
    public interface IAuthService
    {
        Task<T> LoginAsync<T>(LoginRequestDto objToCreate);
        Task<T> RegisterAsync<T>(RegistrationRequestDto objToCreate);
    }
}
