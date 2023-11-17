using AutoMapper;
using Mardev.Arq.Services.Identity.Business;
using Mardev.Arq.Services.Identity.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Mardev.Arq.Services.Identity.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationBusiness _authenticationBusiness;
        private readonly IMapper _mapper;
        private readonly ILogger<AuthController> _logger;

        public AuthController(
            IAuthenticationBusiness authenticationBusiness,
            IMapper mapper,
            ILogger<AuthController> logger)
        {
            _authenticationBusiness = authenticationBusiness;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        [SwaggerResponse(StatusCodes.Status200OK, "Login result", typeof(AuthLoginResponse))]
        public IActionResult Login([FromBody] AuthLoginRequest authloginRequest)
        {
            var result = _authenticationBusiness.Login(authloginRequest.Username, authloginRequest.Password);
            var response = _mapper.Map<AuthLoginResponse>(result);
            return Ok(response);
        }
    }
}
