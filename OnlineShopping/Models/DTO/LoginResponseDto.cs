using OnlineShopping_API.Models;

namespace OnlineShopping_Web.Models.DTO
{
    public class LoginResponseDto
    {
        public LocalUser User { get; set; }
        public string Token { get; set; }

    }
}
