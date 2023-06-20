using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineShopping_Web.Models.DTO;

namespace OnlineShopping_Web.Models.VM
{
	public class ProductUpdateVM
	{
		public ProductUpdateVM()
		{
			Product = new ProductDto();
		}
		public ProductDto Product { get; set; }
		[ValidateNever]
		public IEnumerable<SelectListItem> CategoryList { get; set; }
	}
}
