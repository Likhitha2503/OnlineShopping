using Microsoft.AspNetCore.Mvc;
using OnlineShopping_API.Models;
using OnlineShopping_API.Repository.IRepository;
using OnlineShopping_Web.Models.DTO;
using System.Net;

namespace OnlineShopping_API.Controllers
{
        [Route("api/UsersAuth")]
        [ApiController]
        public class UsersController : Controller
        {
            private readonly IUserRepository _userRepo;
            protected APIResponse _response;
            public UsersController(IUserRepository userRepo)
            {
                _userRepo = userRepo;
                this._response = new();
            }

            [HttpPost("login")]
            public async Task<IActionResult> Login([FromBody] LoginRequestDto model)
            {
                var loginResponse = await _userRepo.Login(model);
                if (loginResponse.User == null || string.IsNullOrEmpty(loginResponse.Token))
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorMessages.Add("Email or password is incorrect");
                    return BadRequest(_response);
                }
                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                _response.Result = loginResponse;
                return Ok(_response);
            }

            [HttpPost("register")]
            public async Task<IActionResult> Register([FromBody] RegistrationRequestDto model)
            {
                bool ifEmailUnique = _userRepo.IsUniqueUser(model.Email);
                if (!ifEmailUnique)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorMessages.Add("Email already exists");
                    return BadRequest(_response);
                }

                var user = await _userRepo.Register(model);
                if (user == null)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorMessages.Add("Error while registering");
                    return BadRequest(_response);
                }
                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                return Ok(_response);
            }
        }
      
    
}
