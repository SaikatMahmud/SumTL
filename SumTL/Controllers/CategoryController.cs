using Microsoft.AspNetCore.Mvc;
using SumTL.BLL.DTOs;
using SumTL.BLL.ServiceAccess;
using SumTL.BLL.Services;

namespace SumTL.Controllers
{
    public class CategoryController : Controller
    {
        private CategoryService categoryService;
        private ItemService itemService;
        public CategoryController(IBusinessService serviceAccess)
        {
            categoryService = serviceAccess.CategoryService;
            itemService = serviceAccess.ItemService;
        }
        //public IActionResult Index()
        //{
        //    List<CategoryItemDTO> data = categoryService.Get();
        //    var i = 0;
        //    return View();
        //}
        //public IActionResult Index()
        //{
        //    List<ItemCategoryDTO> data = itemService.Get();
        //    var i = 0;
        //    return View();
        //}

        public IActionResult Index()
        {
            ItemCategoryDTO data = itemService.Get(it => it.Id == 3);
            var i = 0;
            return View();
        }
    }
}
