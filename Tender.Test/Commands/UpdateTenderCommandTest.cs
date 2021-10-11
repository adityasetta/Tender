namespace Tender.Test.Commands
{
    using System;
    using System.Threading;

    using CSharpFunctionalExtensions;

    using Moq;

    using Tender.ApplicationService.Command.UpdateTender;
    using Tender.Infrastructure.Repositories.Interfaces;
    using Tender.Shared.Requests;

    using Xunit;

    public class UpdateTenderCommandTest
    {
        [Fact]
        public async void Update_Tender_Edit_Successfully()
        {
            Mock<ITenderQueryRepository> mockRepository = new();

            UpdateTenderRequest requestDummy = new UpdateTenderRequest()
            {
                TenderId = 99,
                TenderName = "testName",
                ClosingDate = DateTime.Now,
                ReleaseDate = DateTime.Now,
                ContractNo = "testNo-1",
                Description = "test Description"
            };

            UpdateTenderCommand tenderCommand = new UpdateTenderCommand()
            {
                UserId = "testId",
                Tender = requestDummy
            };

            mockRepository.Setup(t => t.EditTender("testId", requestDummy)).ReturnsAsync(Result.Success());

            UpdateTenderCommandHandler handler = new(mockRepository.Object);

            var result = await handler.Handle(tenderCommand, new CancellationToken());

            Assert.Equal(Result.Success(), result);
        }
    }
}
