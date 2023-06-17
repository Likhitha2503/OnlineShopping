using Microsoft.AspNetCore.Mvc;
using OnlineShopping.DataStore;
using OnlineShopping.Models;
using OnlineShopping.Models.DTO;
using OnlineShopping_API.DataStore;

namespace OnlineShopping.Controllers
{
    [ApiController]
    [Route("api/CategoryAPI")]
    public class CategoryAPIController : ControllerBase
    {

        private readonly ApplicationDbContext _db;
        public CategoryAPIController(ApplicationDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<CategoryDto>> GetCategories()
        {
            return Ok(_db.Categories.ToList());
        }


        [HttpGet("{id:int}",Name ="GetCategory")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public ActionResult<CategoryDto> GetCategories(int id)
        {
            if(id == 0)
            {
                return BadRequest();
            }

            var category = _db.Categories.FirstOrDefault(u => u.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<CategoryDto> CreateVilla([FromBody] CategoryDto categoryDto)
        { 
            if (_db.Categories.FirstOrDefault(u=>u.Name.ToLower()== categoryDto.Name.ToLower()) != null)
            {
                ModelState.AddModelError("CustomeError", "Category already Exist");
                return BadRequest(ModelState);

            }
            if (categoryDto == null)
            {
                return BadRequest(categoryDto);
            }
            if (categoryDto.Id > 0) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            Category model = new()
            {
                Id = categoryDto.Id,
                ImageUrl = categoryDto.ImageUrl,
                Name = categoryDto.Name

            };
            _db.Categories.Add(model);
            _db.SaveChanges();

            return CreatedAtRoute("GetCategory",new { id = categoryDto.Id },categoryDto);
        }


        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("{id:int}", Name = "DeleteCategory")]
        public IActionResult DeleteCategory(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var category = _db.Categories.FirstOrDefault(u => u.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            _db.Categories.Remove(category);
            _db.SaveChanges();
            return NoContent();

        }
        [HttpPut("{id:int}", Name = "UpdateCategory")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateCategory(int id, [FromBody]CategoryDto categoryDto)
        {
            if (categoryDto == null || id != categoryDto.Id)
            {
                return BadRequest();
            }
            Category model = new()
            {
                Id = categoryDto.Id,
                ImageUrl = categoryDto.ImageUrl,
                Name = categoryDto.Name

            };
            _db.Categories.Update(model);
            _db.SaveChanges();

            return NoContent();

        }


    }
}
