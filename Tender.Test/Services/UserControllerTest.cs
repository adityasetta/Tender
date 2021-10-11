namespace Tender.Test.Services
{
    using System;

    using Moq;

    using Tender.Api.Controllers;
    using Tender.ApplicationService.Interfaces;
    using Tender.Domain.Entities;
    using Tender.Shared.Requests;
    using Tender.Shared.Responses;

    using Xunit;

    public class UserControllerTest
    {
        [Fact]
        public async void Get_User_Details_By_Id()
        {
            Mock<IUserService> mockService = new();

            User resultDummy = new()
            {
                UserId = "guest1",
                Name = "guest number 1",
                Password = "guest1",
                Role = "GUEST",
                CreatedDate = DateTime.Now
            };

            mockService.Setup(t => t.GetUserDetails("guest1")).ReturnsAsync(resultDummy);

            UserController controller = new(mockService.Object);

            var result = await controller.GetUserDetailAsync("guest1");

            Assert.True(resultDummy.Equals(result.Value));
        }

        [Fact]
        public async void Authenticate_User_Successfully()
        {
            Mock<IUserService> mockService = new();

            User userDummy = new()
            {
                UserId = "guest1",
                Name = "guest number 1",
                Password = "guest1",
                Role = "GUEST",
                CreatedDate = DateTime.Now
            };

            string tokenDummy = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6Imd1ZXN0MSIsIm5iZiI6MTYzMzc1NTEyOCwiZXhwIjoxNjMzODQxNTI4LCJpYXQiOjE2MzM3NTUxMjh9.Jh_dQ5qu8TOcwZVNUmEI0NEdAxyjeRyi5NZsqAHG_3I";
            AuthenticationResponse resultDummy = new(userDummy, tokenDummy);

            AuthenticationRequest requestDummy = new()
            {
                UserId = "guest1",
                Password = "guest1"
            };

            mockService.Setup(t => t.AuthenticateUser(requestDummy)).ReturnsAsync(resultDummy);

            UserController controller = new(mockService.Object);

            var result = await controller.AuthenticateUser(requestDummy);

            Assert.True(resultDummy.Equals(result.Value));
        }
    }
}
