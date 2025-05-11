using System;
using System.Threading;
using System.Threading.Tasks;
using bcommerce_server.Application.Abstractions;
using bcommerce_server.Domain.Customers;
using bcommerce_server.Domain.Customers.Repositories;
using bcommerce_server.Domain.Customers.ValueObjects;
using bcommerce_server.Domain.Security;
using bcommerce_server.Domain.Validations.Handlers;
using bcommerce_server.Domain.Validations;
using bcommerce_server.Domain.Carts;
using bcommerce_server.Domain.Carts.Repositories;
using bcommerce_server.Infra.Repositories;

namespace bcommerce_server.Application.Customers.Create
{
    public class CreateCustomerUseCase : ICreateCustomerUseCase
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordEncripter _passwordEncripter;
        private readonly ICartRepository _cartRepository;

        public CreateCustomerUseCase(
            ICustomerRepository customerRepository,
            IUnitOfWork unitOfWork,
            IPasswordEncripter passwordEncripter,
            ICartRepository cartRepository)
        {
            _customerRepository = customerRepository;
            _unitOfWork = unitOfWork;
            _passwordEncripter = passwordEncripter;
            _cartRepository = cartRepository;
        }

        public async Task<Result<CreateCustomerOutput, Notification>> Execute(CreateCustomerInput input)
        {
            var notification = Notification.Create();
            await BeginTransaction();

            if (await EmailAlreadyExists(input.Email))
            {
                return await FailWith("Já existe um cliente com este e-mail.");
            }

            var customer = BuildAndValidateCustomer(input, notification);
            if (notification.HasError())
            {
                return await FailWith(notification);
            }

            var cart = Cart.NewCart(customer.Id.Value);

            try
            {
                await _customerRepository.Insert(customer, CancellationToken.None);
                await _cartRepository.Insert(cart, CancellationToken.None);
                await _unitOfWork.Commit();

                var output = CreateCustomerOutput.FromCustomer(customer);
                return Result<CreateCustomerOutput, Notification>.Ok(output);
            }
            catch (Exception ex)
            {
                await _unitOfWork.Rollback();
                return Result<CreateCustomerOutput, Notification>.Fail(Notification.Create(new Error(ex.Message)));
            }
            finally
            {
                await _unitOfWork.DisposeAsync();
            }
        }

        // 🔒 MÉTODOS PRIVADOS

        private async Task BeginTransaction()
        {
            await _unitOfWork.Begin();
        }

        private async Task<bool> EmailAlreadyExists(string email)
        {
            var existing = await _customerRepository.GetByEmail(email, CancellationToken.None);
            return existing is not null;
        }

        private Customer BuildAndValidateCustomer(CreateCustomerInput input, Notification notification)
        {
            var passwordEncrypted = _passwordEncripter.Encrypt(input.Password);

            var customer = Customer.NewCustomer(
                input.Name,
                Email.From(input.Email),
                passwordEncrypted
            );

            customer.Validate(notification);
            return customer;
        }

        private async Task<Result<CreateCustomerOutput, Notification>> FailWith(string message)
        {
            await _unitOfWork.Rollback();
            await _unitOfWork.DisposeAsync();
            return Result<CreateCustomerOutput, Notification>.Fail(Notification.Create(new Error(message)));
        }

        private async Task<Result<CreateCustomerOutput, Notification>> FailWith(Notification notification)
        {
            await _unitOfWork.Rollback();
            await _unitOfWork.DisposeAsync();
            return Result<CreateCustomerOutput, Notification>.Fail(notification);
        }
    }
}




// using System;
// using System.Threading;
// using System.Threading.Tasks;
// using bcommerce_server.Application.Abstractions;
// using bcommerce_server.Domain.Carts;
// using bcommerce_server.Domain.Carts.Repositories;
// using bcommerce_server.Domain.Customers;
// using bcommerce_server.Domain.Customers.Repositories;
// using bcommerce_server.Domain.Customers.ValueObjects;
// using bcommerce_server.Domain.Security;
// using bcommerce_server.Domain.Validations.Handlers;
// using bcommerce_server.Domain.Validations;
// using bcommerce_server.Infra.Repositories;
//
// namespace bcommerce_server.Application.Customers.Create;
//
//
//
//
// public class CreateCustomerUseCase : ICreateCustomerUseCase
// {
//     public CreateCustomerUseCase(ICustomerRepository customerRepository, IUnitOfWork unitOfWork, IPasswordEncripter passwordEncripter, ICartRepository cartRepository)
//     {
//         _customerRepository = customerRepository;
//         _unitOfWork = unitOfWork;
//         _passwordEncripter = passwordEncripter;
//         _cartRepository = cartRepository;
//     }
//
//
//     private readonly ICustomerRepository _customerRepository;
//     private readonly IUnitOfWork _unitOfWork;
//     private readonly IPasswordEncripter _passwordEncripter;
//     private readonly ICartRepository _cartRepository;   
//
//  
//     public async Task<Result<CreateCustomerOutput, Notification>> Execute(CreateCustomerInput input)
//     {
//         var notification = Notification.Create();
//             await BeginTransaction();
//         
//             if (await EmailAlreadyExists(input.Email))
//             {
//                 return await FailWith("Já existe um cliente com este e-mail.");
//             }
//         
//             var customer = BuildAndValidateCustomer(input, notification);
//             if (notification.HasError())
//             {
//                 return await FailWith(notification);
//             }
//         
//             // ✅ Criar um carrinho vazio associado ao novo cliente
//             var cart = Cart.NewCart(customer.Id);
//         
//             try
//             {
//                 await _customerRepository.Insert(customer, CancellationToken.None);
//                 await _cartRepository.Insert(cart, CancellationToken.None);
//                 await _unitOfWork.Commit();
//         
//                 var output = CreateCustomerOutput.FromCustomer(customer);
//                 return Result<CreateCustomerOutput, Notification>.Ok(output);
//             }
//             catch (Exception ex)
//             {
//                 await _unitOfWork.Rollback();
//                 return Result<CreateCustomerOutput, Notification>.Fail(Notification.Create(new Error(ex.Message)));
//             }
//             finally
//             {
//                 await _unitOfWork.DisposeAsync();
//             }
//         // var notification = Notification.Create();
//         //
//         // await BeginTransaction();
//         //
//         // if (await EmailAlreadyExists(input.Email))
//         // {
//         //     return await FailWith("Já existe um cliente com este e-mail.");
//         // }
//         //
//         // var customer = BuildAndValidateCustomer(input, notification);
//         // if (notification.HasError())
//         // {
//         //     return await FailWith(notification);
//         // }
//         //
//         // // TODO: criar um cart
//         //
//         // return await SaveCustomer(customer);
//     }
//
//     // 🔒 MÉTODOS PRIVADOS
//
//     private async Task BeginTransaction()
//     {
//         await _unitOfWork.Begin();
//     }
//
//     private async Task<bool> EmailAlreadyExists(string email)
//     {
//         var existing = await _customerRepository.GetByEmail(email, CancellationToken.None);
//         return existing is not null;
//     }
//
//     private Customer BuildAndValidateCustomer(CreateCustomerInput input, Notification notification)
//     {
//         var passwordEncripted = _passwordEncripter.Encrypt(input.Password);
//         
//         var customer = Customer.NewCustomer(
//             input.Name,
//             Email.From(input.Email),
//             passwordEncripted
//         );
//
//         customer.Validate(notification);
//         return customer;
//     }
//
//     private async Task<Result<CreateCustomerOutput, Notification>> SaveCustomer(Customer customer)
//     {
//         try
//         {
//             await _customerRepository.Insert(customer, CancellationToken.None);
//             await _unitOfWork.Commit();
//
//             var output = CreateCustomerOutput.FromCustomer(customer);
//             return Result<CreateCustomerOutput, Notification>.Ok(output);
//         }
//         catch (Exception ex)
//         {
//             await _unitOfWork.Rollback();
//             return Result<CreateCustomerOutput, Notification>.Fail(Notification.Create(new Error(ex.Message)));
//         }
//         finally
//         {
//             await _unitOfWork.DisposeAsync();
//         }
//     }
//
//     private async Task<Result<CreateCustomerOutput, Notification>> FailWith(string message)
//     {
//         await _unitOfWork.Rollback();
//         await _unitOfWork.DisposeAsync();
//         return Result<CreateCustomerOutput, Notification>.Fail(Notification.Create(new Error("Já existe um cliente com este e-mail.")));
//     }
//
//     private async Task<Result<CreateCustomerOutput, Notification>> FailWith(Notification notification)
//     {
//         await _unitOfWork.Rollback();
//         await _unitOfWork.DisposeAsync();
//         return Result<CreateCustomerOutput, Notification>.Fail(notification);
//     }
// }
//
//
