using bcommerce_server.Domain.Categories;

namespace bcommerce_server.Application.Categories.Create;

public record CreateCategoryOutput(string Id, string Name, string Description, bool IsActive)
{
    public static CreateCategoryOutput From(Category category)
        => new(category.Id.Value, category.Name, category.Description, category.IsActive);
}