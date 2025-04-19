using bcommerce_server.Application.Abstractions;
using bcommerce_server.Domain.Validations.Handlers;
using OneOf;

namespace bcommerce_server.Application.Categories.Create;

public interface ICreateCategoryUseCase : IUseCase<CreateCategoryInput, CreateCategoryOutput, Notification>
{
    Result<Notification, CreateCategoryOutput> Execute(CreateCategoryInput input);
}