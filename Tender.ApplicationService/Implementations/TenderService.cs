namespace Tender.ApplicationService.Implementations
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Tender.ApplicationService.Interfaces;
    using Tender.Infrastructure.Repositories.Interfaces;
    using Tender.Shared.Responses;

    public class TenderService : ITenderService
    {
        private readonly ITenderQueryRepository tenderQueryRepository;

        public TenderService(ITenderQueryRepository tenderQueryRepository)
        {
            this.tenderQueryRepository = tenderQueryRepository;
        }

        public async Task<TenderDetailResponse> GetTenderDetail(long tenderId)
        {
            return await this.tenderQueryRepository.GetTender(tenderId).ConfigureAwait(false);
        }

        public async Task<List<TenderListResponse>> GetTenderList()
        {
            return await this.tenderQueryRepository.GetTenderList().ConfigureAwait(false);
        }
    }
}
