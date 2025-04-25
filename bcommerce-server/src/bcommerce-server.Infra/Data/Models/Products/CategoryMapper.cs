using bcommerce_server.Domain.Products;
using bcommerce_server.Domain.Products.Identifiers;

namespace bcommerce_server.Infra.Data.Models.Products;

public static class CategoryMapper
{
    public static Category ToDomain(CategoryDataModel model)
    {
        return Category.With(
            CategoryID.From(model.Id),
            model.Name,
            model.CreatedAt,
            model.UpdatedAt
        );
    }

    public static CategoryDataModel ToDataModel(Category category)
    {
        return new CategoryDataModel(
            category.Id.Value,
            category.Name,
            category.CreatedAt,
            category.UpdatedAt
        );
    }
}