using Microsoft.AspNetCore.Mvc;
using SumTL.BLL.DTOs;
using SumTL.BLL.ServiceAccess;
using SumTL.BLL.Services;

namespace SumTL.Controllers
{
    public class AuthController : Controller
    {
        private AuthService authService;
        public AuthController(IBusinessService serviceAccess)
        {
            authService = serviceAccess.AuthService;
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(AppUserDTO obj)
        {
            if (ModelState.IsValid)
            {
                string errorMsg;
                var result = authService.Register(obj, out errorMsg);
                if (errorMsg != null)
                {
                    ViewBag.Error = errorMsg;
                }
                return View();
            }
            return NotFound();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

    }
}
