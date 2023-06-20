using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OnlineShopping_Web.Models;
using OnlineShopping_Web.Models.DTO;
using OnlineShopping_Web.Services;
using OnlineShopping_Web.Services.IServices;

namespace OnlineShopping_Web.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        public CategoryController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        public async Task<IActionResult> IndexCategory()
        {
            List<CategoryDto> list = new();

            var response = await _categoryService.GetAllAsync<APIResponse>();
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<CategoryDto>>(Convert.ToString(response.Result));
            }
            return View(list);
        }

		public async Task<IActionResult> CreateCategory()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> CreateCategory(CategoryDto model)
		{
			if (ModelState.IsValid)
			{

				var response = await _categoryService.CreateAsync<APIResponse>(model);
				if (response != null && response.IsSuccess)
				{
                    TempData["success"] = "Category created successfully";
                    return RedirectToAction(nameof(IndexCategory));
				}
			}
            TempData["error"] = "Error encountered.";
            return View(model);
		}

        public async Task<IActionResult> UpdateCategory(int categoryId)
        {
            var response = await _categoryService.GetAsync<APIResponse>(categoryId);
            if (response != null && response.IsSuccess)
            {
                CategoryDto model = JsonConvert.DeserializeObject<CategoryDto>(Convert.ToString(response.Result));
                return View(_mapper.Map<CategoryDto>(model));
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateCategory(CategoryDto model)
        {
            if (ModelState.IsValid)
            {
                TempData["success"] = "Category updated successfully";

                var response = await _categoryService.UpdateAsync<APIResponse>(model);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(IndexCategory));
                }
            }
            TempData["error"] = "Error encountered.";
            return View(model);
        }

        public async Task<IActionResult> DeleteCategory(int categoryId)
        {
            var response = await _categoryService.GetAsync<APIResponse>(categoryId);
            if (response != null && response.IsSuccess)
            {
                CategoryDto model = JsonConvert.DeserializeObject<CategoryDto>(Convert.ToString(response.Result));
                return View(model);
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCategory(CategoryDto model)
        {

            var response = await _categoryService.DeleteAsync<APIResponse>(model.Id);
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Category deleted successfully";
                return RedirectToAction(nameof(IndexCategory));
            }

            return View(model);
        }



    }
}
