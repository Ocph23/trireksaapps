using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using WebApi.Middlewares;
using WebApi.Models;

namespace WebApi
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AppSettings _appSettings;

        public JwtMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings)
        {
            _next = next;
            _appSettings = appSettings.Value;
        }

        public async Task Invoke(HttpContext context, IUserService userService)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
            {
                await attachUserToContext(context, userService, token);
            }

            await _next(context);
        }

        private async Task attachUserToContext(HttpContext context, IUserService userService, string token)
        {
            try
            {
                await Task.Delay(100);
                var tokenHandler = new JwtSecurityTokenHandler();
                SecurityToken validatedToken = null;
                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                string id = jwtToken.Claims.First(x => x.Type == "id").Value;
                var name = jwtToken.Claims.First(x => x.Type == "name").Value;
                var roles = jwtToken.Claims.First(x => x.Type == "roles").Value;
                var claims = new[]
                {
                               new Claim(ClaimTypes.NameIdentifier, id),
                               new Claim(ClaimTypes.Name, name),
                               new Claim(ClaimTypes.Role, roles),
                };
                var identity = new ClaimsIdentity(claims, "Bearer");

                var user = await userService.GetById(id);

                context.User = new ClaimsPrincipal(identity);
                if (user != null)
                    context.Items["User"] = user;
            }
            catch
            {
                //context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
            }
        }
    }
}