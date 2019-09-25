using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MMWebAPI.Infrastructure.IoC.Manager;
using MMWebAPI.WebAPI.Middelware;
namespace MMWebAPI.WebAPI.Extensions
{
    /// <summary>
    /// Class that Extends ServiceCollection to register services and middlewares
    /// </summary>
    public static class ServiceCollectionExtension
    {
        /// <summary>
        /// Register the services of the API
        /// </summary>
        /// <param name="serviceCollection"></param>
        /// <param name="configuration"></param>
        public static void AddNativeIoC(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            RegisterAllServices(serviceCollection, configuration);
            RegisterMiddlewares(serviceCollection);
        }

        private static void RegisterAllServices(IServiceCollection serviceCollection, IConfiguration configuration)
        {
            Bootstrapper.RegisterServices(serviceCollection, configuration);
            Bootstrapper.RegisterIdentityConfigurationService(serviceCollection, configuration);
        }
        private static void RegisterMiddlewares(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<ErrorHandlerMiddleware>();
        }
    }
}
