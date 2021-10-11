namespace Tender.Test.Commands
{
    using System;
    using System.Threading;

    using CSharpFunctionalExtensions;

    using Moq;

    using Tender.ApplicationService.Command.PostTender;
    using Tender.Infrastructure.Repositories.Interfaces;
    using Tender.Shared.Requests;

    using Xunit;

    public class PostTenderCommandTest
    {
        [Fact]
        public async void Post_Tender_Save_Successfully()
        {
            Mock<ITenderQueryRepository> mockRepository = new();

            PostTenderRequest requestDummy = new PostTenderRequest()
            {
                TenderName = "testName",
                ClosingDate = DateTime.Now,
                ReleaseDate = DateTime.Now,
                ContractNo = "testNo-1",
                Description = "test Description"
            };

            PostTenderCommand tenderCommand = new PostTenderCommand()
            {
                UserId = "testId",
                Tender = requestDummy
            };

            mockRepository.Setup(t => t.CreateTender("testId", requestDummy)).ReturnsAsync(Result.Success());

            PostTenderCommandHandler handler = new(mockRepository.Object);

            var result = await handler.Handle(tenderCommand, new CancellationToken());

            Assert.Equal(Result.Success(), result);
        }
    }
}
