using Microsoft.AspNetCore.Mvc;
using SumTL.BLL.DTOs;
using SumTL.BLL.ServiceAccess;
using SumTL.BLL.Services;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace SumTL.Controllers
{
    [AllowAnonymous]
    public class AuthController : Controller
    {
        private AuthService authService;
        public AuthController(IBusinessService serviceAccess)
        {
            authService = serviceAccess.AuthService;
        }

        #region Razor views

        //public IActionResult Register()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public IActionResult Register(AppUserDTO obj)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        string errorMsg;
        //        var result = authService.Register(obj, out errorMsg);
        //        if (errorMsg != null)
        //        {
        //            ViewBag.Error = errorMsg;
        //            return View();
        //        }
        //        return RedirectToAction("Index", "Home");

        //    }
        //    return NotFound();
        //}
        //[HttpGet]
        //public IActionResult Login()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public IActionResult Login(AppUserDTO obj)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        string errorMsg;
        //        var result = authService.Login(obj, out errorMsg);
        //        if (errorMsg != null)
        //        {
        //            ViewBag.Error = errorMsg;
        //            return View();
        //        }

        //    }
        //    return RedirectToAction("Index", "Home");
        //}
        //public IActionResult Logout()
        //{
        //    authService.Logout();
        //    return RedirectToAction("Index", "Home");
        //}
        #endregion

        #region API / Ajax
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(AppUserDTO obj)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { msg = "One or more field validation error!" });
            }
            string errorMsg;
            var result = authService.Register(obj, out errorMsg);
            if (errorMsg != null)
            {
                return Json(new { msg = errorMsg });
            }
            authService.Login(obj, out errorMsg);
            return Ok();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(AppUserDTO obj)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { msg = "One or more field validation error!" });
            }
            string errorMsg;
            var result = authService.Login(obj, out errorMsg);
            if (errorMsg != null)
            {
                return Json(new { msg = errorMsg });
            }
            return Ok();
        }
        [HttpPost]
        public IActionResult Logout()
        {
            authService.Logout();
            return Ok();
        }

        #endregion

    }
}
