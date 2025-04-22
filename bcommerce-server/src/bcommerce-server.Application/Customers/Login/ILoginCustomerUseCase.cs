using System.Threading.Tasks;
using bcommerce_server.Application.Abstractions;
using bcommerce_server.Domain.Validations.Handlers;

namespace bcommerce_server.Application.Customers.Login;

public interface ILoginCustomerUseCase : IUseCase<LoginCustomerInput, LoginCustomerOutput, Notification>
{

}