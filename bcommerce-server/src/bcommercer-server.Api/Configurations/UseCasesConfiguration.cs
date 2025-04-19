using bcommerce_server.Application.Customers.Create;
using bcommerce_server.Domain.Customers.Repositories;
using bcommerce_server.Infra.Repositories;

namespace bcommercer_server.Api.Configurations;

public static class UseCasesConfiguration
{
    public static IServiceCollection AddUseCases(this IServiceCollection services)
    {
        services.AddRepositories()
            .AddApplicationUseCases();
        return services;
    }
    
    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddTransient<ICustomerRepository, CustomerRepository>();
        return services;
    }
    
    private static IServiceCollection AddApplicationUseCases(this IServiceCollection services)
    {
        services.AddScoped<ICreateCustomerUseCase, CreateCustomerUseCase>();
        return services;
    }
}