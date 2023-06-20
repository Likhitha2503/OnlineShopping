using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using OnlineShopping_Web.Models;
using OnlineShopping_Web.Models.DTO;
using OnlineShopping_Web.Models.VM;
using OnlineShopping_Web.Services;
using OnlineShopping_Web.Services.IServices;

namespace OnlineShopping_Web.Controllers
{
	public class ProductController : Controller
	{
		private readonly IProductService _productService;
		private readonly ICategoryService  _categoryService;
		private readonly IMapper _mapper;
		public ProductController(IProductService productService, IMapper mapper, ICategoryService categoryService)
		{
			_productService = productService;
			_mapper = mapper;
			_categoryService = categoryService;
		}

		public async Task<IActionResult> IndexProduct()
		{
			List<ProductDto> list = new();

			var response = await _productService.GetAllAsync<APIResponse>();
			if (response != null && response.IsSuccess)
			{
				list = JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(response.Result));
			}
			return View(list);
		}
		public async Task<IActionResult> CreateProduct()
		{
			ProductCreateVM productVM = new();
			var response = await _categoryService.GetAllAsync<APIResponse>();
			if (response != null && response.IsSuccess)
			{
				productVM.CategoryList = JsonConvert.DeserializeObject<List<CategoryDto>>
					(Convert.ToString(response.Result)).Select(i => new SelectListItem
					{
						Text = i.Name,
						Value = i.Id.ToString()
					}); ;
			}
			return View(productVM);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> CreateProduct(ProductCreateVM model)
		{
			if (ModelState.IsValid)
			{

				var response = await _productService.CreateAsync<APIResponse>(model.Product);
				if (response != null && response.IsSuccess)
				{
					return RedirectToAction(nameof(IndexProduct));
				}
			}
			return View(model);
		}
	}
	
}

