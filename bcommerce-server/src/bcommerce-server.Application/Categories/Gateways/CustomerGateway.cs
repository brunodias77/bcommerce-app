using bcommerce_server.Domain.Customers;
using bcommerce_server.Domain.Customers.Gateways;
using bcommerce_server.Domain.Customers.Identifiers;

namespace bcommerce_server.Application.Categories.Gateways;

public class CustomerGateway : ICustomerGateway
{
    public Task<Customer> GetById(CustomerID id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task Add(Customer customer, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task Update(Customer customer, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task Delete(Customer customer, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
    
    
    
    
    
    // private readonly IGenericRepository<Customer> _repository;
    //
    // public CustomerGateway(IGenericRepository<Customer> repository)
    // {
    //     _repository = repository;
    // }
    //
    // public Task<Customer> GetById(CustomerID id, CancellationToken cancellationToken)
    //     => _repository.Get(id, cancellationToken);
    //
    // public Task Add(Customer customer, CancellationToken cancellationToken)
    //     => _repository.Insert(customer, cancellationToken);
    //
    // public Task Update(Customer customer, CancellationToken cancellationToken)
    //     => _repository.Update(customer, cancellationToken);
    //
    // public Task Delete(Customer customer, CancellationToken cancellationToken)
    //     => _repository.Delete(customer, cancellationToken);
}