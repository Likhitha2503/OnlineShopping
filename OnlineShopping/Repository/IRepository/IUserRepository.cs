using OnlineShopping_API.Models;
using OnlineShopping_Web.Models.DTO;

namespace OnlineShopping_API.Repository.IRepository
{
    public interface IUserRepository
    {
        bool IsUniqueUser(string Email);
        Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
        Task<LocalUser> Register(RegistrationRequestDto registerationRequestDto);
    }
}
