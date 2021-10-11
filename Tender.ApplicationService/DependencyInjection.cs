namespace Tender.ApplicationService
{

    using MediatR;

    using Microsoft.Extensions.DependencyInjection;

    using System.Reflection;

    using Tender.ApplicationService.Command.DeleteTender;
    using Tender.ApplicationService.Command.PostTender;
    using Tender.ApplicationService.Command.UpdateTender;
    using Tender.ApplicationService.Implementations;
    using Tender.ApplicationService.Interfaces;

    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services)
        {
            services.AddScoped<ITenderService, TenderService>();
            services.AddScoped<IUserService, UserService>();
            services.AddMediatR(typeof(PostTenderCommandHandler).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(DeleteTenderCommandHandler).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(UpdateTenderCommandHandler).GetTypeInfo().Assembly);

            return services;
        }
    }
}
