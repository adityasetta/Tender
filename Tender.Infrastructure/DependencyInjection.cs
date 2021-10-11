namespace Tender.Infrastructure
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.EntityFrameworkCore;

    using Tender.Domain.Repositories;
    using Tender.Infrastructure.Repositories.DataAccess;
    using Tender.Infrastructure.Repositories.Implementations;
    using Tender.Infrastructure.Repositories.Interfaces;

    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ITenderQueryRepository, TenderQueryRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            var connectionString = configuration.GetValue<string>("TenderConfigs:ConnectionStrings:SqlConnectionString");
            services.AddDbContext<UserContext>(option => option.UseSqlServer(connectionString));

            return services;
        }
    }
}
