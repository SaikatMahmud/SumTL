using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using SumTL.BLL.DTOs;
using SumTL.BLL.ServiceAccess;
using SumTL.BLL.Services;
using System.Text.Json;

namespace SumTL.Controllers
{
    public class CategoryController : Controller
    {
        private CategoryService categoryService;
        //private ItemService itemService;
        public CategoryController(IBusinessService serviceAccess)
        {
            categoryService = serviceAccess.CategoryService;
            //itemService = serviceAccess.ItemService;
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

        //public IActionResult Index()
        //{
        //    ItemDTO data = itemService.Get(it => it.Id == 3);
        //    var i = 0;
        //    return View();
        //}
        //#region MVC
        //public IActionResult GetAll()
        //{
        //    var data = categoryService.Get();
        //    return View(data);
        //}
        //public IActionResult Get(int id)
        //{
        //    var data = categoryService.Get(c => c.Id == id);
        //    return View(data);
        //}
        //[HttpGet]
        //public IActionResult Edit(int id)
        //{
        //    var data = categoryService.Get(c => c.Id == id);
        //    return View(data);
        //}
        //[HttpPost]
        //public IActionResult Edit(CategoryDTO ct)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View();
        //    }
        //    var result = categoryService.Update(ct);
        //    TempData["msg"] = result ? "Updated successfully!" : "Error! Not Updated";
        //    return !result ? View() : RedirectToAction("GetAll");
        //}
        //[HttpGet]
        //public IActionResult Create()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public IActionResult Create(CategoryDTO ct)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View();
        //    }
        //    var result = categoryService.Create(ct);
        //    TempData["msg"] = result ? "Added successfully!" : "Error! Could not added";
        //    return !result ? View() : RedirectToAction("GetAll");
        //}

        //public IActionResult Delete(int id)
        //{
        //    var result = categoryService.Delete(id);
        //    TempData["msg"] = result ? "Deleted successfully!" : "Error! Could not deleted";
        //    return !result ? View("ErrorView") : RedirectToAction("GetAll");
        //}
        //#endregion

        #region API
        public IActionResult Index()
        {
            return View();
        }
        public JsonResult GetAll()
        {
            var data = categoryService.Get();
            return Json(new { result = data });
        }
        

        #endregion
    }
}
