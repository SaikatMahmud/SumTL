using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SumTL.BLL.DTOs;
using SumTL.BLL.ServiceAccess;
using SumTL.BLL.Services;

namespace SumTL.Controllers
{
     [Authorize(Roles = "General, Admin")]
    public class ItemController : Controller
    {
        private CategoryService categoryService;
        private ItemService itemService;
        private readonly IWebHostEnvironment webHostEnvironment;
        public ItemController(IBusinessService serviceAccess, IWebHostEnvironment _webHostEnvironment)
        {
            categoryService = serviceAccess.CategoryService;
            itemService = serviceAccess.ItemService;
            webHostEnvironment = _webHostEnvironment;
        }
        #region Razor views
        //public IActionResult GetAll()
        //{
        //    var data = itemService.Get("Category");
        //    return View(data);
        //}
        //public IActionResult Get(int id)
        //{
        //    var data = itemService.Get(c => c.Id == id);
        //    return View(data);
        //}
        //[HttpGet]
        //public IActionResult Edit(int id)
        //{
        //    var data = itemService.Get(c => c.Id == id);
        //    IEnumerable<SelectListItem> catList = categoryService.Get().Select(c => new SelectListItem
        //    {
        //        Text = c.CategoryName,
        //        Value = c.Id.ToString(),
        //    });
        //    ViewBag.catList = catList;
        //    return View(data);
        //}
        //[HttpPost]
        //public IActionResult Edit(ItemDTO ct)
        //{
        //    IEnumerable<SelectListItem> catList = categoryService.Get().Select(c => new SelectListItem
        //    {
        //        Text = c.CategoryName,
        //        Value = c.Id.ToString(),
        //    });
        //    ViewBag.catList = catList;
        //    if (!ModelState.IsValid)
        //    {
        //        return View(ct);
        //    }
        //    var result = itemService.Update(ct);
        //    TempData["msg"] = result ? "Updated successfully!" : "Error! Not Updated";
        //    return !result ? View() : RedirectToAction("GetAll");
        //}
        //[HttpGet]
        //public IActionResult Create()
        //{
        //    IEnumerable<SelectListItem> catList = categoryService.Get().Select(c => new SelectListItem
        //    {
        //        Text = c.CategoryName,
        //        Value = c.Id.ToString(),
        //    });
        //    ViewBag.catList = catList;
        //    return View();
        //}
        //[HttpPost]
        //public IActionResult Create(ItemDTO ct)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        IEnumerable<SelectListItem> catList = categoryService.Get().Select(c => new SelectListItem
        //        {
        //            Text = c.CategoryName,
        //            Value = c.Id.ToString(),
        //        });
        //        ViewBag.catList = catList;
        //        return View(ct);
        //    }
        //    var result = itemService.Create(ct);
        //    TempData["msg"] = result ? "Added successfully!" : "Error! Could not added";
        //    return !result ? View() : RedirectToAction("GetAll");
        //}

        //public IActionResult Delete(int id)
        //{
        //    var result = itemService.Delete(id);
        //    TempData["msg"] = result ? "Deleted successfully!" : "Error! Could not deleted";
        //    return !result ? View("ErrorView") : RedirectToAction("GetAll");
        //}
        #endregion

        #region API / Ajax
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var data = itemService.Get("Category");
            return Json(new { data = data });
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var data = itemService.Get(i => i.Id == id, "Images");
            if (data == null) return NotFound();
            IEnumerable<SelectListItem> catList = categoryService.Get().Select(c => new SelectListItem
            {
                Text = c.CategoryName,
                Value = c.Id.ToString(),
            });
            ViewBag.catList = catList;
            return View(data);
        }
        [HttpPut]
        public IActionResult Edit(ItemDTO it)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { msg = "One or more field validation error!" });
            }
            var result = itemService.Update(it);
            return Ok();
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var result = itemService.Delete(id);
            if (!result) return Json(new { msg = "Delete failed. Category not found!" });
            return Ok();
        }

        [HttpGet]
        public IActionResult Create()
        {
            IEnumerable<SelectListItem> catList = categoryService.Get().Select(c => new SelectListItem
            {
                Text = c.CategoryName,
                Value = c.Id.ToString(),
            });
            ViewBag.catList = catList;
            return View();
        }
        [HttpPost]
        public IActionResult Create(ItemDTO it)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { msg = "One or more field validation error!" });
            }
            var result = itemService.Create(it);
            return Ok();
        }
        [HttpPost]
        [Route("Item/ImageUpload/{id}")]
        public IActionResult ImageUpload(int id, IFormFile file)
        {
            if (id == 0 || file == null) return Json(new { msg = "No file provided or invalid item!" });
            var data = itemService.Get(i => i.Id == id);
            if (data == null) return Json(new { msg = "Invalid item!" });
            string wwwRootPath = webHostEnvironment.WebRootPath;
            string imagePath = Path.Combine(wwwRootPath, @"uploads\images");
            string imageName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            using (var fileStream = new FileStream(Path.Combine(imagePath, imageName), FileMode.Create))
            {
                file.CopyTo(fileStream);
            }
            itemService.UploadImage(id, @"\uploads\images\" + imageName);
            return Ok();
        }


        [HttpDelete]
        // [Route("Item/ImageUpload/{id}")]
        public IActionResult DeleteImage(int id)
        {
            var imageUrl = itemService.DeleteImage(id);
            var imagePath = Path.Combine(webHostEnvironment.WebRootPath, imageUrl.TrimStart('\\')); // remove leading backslash
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> UploadItemBulk(IFormFile file)
        {
            if (file == null) return Json(new { msg = "No file provided!" });
            var result = await itemService.UploadFromExcel(file.OpenReadStream());
            if (result) return Ok();
            return Json(new { msg = "Error! Could not upload items!" });
        }

        [HttpGet]
        public IActionResult GetAllCustomized(int draw, int start, int length, string search)
        {
            int totalCount = 0;
            int filteredCount = 0;
            List<ItemDTO> result;
            if (string.IsNullOrEmpty(search))
            {
                result = itemService.GetCustomized(start, length, out totalCount, out filteredCount, "Category");
            }
            else
            {
                result = itemService.GetCustomized(c => c.ItemName.Contains(search), start, length, out totalCount, out filteredCount, "Category");
            }
            var response = new
            {
                draw = draw,
                recordsTotal = totalCount,
                recordsFiltered = filteredCount,
                data = result
            };
            return Json(response);
        }

        #endregion
    }
}
