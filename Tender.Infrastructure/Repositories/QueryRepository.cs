namespace Tender.Infrastructure.Repositories
{
    using Microsoft.Extensions.Options;

    using System.Data;
    using System.Data.SqlClient;

    using Tender.Shared.Settings;

    /// <summary>
    /// Base implementation for all query repositories
    /// </summary>
    public abstract class QueryRepository
    {
        /// <summary>
        /// The configuration
        /// </summary>
        private readonly string _connectionString;

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryRepository"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public QueryRepository(IOptions<TenderConfigs> configuration)
        {
            _connectionString = configuration?.Value?.ConnectionStrings?.SqlConnectionString;
        }

        /// <summary>
        /// Gets the database connection.
        /// </summary>
        /// <value>
        /// The database connection.
        /// </value>
        protected IDbConnection DbConnection => new SqlConnection(_connectionString);
    }
}
