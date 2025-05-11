using bcommerce_server.Application.Carts.Add;
using bcommerce_server.Application.Customers.Create;
using bcommerce_server.Application.Customers.Login;
using bcommerce_server.Application.Products.GetAll;
using bcommerce_server.Application.Products.GetById;

namespace bcommercer_server.Api.Configurations;

public static class ApplicationDependencyInjection
{
    public static void AddApplication(this IServiceCollection services, IConfigurationManager configuration)
    {
        AddUseCases(services);
    }
    
    private static void AddUseCases(IServiceCollection services)
    {
        // Auth
        services.AddScoped<ICreateCustomerUseCase, CreateCustomerUseCase>();
        services.AddScoped<ILoginCustomerUseCase, LoginCustomerUseCase>();
        
        // Products
        services.AddScoped<IGetAllProuctsUseCase, GetAllProductsUseCase>();
        services.AddScoped<IGetProductByIdUseCase, GetProductByIdUseCase>();

        // Cart
        services.AddScoped<IAddItemToCartUseCase, AddItemToCartUseCase>();
    }
}