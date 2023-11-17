using Mardev.Arq.Services.Identity.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                result.UserId = "100";
                result.Token = _jwtTokenGenerator.GenerateToken("100", new List<string> { "WebApp" });
            }
            return result;
        }
    }
}
