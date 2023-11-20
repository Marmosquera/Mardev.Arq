using Microsoft.AspNetCore.Authentication.Cookies;
using System.Text;

namespace Mardev.Arq.Front.Web.Installers
{
    public static class AuthenticationInstaller
    {
        public static void AddAuthentication(this WebApplicationBuilder builder)
        {
            var config = builder.Configuration.GetRequiredSection(nameof(AuthOptions)).Get<AuthOptions>();

            var key = Encoding.ASCII.GetBytes(Repeat(config?.Jwt?.ClientSecret, 32));


            builder.Services
                /*.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(o => o.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = config?.Jwt?.Issuer,
                    ValidAudience = config?.Jwt?.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                });*/
                .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.ExpireTimeSpan = TimeSpan.FromHours(10);
                    options.LoginPath = "/Auth/Login";
                    options.AccessDeniedPath = "/Auth/AccessDenied";
                });
        }

        private static string Repeat(string? key, int length)
        {
            key ??= "a";
            while (key.Length < length)
            {
                key += key;
            }
            return key[..length];
        }

    }

    public record AuthOptions
    {
        public AuthJwtOptions Jwt { get; init; } = default!;
    }
    public record AuthJwtOptions
    {
        public string Issuer { get; init; } = default!;
        public string Audience { get; init; } = default!;
        public string JwksEndpoint { get; init; } = default!;
        public bool ValidateLifetime { get; init; } = true;
        public string ClientId { get; init; } = default!;
        public string ClientSecret { get; init; } = default!;
    }
}
