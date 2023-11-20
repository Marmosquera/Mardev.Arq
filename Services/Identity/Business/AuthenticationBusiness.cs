using Mardev.Arq.Services.Identity.Business.Models;
using Mardev.Arq.Shared.Contracts;

namespace Mardev.Arq.Services.Identity.Business
{
    public interface IAuthenticationBusiness
    {
        LoginResult Login(string username, string password);
    }

    public class AuthenticationBusiness : IAuthenticationBusiness
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AuthenticationBusiness(IJwtTokenGenerator jwtTokenGenerator)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public LoginResult Login(string username, string password)
        {
            var result = new LoginResult();
            if (username == "p" && password == "p")
            {
                var appUser = new ApplicationUser
                {
                    UserId = "100",
                    Name = "UserName",
                    Email = "m@m.com",
                    Roles = new List<string> { "WebApp" }
                };
                result.UserId = appUser.UserId;
                result.Token = _jwtTokenGenerator.GenerateToken(appUser);
            }
            return result;
        }
    }
}
