using App.Business.Abstract;
using App.Entities.Models;
using ECommerce.WebUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private IProductService _productService;
        private ICategoryService _categoryService;

        public ProductController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }
        public static bool FilterState { get; set; } = true;
        public IActionResult Index(int page = 1, int category=0, bool higherToLower = false)
        {
            int pageSize = 10;
            var products = _productService.GetAllByCategory(category);
            //products=_productService.GetAllByFilterAZ(products, filterAZ);
            products = _productService.GetAllByPriceFilter(products, higherToLower);
            FilterState = !FilterState;
            var model = new ProductListViewModel
            {
                HigherToLower = FilterState,
                //CurrentFilterState= FilterState,
                Products = products.Skip((page-1)*pageSize).Take(pageSize).ToList(),
                CurrentCategory = category,
                PageCount=(int)Math.Ceiling(products.Count/(double)pageSize),
                PageSize=pageSize,
                CurrentPage=page
            };
            return View(model);
        }

        
   


    }
}
