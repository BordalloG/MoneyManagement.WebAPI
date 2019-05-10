using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MMWebAPI.Application.Implementations;
using MMWebAPI.Application.Interfaces;
using MMWebAPI.Domain.Implementations.Domain;
using MMWebAPI.Domain.Interfaces.Domain;
using MMWebAPI.Domain.Interfaces.Repository;
using MMWebAPI.Domain.Validator;
using MMWebAPI.Infrastructure.Repository.Context;
using MMWebAPI.Infrastructure.Repository.Implementation;

namespace MMWebAPI.Infrastructure.IoC.Manager
{
    public class Bootstrapper
    {
        public static void RegisterServices(IServiceCollection serviceCollection, IConfiguration configuration)
        {
            //Application
            serviceCollection.AddScoped<IFinancialTransactionApplicationService, FinancialTransactionApplicationService>();
            serviceCollection.AddScoped<IFinancialTransactionGroupApplicationService, FinancialTransactionGroupApplicationService>();
            
            //Domain
            serviceCollection.AddScoped<IFinancialTransactionDomainService, FinancialTransactionDomainService>();
            serviceCollection.AddScoped<IFinancialTransactionGroupDomainService, FinancialTransactionGroupDomainService>();

            //Repository
            serviceCollection.AddScoped<IFinancialTransactionRepository, FinancialTransactionRepository>();
            serviceCollection.AddScoped<IFinancialTransactionGroupRepository, FinancialTransactionGroupRepository>();

            serviceCollection.AddSingleton(sp => new MMContext(configuration.GetConnectionString("DefaultConnection")));
        }
    }
}
