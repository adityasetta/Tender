namespace Tender.Api.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using System;
    using System.Threading.Tasks;

    using Tender.ApplicationService.Interfaces;
    using Tender.Domain.Entities;
    using Tender.Libraries.UserAuthorization.Filters;
    using Tender.Shared.Entities;
    using Tender.Shared.Requests;
    using Tender.Shared.Responses;

    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("api/user/{userId}")]
        [AuthorizeRoles(Role.Guest, Role.Admin)]
        public async Task<ActionResult<User>> GetUserDetailAsync(string userId)
        {
            return await _userService.GetUserDetails(userId).ConfigureAwait(false);
        }

        [HttpPost]
        [Route("api/user/authenticate")]
        public async Task<ActionResult<AuthenticationResponse>> AuthenticateUser([FromBody] AuthenticationRequest request)
        {
            try
            {
                var result = await _userService.AuthenticateUser(request).ConfigureAwait(false);

                if (result == null)
                {
                    return this.Unauthorized("Authentication Failed");
                }

                return result;
            }
            catch (Exception ex)
            {
                return this.Ok(ex.Message);
            }
        }
    }
}
