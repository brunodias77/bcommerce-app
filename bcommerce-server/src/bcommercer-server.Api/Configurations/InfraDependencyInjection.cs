using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bcommerce_server.Domain.Security;

namespace bcommercer_server.Api.Configurations
{
    public static class InfraDependencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfigurationManager configuration)
        {
            AddToken(services, configuration);
            AddRepositories(services);
        }

        private static void AddToken(IServiceCollection services, IConfigurationManager configuration)
        {
            services.Configure<JwtSettings>(configuration.GetSection("Settings:Jwt"));
            services.AddScoped<ITokenService, TokenService>();

        }

        private static void AddRepositories(IServiceCollection services)
        {
            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IUnitOfWork, DapperUnitOfWork>();
        }

    }
}