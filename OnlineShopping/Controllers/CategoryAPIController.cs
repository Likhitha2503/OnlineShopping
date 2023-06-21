using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShopping.Models;
using OnlineShopping.Models.DTO;
using OnlineShopping_API.DataStore;
using OnlineShopping_API.Models;
using OnlineShopping_API.Repository.IRepository;
using System.Data;
using System.Net;

namespace OnlineShopping.Controllers
{
    [ApiController]
    [Route("api/CategoryAPI")]
    public class CategoryAPIController : ControllerBase
    {
        protected APIResponse _response;

        private readonly ICategoryRepository _dbCategory;
        private readonly IMapper _mapper;

        public CategoryAPIController(ICategoryRepository dbCategory, IMapper mapper)
        {
            _dbCategory = dbCategory;
            _mapper = mapper;
            this._response = new();

        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public  async Task<ActionResult<APIResponse>> GetCategories()
        {
            try
            {

                IEnumerable<Category> categoryList = await _dbCategory.GetAllAsync();
                _response.Result = _mapper.Map<List<CategoryDto>>(categoryList);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }


        [HttpGet("{id:int}",Name = "GetCategory")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]

        public async Task<ActionResult<APIResponse>> GetCategories(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var category = await _dbCategory.GetAsync(u => u.Id == id);
                if (category == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                _response.Result = _mapper.Map<CategoryDto>(category);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<APIResponse>> CreateCategory([FromBody] CategoryDto createDto)
        {
            try
            {
               
                if (await _dbCategory.GetAsync(u => u.Name.ToLower() == createDto.Name.ToLower()) != null)
                {
                    ModelState.AddModelError("ErrorMessages", "Category already Exists!");
                    return BadRequest(ModelState);
                }

                if (createDto == null)
                {
                    return BadRequest(createDto);
                }
                
                Category category = _mapper.Map<Category>(createDto);

                
                await _dbCategory.CreateAsync(category);
                _response.Result = _mapper.Map<CategoryDto>(category);
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetCategory", new { id = category.Id }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }


        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpDelete("{id:int}", Name = "DeleteCategory")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<APIResponse>> DeleteCategory(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                var category = await _dbCategory.GetAsync(u => u.Id == id);
                if (category == null)
                {
                    return NotFound();
                }
                await _dbCategory.RemoveAsync(category);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;

        }
        [HttpPut("{id:int}", Name = "UpdateCategory")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<APIResponse>> UpdateCategory(int id, [FromBody]CategoryDto updateDto)
        {
            try
            {
                if (updateDto == null || id != updateDto.Id)
                {
                    return BadRequest();
                }

                Category model = _mapper.Map<Category>(updateDto);

                await _dbCategory.UpdateAsync(model);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }


    }
}
