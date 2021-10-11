namespace Tender.Api.Extensions
{
    using FluentValidation;

    using Microsoft.Extensions.DependencyInjection;

    using System;
    using System.Linq;
    using System.Reflection;

    public static class ServiceCollectionDependenciesExtensions
    {
        public static IServiceCollection ConfigureDependencies(this IServiceCollection serviceCollection)
        {
            serviceCollection?.RegisterAllTypes<IValidator>(new[] { typeof(Startup).Assembly });
            return serviceCollection;
        }

        /// <summary>
        /// Registers all types, primarily are command validations.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="services">The services.</param>
        /// <param name="assemblies">The assemblies.</param>
        /// <param name="lifetime">The lifetime.</param>
        private static void RegisterAllTypes<T>(this IServiceCollection services, Assembly[] assemblies, ServiceLifetime lifetime = ServiceLifetime.Transient)
        {
            var typesFromAssemblies = assemblies.SelectMany(a => a.DefinedTypes.Where(x => x.GetInterfaces().Contains(typeof(T)) && !x.IsAbstract));
            foreach (var type in typesFromAssemblies)
            {
                services.Add(new ServiceDescriptor(typeof(T), type, lifetime));
            }
        }
    }
}
