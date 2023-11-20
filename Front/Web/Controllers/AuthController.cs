using Mardev.Arq.Front.Web.Models;
using Mardev.Arq.Front.Web.Services.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

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
                var handler = new JwtSecurityTokenHandler();
                var jwt = handler.ReadJwtToken(response.Result.Token);
                var identity = new ClaimsIdentity(jwt.Claims, CookieAuthenticationDefaults.AuthenticationScheme, JwtRegisteredClaimNames.Name, ClaimTypes.Role);
                var principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["error"] = response.Message;
                return View(model);
            }
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            Response.Cookies.Delete(TokenCookie);
            return RedirectToAction("Index", "Home");
        }

    }
}
