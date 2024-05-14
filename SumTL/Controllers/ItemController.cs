using Microsoft.AspNetCore.Mvc;
using SumTL.BLL.DTOs;
using SumTL.BLL.ServiceAccess;
using SumTL.BLL.Services;

namespace SumTL.Controllers
{
    public class ItemController : Controller
    {
        //private CategoryService categoryService;
        private ItemService itemService;
        public ItemController(IBusinessService serviceAccess)
        {
            //categoryService = serviceAccess.CategoryService;
            itemService = serviceAccess.ItemService;
        }
        public IActionResult GetAll()
        {
            var data = itemService.Get("Category");
            return View(data);
        }
        public IActionResult Get(int id)
        {
            var data = itemService.Get(c => c.Id == id);
            return View(data);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var data = itemService.Get(c => c.Id == id);
            return View(data);
        }
        [HttpPost]
        public IActionResult Edit(ItemDTO ct)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var result = itemService.Update(ct);
            TempData["msg"] = result ? "Updated successfully!" : "Error! Not Updated";
            return !result ? View() : RedirectToAction("GetAll");
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(ItemDTO ct)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var result = itemService.Create(ct);
            TempData["msg"] = result ? "Added successfully!" : "Error! Could not added";
            return !result ? View() : RedirectToAction("GetAll");
        }

        public IActionResult Delete(int id)
        {
            var result = itemService.Delete(id);
            TempData["msg"] = result ? "Deleted successfully!" : "Error! Could not deleted";
            return !result ? View("ErrorView") : RedirectToAction("GetAll");
        }
    }
}
