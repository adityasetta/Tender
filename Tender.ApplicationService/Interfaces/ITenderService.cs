namespace Tender.ApplicationService.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Tender.Shared.Responses;

    public interface ITenderService
    {
        Task<TenderDetailResponse> GetTenderDetail(long tenderId);
        Task<List<TenderListResponse>> GetTenderList();
    }
}
