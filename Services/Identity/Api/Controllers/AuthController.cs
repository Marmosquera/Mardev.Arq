using Mardev.Arq.Services.Identity.Business;
using Mardev.Arq.Services.Identity.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Mardev.Arq.Services.Identity.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationBusiness _authenticationBusiness;
        private readonly ILogger<AuthController> _logger;

        public AuthController(
            IAuthenticationBusiness authenticationBusiness,
            ILogger<AuthController> logger)
        {
            _authenticationBusiness = authenticationBusiness;
            _logger = logger;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody]AuthLoginRequest authloginRequest)
        {
            var result = _authenticationBusiness.Login(authloginRequest.Username, authloginRequest.Password);
            return Ok(result);
        }
    }
}
