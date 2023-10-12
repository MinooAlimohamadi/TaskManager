using Microsoft.AspNetCore.Mvc;
using TaskManager.Logic.Service.Authentication;
using TaskManager.Logic.Service.Authentication.Dto;

namespace TaskManager.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IUserService _userService;
        private const string SessionName = "UserId";

        public AuthenticationController(IUserService userService)
        {
            this._userService = userService;
        }

        public IActionResult Login()
        {
            HttpContext.Session.Clear();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Login_dto view)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.Login(view);
                if (result == null || string.IsNullOrEmpty(result.UserName))
                {
                    ModelState.AddModelError("Password", "Invalid login attempt.");
                    return View(view);
                }

                HttpContext.Session.SetInt32(SessionName, result.Id);

                return RedirectToAction("Index", "Home");
            }
            return View(view);
        }

        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(User_dtod view)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.POST(view);

                return RedirectToAction("Login");
            }
            return View(view);
        }

        public IActionResult UserList()
        {
            var result = _userService.GET().ToList();

            return View(result);
        }

        public async Task<ActionResult> Edit(int id)
        {
            var result = await _userService.GET(id);

            return View(result);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(User_dtod view)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.PUT(view);

                return RedirectToAction("UserList");
            }

            return View(view);
        }

        public async Task<ActionResult> Delete(int id)
        {
            var current = HttpContext!.Session.GetInt32("UserId");
            if (current != null)
            {
                if (current == id)
                {
                    return RedirectToAction("UserList");
                }
            }
            var result = await _userService.DELETE(id);

            return RedirectToAction("UserList");
        }
    }
}
