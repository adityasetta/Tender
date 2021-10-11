namespace Tender.Test.Services
{
    using System;
    using System.Collections.Generic;

    using Moq;

    using Tender.ApplicationService.Implementations;
    using Tender.Infrastructure.Repositories.Interfaces;
    using Tender.Shared.Responses;

    using Xunit;

    public class TenderServiceTest
    {
        [Fact]
        public async void Get_Tender_Details()
        {
            Mock<ITenderQueryRepository> mockRepository = new();

            TenderDetailResponse resultDummy = new TenderDetailResponse()
            {
                TenderId = 99,
                TenderName = "testName",
                ClosingDate = DateTime.Now,
                ReleaseDate = DateTime.Now,
                ContractNo = "testNo-1",
                Description = "test Description",
                CreatorId = "tester"
            };

            mockRepository.Setup(t => t.GetTender(99)).ReturnsAsync(resultDummy);

            TenderService service = new(mockRepository.Object);

            var result = await service.GetTenderDetail(99);

            Assert.True(resultDummy.Equals(result));
        }

        [Fact]
        public async void Get_Tender_List()
        {
            Mock<ITenderQueryRepository> mockRepository = new();

            var resultDummyList = new List<TenderListResponse>() {
                new TenderListResponse()
                {
                    TenderId = 1,
                    TenderName = "testName",
                    ClosingDate = DateTime.Now,
                    ReleaseDate = DateTime.Now,
                    ContractNo = "testNo-1"
                },
                new TenderListResponse()
                {
                    TenderId = 2,
                    TenderName = "testName",
                    ClosingDate = DateTime.Now,
                    ReleaseDate = DateTime.Now,
                    ContractNo = "testNo-1"
                },
                new TenderListResponse()
                {
                    TenderId = 3,
                    TenderName = "testName",
                    ClosingDate = DateTime.Now,
                    ReleaseDate = DateTime.Now,
                    ContractNo = "testNo-1"
                }
            };

            mockRepository.Setup(t => t.GetTenderList()).ReturnsAsync(resultDummyList);

            TenderService service = new(mockRepository.Object);

            var result = await service.GetTenderList();

            Assert.Equal(resultDummyList, result);
        }
    }
}
