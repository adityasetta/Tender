namespace Tender.Test.Commands
{
    using System.Threading;

    using CSharpFunctionalExtensions;

    using Moq;

    using Tender.ApplicationService.Command.DeleteTender;
    using Tender.Infrastructure.Repositories.Interfaces;

    using Xunit;

    public class DeleteTenderCommandTest
    {
        [Fact]
        public async void Delete_Tender_Successfully()
        {
            Mock<ITenderQueryRepository> mockRepository = new();

            DeleteTenderCommand tenderCommand = new DeleteTenderCommand()
            {
                UserId = "testId",
                TenderId = 99
            };

            mockRepository.Setup(t => t.DeleteTender("testId", 99)).ReturnsAsync(Result.Success());

            DeleteTenderCommandHandler handler = new(mockRepository.Object);

            var result = await handler.Handle(tenderCommand, new CancellationToken());

            Assert.Equal(Result.Success(), result);
        }
    }
}
