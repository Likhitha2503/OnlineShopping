using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using OnlineShopping_Utility;
using OnlineShopping_Web.Models;
using OnlineShopping_Web.Models.DTO;
using OnlineShopping_Web.Models.VM;
using OnlineShopping_Web.Services;
using OnlineShopping_Web.Services.IServices;
using System.Data;

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

			var response = await _productService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
			if (response != null && response.IsSuccess)
			{
				list = JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(response.Result));
            }
			return View(list);
		}
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateProduct()
		{
			ProductCreateVM productVM = new();
			var response = await _categoryService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateProduct(ProductCreateVM model)
		{
			if (ModelState.IsValid)
			{

                var response = await _productService.CreateAsync<APIResponse>(model.Product, HttpContext.Session.GetString(SD.SessionToken));
				if (response != null && response.IsSuccess)
				{
                    TempData["success"] = "Product created successfully";

                    return RedirectToAction(nameof(IndexProduct));
				}
                else
                {
                    if (response.ErrorMessages.Count > 0)
                    {
                        ModelState.AddModelError("ErrorMessages", response.ErrorMessages.FirstOrDefault());
                    }
                }
                var resp = await _categoryService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
                if (resp != null && resp.IsSuccess)
                {
                    model.CategoryList = JsonConvert.DeserializeObject<List<CategoryDto>>
                        (Convert.ToString(resp.Result)).Select(i => new SelectListItem
                        {
                            Text = i.Name,
                            Value = i.Id.ToString()
                        }); ;
                }
            }
            return View(model);
		}

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateProduct(int Id)
        {
            ProductUpdateVM productVM = new();
            var response = await _productService.GetAsync<APIResponse>(Id, HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                ProductDto model = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
                productVM.Product = _mapper.Map<ProductDto>(model);
            }

            response = await _categoryService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                productVM.CategoryList = JsonConvert.DeserializeObject<List<CategoryDto>>
                    (Convert.ToString(response.Result)).Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    });
                return View(productVM);
            }


            return NotFound();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateProduct(ProductUpdateVM model)
        {
            if (ModelState.IsValid)
            {
                TempData["success"] = "Product updated successfully";

                var response = await _productService.UpdateAsync<APIResponse>(model.Product, HttpContext.Session.GetString(SD.SessionToken));
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(IndexProduct));
                }
                else
                {
                    if (response.ErrorMessages.Count > 0)
                    {
                        ModelState.AddModelError("ErrorMessages", response.ErrorMessages.FirstOrDefault());
                    }
                }
            }

            var resp = await _categoryService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (resp != null && resp.IsSuccess)
            {
                model.CategoryList = JsonConvert.DeserializeObject<List<CategoryDto>>
                    (Convert.ToString(resp.Result)).Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    }); ;
            }
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteProduct(int Id)
        {
            ProductDeleteVM productVM = new();
            var response = await _productService.GetAsync<APIResponse>(Id, HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                ProductDto model = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
                productVM.Product = model;
            }

            response = await _categoryService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                productVM.CategoryList = JsonConvert.DeserializeObject<List<CategoryDto>>
                    (Convert.ToString(response.Result)).Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    });
                return View(productVM);
            }


            return NotFound();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteProduct(ProductDeleteVM model)
        {

            var response = await _productService.DeleteAsync<APIResponse>(model.Product.Id, HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Product deleted successfully";

                return RedirectToAction(nameof(IndexProduct));
            }


            return View(model);
        }
    }

	
}

