namespace Tender.ApplicationService.Interfaces
{
    using System.Threading.Tasks;

    using Tender.Domain.Entities;
    using Tender.Shared.Requests;
    using Tender.Shared.Responses;

    public interface IUserService
    {
        Task<User> GetUserDetails(string userId);
        Task<AuthenticationResponse> AuthenticateUser(AuthenticationRequest request);
    }
}
