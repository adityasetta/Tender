namespace Tender.Shared.Responses
{

    using Tender.Domain.Entities;

    public class AuthenticationResponse
    {
        public AuthenticationResponse(User user, string token, int expiresIn)
        {
            UserId = user.UserId;
            Name = user.Name;
            Role = user.Role;
            Token = token;
            ExpiresIn = expiresIn;
        }

        public string UserId { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
        public int ExpiresIn { get; set; }
    }
}
