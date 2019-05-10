using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MMWebAPI.Infrastructure.IoC.Manager;
using MMWebAPI.WebAPI.Middelware;
namespace MMWebAPI.WebAPI.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddNativeIoC(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            RegisterAllServices(serviceCollection, configuration);
            RegisterMiddlewares(serviceCollection);
        }

        private static void RegisterAllServices(IServiceCollection serviceCollection, IConfiguration configuration)
        {
            Bootstrapper.RegisterServices(serviceCollection, configuration);
        }
        private static void RegisterMiddlewares(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<ErrorHandlerMiddleware>();
        }
    }
}
