
using ASPLoanMSC103.Model;
using ASPLoanMSC103.Services;
using Microsoft.AspNetCore.Mvc;

namespace ASPLoanMSC103.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [ActionName("Login")]
        public IActionResult Authenticated(string? returnUrl)
        {
            returnUrl ??= Url.Content("~/");
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [ActionName("Login")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Authenticated(LoginRequest req, string? ReturnUrl = null)
        {
            ReturnUrl ??= Url.Content("~/");
            ViewBag.ReturnUrl = ReturnUrl;
            if (!ModelState.IsValid)
            {
                return View(req);
            }

            var loginResult = await _userService.GetAuthenticatedUser(req);

            if (!loginResult.IsSuccess)
            {
                ModelState.AddModelError("", loginResult.Message);
                return View(req);
            }
            return RedirectToAction("Index", "Employee");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterRequest req)
        {
            if (!ModelState.IsValid)
            {
                return View(req);
            }

            var createResult = await _userService.CreateUser(req);
            if (!createResult.IsSuccess)

            {
                ModelState.AddModelError("", createResult.Message);
                return View(req);
            }

            return RedirectToAction("Login");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            if (await _userService.LogUserOut())
            {
                return RedirectToAction("Login");
            }

            return BadRequest();
        }

        public ActionResult AccessDenied() { return View(); }
    }
}