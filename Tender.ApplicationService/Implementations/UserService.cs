namespace Tender.ApplicationService.Implementations
{

    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;

    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;

    using Tender.ApplicationService.Interfaces;
    using Tender.Domain.Entities;
    using Tender.Domain.Repositories;
    using Tender.Shared.Requests;
    using Tender.Shared.Responses;
    using Tender.Shared.Settings;

    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        private readonly TenderConfigs _configs;

        public UserService(IUserRepository userRepository, IOptions<TenderConfigs> configs)
        {
            _userRepository = userRepository;
            _configs = configs?.Value;
        }

        public async Task<User> GetUserDetails(string userId)
        {
            return await _userRepository.GetUserById(userId).ConfigureAwait(false);
        }

        public async Task<AuthenticationResponse> AuthenticateUser(AuthenticationRequest request)
        {
            var user = await _userRepository.GetUserById(request.UserId).ConfigureAwait(false);

            if (user != null && user.Password == request.Password)
            {
                var token = GenerateToken(user);

                // set local expiration duration to 24 hours
                return new AuthenticationResponse(user, token, (3600 * 24));
            }

            return null;
        }

        /// <summary>
        /// Generates the token valid for 24 hours.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        private string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configs.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.UserId) }),
                Expires = DateTime.UtcNow.AddHours(24),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
