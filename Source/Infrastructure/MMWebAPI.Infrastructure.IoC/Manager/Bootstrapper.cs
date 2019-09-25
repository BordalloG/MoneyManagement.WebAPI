
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MMWebAPI.Application.Implementations;
using MMWebAPI.Application.Interfaces;
using MMWebAPI.Application.TokenConfiguration;
using MMWebAPI.Domain.Implementations.Domain;
using MMWebAPI.Domain.Interfaces.Domain;
using MMWebAPI.Domain.Interfaces.Repository;
using MMWebAPI.Infrastructure.Repository.Context;
using MMWebAPI.Infrastructure.Repository.Implementation;
using System.Text;

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

        public static void RegisterIdentityConfigurationService(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));


            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
            //JWT

            var appSettingsSection = configuration.GetSection("AppSettings");

            services.Configure<AppSettings>(appSettingsSection);

            var appSettings = appSettingsSection.Get<AppSettings>();

            var key = Encoding.ASCII.GetBytes(appSettings.Secret);

            services.AddAuthentication(x =>
           {
               x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
               x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
           }).AddJwtBearer(x =>
           {
               x.RequireHttpsMetadata = false; //aceita apenas https
               x.SaveToken = true;
               x.TokenValidationParameters = new TokenValidationParameters()
               {
                   ValidateIssuerSigningKey = true, //valida se issuer( emissor ) tem que ser o mesmo que envia o token
                   IssuerSigningKey = new SymmetricSecurityKey(key),
                   ValidateIssuer = true,
                   ValidAudience = appSettings.ValidoEm,
                   ValidIssuer = appSettings.Emissor
               };
           });

        }
    }
}
