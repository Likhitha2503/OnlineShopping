using OnlineShopping_Utility;
using OnlineShopping_Web.Models;
using OnlineShopping_Web.Models.DTO;
using OnlineShopping_Web.Services.IServices;

namespace OnlineShopping_Web.Services
{
    public class ProductService : BaseService, IProductService
	{
        private readonly IHttpClientFactory _clientFactory;
        private string categoryUrl;

        public ProductService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory = clientFactory;
            categoryUrl = configuration.GetValue<string>("ServiceUrls:OnlineShoppingAPI");

        }

        public Task<T> CreateAsync<T>(ProductDto dto,string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = categoryUrl + "/api/ProductAPI",Token = token
			});
        }


        public Task<T> DeleteAsync<T>(int id,string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Url = categoryUrl + "/api/ProductAPI/" + id,Token = token
            });
        }

        public Task<T> GetAllAsync<T>(string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = categoryUrl + "/api/ProductAPI", Token = token    
			});
        }

        public Task<T> GetAsync<T>(int id,string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = categoryUrl + "/api/ProductAPI/" + id, Token = token
            });
        }

        public Task<T> UpdateAsync<T>(ProductDto dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = categoryUrl + "/api/ProductAPI/" + dto.Id, Token = token
            });
        }


    }
}

