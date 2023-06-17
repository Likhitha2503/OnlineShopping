using Microsoft.AspNetCore.Mvc;
using OnlineShopping.DataStore;
using OnlineShopping.Models;
using OnlineShopping.Models.DTO;

namespace OnlineShopping.Controllers
{
    [ApiController]
    [Route("api/CategoryAPI")]
    public class CategoryAPIController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<CategoryDto>> GetCategories()
        {
            return Ok(CategoryStore.CategoryList);
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

            var category = CategoryStore.CategoryList.FirstOrDefault(u => u.Id == id);
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
            if (CategoryStore.CategoryList.FirstOrDefault(u=>u.Name.ToLower()== categoryDto.Name.ToLower()) != null)
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
            categoryDto.Id = CategoryStore.CategoryList.OrderByDescending(u => u.Id).FirstOrDefault().Id+1;
            CategoryStore.CategoryList.Add(categoryDto);

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
            var category = CategoryStore.CategoryList.FirstOrDefault(u => u.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            CategoryStore.CategoryList.Remove(category);
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
            var category = CategoryStore.CategoryList.FirstOrDefault(u => u.Id == id);
            category.Name = categoryDto.Name;
            

            return NoContent();

        }


    }
}
