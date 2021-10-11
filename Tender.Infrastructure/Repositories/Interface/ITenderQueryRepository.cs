namespace Tender.Infrastructure.Repositories.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CSharpFunctionalExtensions;

    using Tender.Shared.Requests;
    using Tender.Shared.Responses;

    /// <summary>
    /// The user details query repository.
    /// </summary>
    public interface ITenderQueryRepository
    {
        Task<Result> CreateTender(string userId, PostTenderRequest request);
        Task<Result> EditTender(string userId, UpdateTenderRequest request);
        Task<Result> DeleteTender(string userId, long tenderId);
        Task<TenderDetailResponse> GetTender(long tenderId);
        Task<List<TenderListResponse>> GetTenderList();
    }
}
