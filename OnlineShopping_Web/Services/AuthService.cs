using OnlineShopping_Utility;
using OnlineShopping_Web.Models.DTO;
using OnlineShopping_Web.Models;
using OnlineShopping_Web.Services.IServices;

namespace OnlineShopping_Web.Services
{
    public class AuthService : BaseService, IAuthService
    {
        private readonly IHttpClientFactory _clientFactory;
        private string villaUrl;

        public AuthService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory = clientFactory;
            villaUrl = configuration.GetValue<string>("ServiceUrls:OnlineShoppingAPI");

        }

        public Task<T> LoginAsync<T>(LoginRequestDto obj)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = obj,
                Url = villaUrl + "/api/UsersAuth/login"
            });
        }


        public Task<T> RegisterAsync<T>(RegistrationRequestDto obj)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = obj,
                Url = villaUrl + "/api/UsersAuth/register"
            });
        }

       
    } 
    

    
}
