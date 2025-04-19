using bcommerce_server.Application.Abstractions;
using bcommerce_server.Domain.Validations.Handlers;
using bcommerce_server.Domain.Categories;
using OneOf;


namespace bcommerce_server.Application.Categories.Create;

public class CreateCategoryUseCase : IUseCase<CreateCategoryInput, CreateCategoryOutput, Notification>
{
    // private readonly string _categoryRepository;
    //
    // public Result<CreateCategoryOutput, Notification> Execute(CreateCategoryInput input)
    // {
    //     var notification = Notification.Create();
    //
    //     var category = Category.NewCategory(
    //         input.Name,
    //         input.Description,
    //         input.IsActive
    //     );
    //
    //     category.Validate(notification);
    //
    //     if (notification.HasError())
    //         return Result<CreateCategoryOutput, Notification>.Fail(notification);
    //
    //     try
    //     {
    //         var created = _categoryRepository.Create(category);
    //         return Result<CreateCategoryOutput, Notification>.Ok(CreateCategoryOutput.From(created));
    //     }
    //     catch (Exception ex)
    //     {
    //         return Result<CreateCategoryOutput, Notification>.Fail(Notification.Create(ex));
    //     }
    // }
    // public Result<CreateCategoryOutput, Notification> Execute(CreateCategoryInput input)
    // {
    //     return _useCaseImplementation.Execute(input);
    // }
    public Task<Result<CreateCategoryOutput, Notification>> Execute(CreateCategoryInput input)
    {
        throw new NotImplementedException();
    }
}