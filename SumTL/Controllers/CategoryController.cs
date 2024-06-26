﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using SumTL.BLL.DTOs;
using SumTL.BLL.ServiceAccess;
using SumTL.BLL.Services;
using System.Net;
using System.Text.Json;

namespace SumTL.Controllers
{
    [Authorize(Roles = "Admin")]
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

        #region Razor views
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
        #endregion

        #region API / Ajax
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetAll()
        {
            var data = categoryService.Get();
            return Json(new { data = data });
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var data = categoryService.Get(c => c.Id == id);
            return View(data);
        }
        [HttpPut]
        public IActionResult Edit(CategoryDTO ct)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { msg = "One or more field validation error!" });
            }
            var result = categoryService.Update(ct);
            return Ok();
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var result = categoryService.Delete(id);
            if (!result) return Json(new { msg = "Delete failed. Category not found!" });
            return Ok();
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CategoryDTO ct)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { msg = "One or more field validation error!" });
            }
            var result = categoryService.Create(ct);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> UploadCategoryBulk(IFormFile file)
        {
            if (file == null) return Json(new { msg = "No file provided!" });
            var result = await categoryService.UploadFromExcel(file.OpenReadStream());
            if (result) return Ok();
            return Json(new { msg = "Error! Could not upload items!" });
        }

        [HttpGet]
        public IActionResult GetAllCustomized(int draw, int start, int length, string search)
        {
            int totalCount = 0;
            int filteredCount = 0;
            List<CategoryDTO> result;
            if (string.IsNullOrEmpty(search))
            {
                result = categoryService.GetCustomized(start, length, out totalCount, out filteredCount);
            }
            else
            {
                result = categoryService.GetCustomized(c => c.CategoryName.Contains(search), start, length, out totalCount, out filteredCount);
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
