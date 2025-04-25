using bcommerce_server.Domain.Customers.Repositories;
using bcommerce_server.Domain.Products.Repostories;
using bcommerce_server.Domain.Security;
using bcommerce_server.Infra.Repositories;
using bcommerce_server.Infra.Security;

namespace bcommercer_server.Api.Configurations
{
    public static class InfraDependencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfigurationManager configuration)
        {
            AddToken(services, configuration);
            AddRepositories(services);
            AddPasswordEncrypter(services, configuration); // ✅ ADICIONE ESTA LINHA
        }

        private static void AddToken(IServiceCollection services, IConfigurationManager configuration)
        {
            services.Configure<JwtSettings>(configuration.GetSection("Settings:JwtSettings"));
            services.AddScoped<ITokenService, TokenService>();
        }

        private static void AddRepositories(IServiceCollection services)
        {
            services.AddTransient<ICustomerRepository, DapperCustomerRepository>();
            services.AddTransient<IProductRepository, DapperProductRepository>();
            services.AddScoped<IUnitOfWork, DapperUnitOfWork>();
        }
        
        private static void AddPasswordEncrypter(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IPasswordEncripter, PasswordEncripter>();
        }



    }
}