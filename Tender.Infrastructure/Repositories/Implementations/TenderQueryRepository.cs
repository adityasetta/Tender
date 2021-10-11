namespace Tender.Infrastructure.Repositories.Implementations
{
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Threading.Tasks;

    using CSharpFunctionalExtensions;

    using Dapper;

    using Microsoft.Extensions.Options;

    using Tender.Infrastructure.Repositories.Interfaces;
    using Tender.Shared.Requests;
    using Tender.Shared.Responses;
    using Tender.Shared.Settings;

    /// <summary>
    /// The user details query repository.
    /// </summary>
    public class TenderQueryRepository : QueryRepository, ITenderQueryRepository
    {
        #region sp names

        /// <summary>
        /// The stored procedure name to delete tender.
        /// </summary>
        private const string SpDeleteTender = "usp_DeleteTender";

        // <summary>
        /// The stored procedure name to get tender detail.
        /// </summary>
        private const string SpGetTenderDetail = "usp_GetTenderDetail";

        // <summary>
        /// The stored procedure name to get tender list.
        /// </summary>
        private const string SpGetTenderList = "usp_GetTenderList";

        // <summary>
        /// The stored procedure name to post new tender.
        /// </summary>
        private const string SpPostTender = "usp_PostTender";

        /// <summary>
        /// The stored procedure name update tender.
        /// </summary>
        private const string SpUpdateTender = "usp_UpdateTender";

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="TenderQueryRepository"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public TenderQueryRepository(IOptions<TenderConfigs> configuration) : base(configuration)
        {
        }

        public async Task<Result> CreateTender(string userId, PostTenderRequest request)
        {
            string xml = QueryHelper.XmlSerializer(request);

            DynamicParameters dp = new();

            dp.Add("@userId", userId, dbType: DbType.String);
            dp.Add("@body", xml, DbType.Xml);
            dp.Add("@errorCode", dbType: DbType.Int32, direction: ParameterDirection.Output);
            dp.Add("@errorMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 512);

            using (var connection = DbConnection)
            {
                connection.Open();

                await connection.ExecuteAsync(SpPostTender, dp, commandType: CommandType.StoredProcedure).ConfigureAwait(false);

                var messageCode = dp.Get<int>("@errorCode");
                var messageName = dp.Get<string>("@errorMessage");

                if (messageCode != 1)
                {
                    return Result.Failure(messageName);
                }

                return Result.Success();
            }
        }

        public async Task<Result> EditTender(string userId, UpdateTenderRequest request)
        {
            string xml = QueryHelper.XmlSerializer(request);

            DynamicParameters dp = new();

            dp.Add("@userId", userId, dbType: DbType.String);
            dp.Add("@body", xml, DbType.Xml);
            dp.Add("@errorCode", dbType: DbType.Int32, direction: ParameterDirection.Output);
            dp.Add("@errorMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 512);

            using var connection = DbConnection;
            connection.Open();

            await connection.ExecuteAsync(SpUpdateTender, dp, commandType: CommandType.StoredProcedure).ConfigureAwait(false);

            var messageCode = dp.Get<int>("@errorCode");
            var messageName = dp.Get<string>("@errorMessage");

            if (messageCode != 1)
            {
                return Result.Failure(messageName);
            }

            return Result.Success();
        }

        public async Task<Result> DeleteTender(string userId, long tenderId)
        {
            DynamicParameters dp = new();

            dp.Add("@userId", userId, dbType: DbType.String);
            dp.Add("@tenderId", tenderId, DbType.Int64);
            dp.Add("@errorCode", dbType: DbType.Int32, direction: ParameterDirection.Output);
            dp.Add("@errorMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 512);

            using var connection = DbConnection;
            connection.Open();

            await connection.ExecuteAsync(SpDeleteTender, dp, commandType: CommandType.StoredProcedure).ConfigureAwait(false);

            var messageCode = dp.Get<int>("@errorCode");
            var messageName = dp.Get<string>("@errorMessage");

            if (messageCode != 1)
            {
                return Result.Failure(messageName);
            }

            return Result.Success();
        }

        public async Task<TenderDetailResponse> GetTender(long tenderId)
        {
            using var connection = DbConnection;
            using var result = await connection.QueryMultipleAsync(SpGetTenderDetail,
                new
                {
                    tenderId
                },
                commandType: CommandType.StoredProcedure).ConfigureAwait(false);

            var response = (await result.ReadAsync<TenderDetailResponse>().ConfigureAwait(false)).FirstOrDefault();
            return response;
        }

        public async Task<List<TenderListResponse>> GetTenderList()
        {
            using var connection = DbConnection;
            using var result = await connection.QueryMultipleAsync(SpGetTenderList,
                commandType: CommandType.StoredProcedure).ConfigureAwait(false);

            var response = (await result.ReadAsync<TenderListResponse>().ConfigureAwait(false))?.ToList();
            return response;
        }
    }
}
