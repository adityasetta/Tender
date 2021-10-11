namespace Tender.Libraries.UserAuthorization.Middleware
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;

    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Tender.Domain.Repositories;
    using Tender.Shared.Settings;

    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly TenderConfigs _configs;

        public AuthenticationMiddleware(RequestDelegate next, IOptions<TenderConfigs> configs)
        {
            _next = next;
            _configs = configs.Value;
        }

        public async Task Invoke(HttpContext context, IUserRepository userRepository)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
            {
                await ValidateToken(context, userRepository, token);
            }

            await _next(context);
        }

        private async Task ValidateToken(HttpContext context, IUserRepository userRepository, string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_configs.Secret);

                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = jwtToken?.Claims?.First(x => x.Type == "id").Value;

                context.Items["User"] = await userRepository.GetUserById(userId).ConfigureAwait(false);
            }
            catch
            {
                context.Items["User"] = null;
            }
        }
    }
}
