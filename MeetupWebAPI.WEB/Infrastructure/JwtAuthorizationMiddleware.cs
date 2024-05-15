using MeetupWebAPI.DAL.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace MeetupWebAPI.WEB.Infrastructure
{
    public class JwtAuthorizationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly SymmetricSecurityKey _key;

        public JwtAuthorizationMiddleware(RequestDelegate next, IConfiguration config)
        {
            _next = next;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
        }

        public async Task Invoke(HttpContext context, IUserRepository userRepo)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
            {
                await AttachUserToContext(context, userRepo, token);
            }

            await _next(context);
        }

        private async Task AttachUserToContext(HttpContext context, IUserRepository userRepo, string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = _key,
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken securityToken);

            var jwtToken = (JwtSecurityToken)securityToken;
            var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "Id").Value);
            context.Items["User"] = await userRepo.GetUserById(userId);
        }
    }
}
