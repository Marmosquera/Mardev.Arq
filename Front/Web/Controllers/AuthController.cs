using Mardev.Arq.Front.Web.Models;
using Mardev.Arq.Front.Web.Services.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Mardev.Arq.Front.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IIdentityService _identityService;
        private const string TokenCookie = "JWTToken";

        public AuthController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            LoginRequest model = new();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest model)
        {
            var response = await _identityService.Login(new Arq.Services.Identity.Contracts.AuthLoginRequest { Username = model.UserName, Password = model.Password });

            if (response.IsSuccess && !string.IsNullOrEmpty(response.Result?.Token))
            {
                Response.Cookies.Append(TokenCookie, response.Result.Token);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["error"] = response.Message;
                return View(model);
            }
        }

    }
}
