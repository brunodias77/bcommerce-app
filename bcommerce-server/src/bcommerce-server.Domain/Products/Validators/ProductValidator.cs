using System;
using System.Linq;
using System.Text.RegularExpressions;
using bcommerce_server.Domain.Products.Entities;
using bcommerce_server.Domain.Validations;

namespace bcommerce_server.Domain.Products.Validators;
public sealed class ProductValidator : Validator
{
    private const int NAME_MIN = 3;
    private const int NAME_MAX = 150;
    private const int DESC_MIN = 10;
    private const int DESC_MAX = 1000;
    private const int URL_MAX = 2048;

    private readonly Product _product;

    public ProductValidator(Product product, IValidationHandler handler)
        : base(handler)
    {
        _product = product ?? throw new ArgumentNullException(nameof(product));
    }

    public override void Validate()
    {
        ValidateName();
        ValidateDescription();
        ValidatePrice();
        ValidateOldPrice();
        ValidateStockAndSold();
        ValidateCategory();
        ValidateImages();
        ValidateColors();
        ValidateReviews();
        ValidateTimestamps();
        ValidatePromotionFlags();
        ValidateSoftDelete();
    }

    private void ValidateName()
    {
        if (string.IsNullOrWhiteSpace(_product.Name))
        {
            AddError("'name' não pode estar em branco.");
            return;
        }

        var length = _product.Name.Trim().Length;
        if (length < NAME_MIN || length > NAME_MAX)
        {
            AddError($"'name' deve ter entre {NAME_MIN} e {NAME_MAX} caracteres.");
        }
    }

    private void ValidateDescription()
    {
        if (string.IsNullOrWhiteSpace(_product.Description))
        {
            AddError("'description' não pode estar em branco.");
            return;
        }

        var length = _product.Description.Trim().Length;
        if (length < DESC_MIN || length > DESC_MAX)
        {
            AddError($"'description' deve ter entre {DESC_MIN} e {DESC_MAX} caracteres.");
        }
    }

    private void ValidatePrice()
    {
        if (_product.Price <= 0)
        {
            AddError("'price' deve ser maior que zero.");
        }
    }

    private void ValidateOldPrice()
    {
        if (_product.OldPrice.HasValue && _product.OldPrice < 0)
        {
            AddError("'oldPrice' não pode ser negativo.");
        }
    }

    private void ValidateStockAndSold()
    {
        if (_product.StockQuantity < 0)
            AddError("'stockQuantity' não pode ser negativo.");

        if (_product.Sold < 0)
            AddError("'sold' não pode ser negativo.");
    }

    private void ValidateCategory()
    {
        if (_product.CategoryId == null || _product.CategoryId.Value == Guid.Empty)
        {
            AddError("'categoryId' não pode ser vazio.");
        }

        if (_product.Category is not null && string.IsNullOrWhiteSpace(_product.Category.Name))
        {
            AddError("'category.name' não pode estar em branco.");
        }
    }

    private void ValidateImages()
    {
        if (_product.Images == null) return;

        int index = 0;
        foreach (var img in _product.Images)
        {
            if (img == null)
            {
                AddError($"'images[{index}]' não pode ser nula.");
                index++;
                continue;
            }

            if (string.IsNullOrWhiteSpace(img.Url))
                AddError($"'images[{index}].url' não pode estar em branco.");

            if (img.Url.Length > URL_MAX)
                AddError($"'images[{index}].url' excede {URL_MAX} caracteres.");

            index++;
        }
    }

    private void ValidateColors()
    {
        var colors = _product.Colors;

        if (colors == null || !colors.Any())
        {
            AddError("O produto deve conter pelo menos uma cor.");
            return;
        }

        int index = 0;
        foreach (var color in colors)
        {
            if (color is null)
            {
                AddError($"'colors[{index}]' não pode ser nula.");
                index++;
                continue;
            }

            if (color.ProductId == Guid.Empty)
                AddError($"'colors[{index}].productId' não pode ser vazio.");

            var value = color.Color?.Value;
            if (string.IsNullOrWhiteSpace(value))
            {
                AddError($"'colors[{index}].value' não pode estar em branco.");
            }
            else
            {
                var isHex = Regex.IsMatch(value, @"^#([0-9A-Fa-f]{3}|[0-9A-Fa-f]{6})$");
                var isNamed = Regex.IsMatch(value, @"^[A-Za-z]+$");

                if (!isHex && !isNamed)
                {
                    AddError($"'colors[{index}].value' deve ser HEX ou nome válido.");
                }
            }

            index++;
        }
    }

    private void ValidateReviews()
    {
        if (_product.Reviews == null) return;

        int index = 0;
        foreach (var review in _product.Reviews)
        {
            if (review is null)
            {
                AddError($"'reviews[{index}]' não pode ser nula.");
                index++;
                continue;
            }

            if (review.Rating < 1 || review.Rating > 5)
                AddError($"'reviews[{index}].rating' deve estar entre 1 e 5.");

            if (!string.IsNullOrWhiteSpace(review.Comment) && review.Comment.Length > 1000)
                AddError($"'reviews[{index}].comment' excede 1000 caracteres.");

            index++;
        }
    }

    private void ValidateTimestamps()
    {
        if (_product.UpdatedAt < _product.CreatedAt)
            AddError("'updatedAt' não pode ser anterior a 'createdAt'.");

        if (_product.UpdatedAt > DateTime.UtcNow.AddDays(1))
            AddError("'updatedAt' não pode estar no futuro.");
    }

    private void ValidatePromotionFlags()
    {
        if (_product.OldPrice.HasValue && _product.OldPrice.Value <= _product.Price)
        {
            AddError("'oldPrice' deve ser maior que 'price' para indicar promoção.");
        }

        if (_product.CreatedAt < DateTime.UtcNow.AddDays(-30))
        {
            AddError("Produto com mais de 30 dias não deve ser considerado novo.");
        }
    }

    private void ValidateSoftDelete()
    {
        if (!_product.IsActive && !_product.DeletedAt.HasValue)
            AddError("'deletedAt' deve ser preenchido quando o produto está inativo.");
    }

    private void AddError(string message)
    {
        ValidationHandler.Append(new Error(message));
    }
}

















// using System.Text.RegularExpressions;
// using bcommerce_server.Domain.Products;
// using bcommerce_server.Domain.Validations;
//
// namespace bcommerce_server.Domain.Products.Validators;
//
// public class ProductValidator : Validator
// {
//     private const int NAME_MIN_LENGTH = 3;
//     private const int NAME_MAX_LENGTH = 150;
//     private const int DESC_MIN_LENGTH = 10;
//     private const int DESC_MAX_LENGTH = 1000;
//
//     private readonly Product _product;
//
//     public ProductValidator(Product product, IValidationHandler handler)
//         : base(handler)
//     {
//         _product = product ?? throw new ArgumentNullException(nameof(product));
//     }
//
//     public override void Validate()
//     {
//         ValidateName();
//         ValidateDescription();
//         ValidatePrice();
//         ValidateStock();
//         ValidateColors();
//         ValidateImages();
//         ValidateReviews();
//         ValidateSoftDelete();
//     }
//
//     private void ValidateName()
//     {
//         if (string.IsNullOrWhiteSpace(_product.Name))
//         {
//             AddError("'name' não pode estar em branco.");
//             return;
//         }
//
//         int length = _product.Name.Trim().Length;
//         if (length < NAME_MIN_LENGTH || length > NAME_MAX_LENGTH)
//         {
//             AddError($"'name' deve ter entre {NAME_MIN_LENGTH} e {NAME_MAX_LENGTH} caracteres.");
//         }
//     }
//
//     private void ValidateDescription()
//     {
//         if (string.IsNullOrWhiteSpace(_product.Description))
//         {
//             AddError("'description' não pode estar em branco.");
//             return;
//         }
//
//         int length = _product.Description.Trim().Length;
//         if (length < DESC_MIN_LENGTH || length > DESC_MAX_LENGTH)
//         {
//             AddError($"'description' deve ter entre {DESC_MIN_LENGTH} e {DESC_MAX_LENGTH} caracteres.");
//         }
//     }
//
//     private void ValidatePrice()
//     {
//         if (_product.Price <= 0)
//         {
//             AddError("'price' deve ser maior que 0.");
//         }
//
//         if (_product.OldPrice.HasValue && _product.OldPrice.Value < 0)
//         {
//             AddError("'oldPrice' não pode ser negativo.");
//         }
//     }
//
//     private void ValidateStock()
//     {
//         if (_product.StockQuantity < 0)
//         {
//             AddError("'stockQuantity' não pode ser negativo.");
//         }
//
//         if (_product.Sold < 0)
//         {
//             AddError("'sold' não pode ser negativo.");
//         }
//     }
//
//     private void ValidateColors()
//     {
//         int index = 0;
//         foreach (var color in _product.Colors)
//         {
//             if (color is null)
//             {
//                 AddError($"'colors[{index}]' não pode ser nulo.");
//                 index++;
//                 continue;
//             }
//
//             var value = color.Color.Value;
//
//             if (string.IsNullOrWhiteSpace(value))
//                 AddError($"'colors[{index}].value' não pode estar em branco.");
//
//             if (value.Length > 20)
//                 AddError($"'colors[{index}].value' excede o limite de 20 caracteres.");
//
//             // Regex opcional para validar formatos (#FFF, #FFFFFF, nomes)
//             var colorRegex = new Regex(@"^#([0-9A-Fa-f]{3}|[0-9A-Fa-f]{6})$|^[a-zA-Z]+$");
//             if (!colorRegex.IsMatch(value))
//             {
//                 AddError($"'colors[{index}].value' deve ser um código hex ou nome de cor.");
//             }
//
//             index++;
//         }
//     }
//
//     private void ValidateImages()
//     {
//         int index = 0;
//         foreach (var image in _product.Images)
//         {
//             if (image is null)
//             {
//                 AddError($"'images[{index}]' não pode ser nula.");
//                 index++;
//                 continue;
//             }
//
//             if (string.IsNullOrWhiteSpace(image.Url))
//             {
//                 AddError($"'images[{index}].url' não pode estar em branco.");
//             }
//
//             if (image.Url.Length > 2048)
//             {
//                 AddError($"'images[{index}].url' excede o limite de 2048 caracteres.");
//             }
//
//             index++;
//         }
//     }
//
//     private void ValidateReviews()
//     {
//         int index = 0;
//         foreach (var review in _product.Reviews)
//         {
//             if (review is null)
//             {
//                 AddError($"'reviews[{index}]' não pode ser nulo.");
//                 index++;
//                 continue;
//             }
//
//             if (review.Rating < 1 || review.Rating > 5)
//             {
//                 AddError($"'reviews[{index}].rating' deve estar entre 1 e 5.");
//             }
//
//             if (review.Comment?.Length > 1000)
//             {
//                 AddError($"'reviews[{index}].comment' excede o limite de 1000 caracteres.");
//             }
//
//             index++;
//         }
//     }
//
//     private void ValidateSoftDelete()
//     {
//         if (!_product.IsActive && _product.DeletedAt is null)
//         {
//             AddError("'deletedAt' deve estar definido quando o produto está inativo.");
//         }
//     }
//
//     private void AddError(string message)
//     {
//         ValidationHandler.Append(new Error(message));
//     }
// }


// using System.Text.RegularExpressions;
// using bcommerce_server.Domain.Products;
// using bcommerce_server.Domain.Validations;
//
// namespace bcommerce_server.Domain.Products.Validators;
//
// public class ProductValidator : Validator
// {
//     private const int NAME_MIN_LENGTH = 3;
//     private const int NAME_MAX_LENGTH = 150;
//
//     private const int DESCRIPTION_MIN_LENGTH = 10;
//     private const int DESCRIPTION_MAX_LENGTH = 1000;
//
//     private readonly Product _product;
//
//     public ProductValidator(Product product, IValidationHandler handler)
//         : base(handler)
//     {
//         _product = product ?? throw new ArgumentNullException(nameof(product));
//     }
//
//     public override void Validate()
//     {
//         ValidateName();
//         ValidateDescription();
//         ValidatePrice();
//         ValidateOldPrice();
//         ValidateImages();
//         ValidateCategory();
//         ValidateColors();
//         ValidateStock();
//         ValidateSold();
//         ValidateTimestamps(); // 🆕
//         ValidateDerivedFlags();
//     }
//
//     private void ValidateName()
//     {
//         var name = _product.Name;
//
//         if (string.IsNullOrWhiteSpace(name))
//         {
//             AddError("'nome' do produto não pode estar em branco.");
//             return;
//         }
//
//         var length = name.Trim().Length;
//         if (length < NAME_MIN_LENGTH || length > NAME_MAX_LENGTH)
//         {
//             AddError($"'nome' deve ter entre {NAME_MIN_LENGTH} e {NAME_MAX_LENGTH} caracteres.");
//         }
//     }
//
//     private void ValidateDescription()
//     {
//         var desc = _product.Description;
//
//         if (string.IsNullOrWhiteSpace(desc))
//         {
//             AddError("'descrição' do produto não pode estar em branco.");
//             return;
//         }
//
//         var length = desc.Trim().Length;
//         if (length < DESCRIPTION_MIN_LENGTH || length > DESCRIPTION_MAX_LENGTH)
//         {
//             AddError($"'descrição' deve ter entre {DESCRIPTION_MIN_LENGTH} e {DESCRIPTION_MAX_LENGTH} caracteres.");
//         }
//     }
//
//     private void ValidatePrice()
//     {
//         var price = _product.Price;
//
//         if (price is null)
//         {
//             AddError("'preço' não pode ser nulo.");
//             return;
//         }
//
//         if (price.Amount <= 0)
//         {
//             AddError("'preço' deve ser maior que zero.");
//         }
//     }
//
//     private void ValidateOldPrice()
//     {
//         var oldPrice = _product.OldPrice;
//
//         if (oldPrice is not null && oldPrice.Amount < 0)
//         {
//             AddError("'preço antigo' não pode ser negativo.");
//         }
//     }
//
//     private void ValidateImages()
//     {
//         var images = _product.Images;
//
//         // ✅ Permite lista nula ou vazia (regra opcional agora)
//         if (images == null)
//         {
//             // Comentado para não obrigar imagem
//             // AddError("O produto deve ter pelo menos uma imagem.");
//             return;
//         }
//
//         if (!images.Any())
//         {
//             // Comentado para permitir lista vazia
//             // AddError("O produto deve ter pelo menos uma imagem.");
//             return;
//         }
//
//         int index = 0;
//         foreach (var image in images)
//         {
//             if (image is null)
//             {
//                 AddError($"'imagens[{index}]' não pode ser nula.");
//                 continue;
//             }
//
//             if (string.IsNullOrWhiteSpace(image.Url))
//             {
//                 AddError($"'imagens[{index}].url' não pode estar em branco.");
//                 continue;
//             }
//
//             var valid = Regex.IsMatch(image.Url, @"^https?:\/\/.+\.(jpg|jpeg|png|webp|svg)$", RegexOptions.IgnoreCase);
//             if (!valid)
//             {
//                 AddError($"'imagens[{index}].url' possui formato inválido.");
//             }
//
//             index++;
//         }
//     }
//
//     private void ValidateCategory()
//     {
//         var category = _product.Category;
//
//         if (category is null)
//         {
//             AddError("'categoria' do produto não pode ser nula.");
//             return;
//         }
//
//         if (string.IsNullOrWhiteSpace(category.Name))
//         {
//             AddError("'categoria' do produto não pode estar em branco.");
//         }
//     }
//
//     private void ValidateColors()
//     {
//         var colors = _product.Colors;
//
//         if (colors is null || !colors.Any())
//         {
//             AddError("O produto deve conter pelo menos uma cor.");
//             return;
//         }
//
//         int index = 0;
//         foreach (var color in colors)
//         {
//             if (color is null)
//             {
//                 AddError($"'cores[{index}]' não pode ser nula.");
//                 continue;
//             }
//
//             if (string.IsNullOrWhiteSpace(color.Value))
//             {
//                 AddError($"'cores[{index}]' não pode estar em branco.");
//             }
//
//             index++;
//         }
//     }
//
//     private void ValidateStock()
//     {
//         if (_product.Stock is null)
//         {
//             AddError("'estoque' não pode ser nulo.");
//             return;
//         }
//
//         if (_product.Stock.Quantity < 0)
//         {
//             AddError("'estoque' não pode ser negativo.");
//         }
//     }
//
//     private void ValidateSold()
//     {
//         if (_product.Sold < 0)
//         {
//             AddError("'vendidos' não pode ser negativo.");
//         }
//     }
//
//     private void ValidateTimestamps()
//     {
//         if (_product.UpdatedAt < _product.CreatedAt)
//         {
//             AddError("'data de atualização' não pode ser anterior à 'data de criação'.");
//         }
//
//         if (_product.UpdatedAt > DateTime.UtcNow.AddDays(1))
//         {
//             AddError("'data de atualização' não pode estar no futuro.");
//         }
//     }
//
//     private void ValidateDerivedFlags()
//     {
//         if (_product.OldPrice is not null && _product.OldPrice.Amount <= _product.Price.Amount)
//         {
//             AddError("'preço antigo' deve ser maior que o preço atual para o produto estar em promoção.");
//         }
//
//         if (_product.CreatedAt < DateTime.UtcNow.AddDays(-30))
//         {
//             AddError("Produto com mais de 30 dias não pode ser considerado 'novo'.");
//         }
//     }
//
//     private void AddError(string message)
//     {
//         ValidationHandler.Append(new Error(message));
//     }
// }
//
// // using System.Text.RegularExpressions;
// // using bcommerce_server.Domain.Products;
// // using bcommerce_server.Domain.Validations;
// //
// // namespace bcommerce_server.Domain.Products.Validators;
// //
// // /// <summary>
// // /// Validador da entidade Product.
// // /// </summary>
// // public class ProductValidator : Validator
// // {
// //     private const int NAME_MIN_LENGTH = 3;
// //     private const int NAME_MAX_LENGTH = 150;
// //
// //     private const int DESCRIPTION_MIN_LENGTH = 10;
// //     private const int DESCRIPTION_MAX_LENGTH = 1000;
// //
// //     private readonly Product _product;
// //
// //     public ProductValidator(Product product, IValidationHandler handler)
// //         : base(handler)
// //     {
// //         _product = product ?? throw new ArgumentNullException(nameof(product));
// //     }
// //
// //     public override void Validate()
// //     {
// //         ValidateName();
// //         ValidateDescription();
// //         ValidatePrice();
// //         ValidateOldPrice();
// //         ValidateImages();
// //         ValidateCategory();
// //         ValidateColors();
// //         ValidateStock();
// //         ValidateSold();
// //         ValidateDerivedFlags();
// //     }
// //
// //     private void ValidateName()
// //     {
// //         var name = _product.Name;
// //
// //         if (string.IsNullOrWhiteSpace(name))
// //         {
// //             AddError("'nome' do produto não pode estar em branco.");
// //             return;
// //         }
// //
// //         var length = name.Trim().Length;
// //         if (length < NAME_MIN_LENGTH || length > NAME_MAX_LENGTH)
// //         {
// //             AddError($"'nome' deve ter entre {NAME_MIN_LENGTH} e {NAME_MAX_LENGTH} caracteres.");
// //         }
// //     }
// //
// //     private void ValidateDescription()
// //     {
// //         var desc = _product.Description;
// //
// //         if (string.IsNullOrWhiteSpace(desc))
// //         {
// //             AddError("'descrição' do produto não pode estar em branco.");
// //             return;
// //         }
// //
// //         var length = desc.Trim().Length;
// //         if (length < DESCRIPTION_MIN_LENGTH || length > DESCRIPTION_MAX_LENGTH)
// //         {
// //             AddError($"'descrição' deve ter entre {DESCRIPTION_MIN_LENGTH} e {DESCRIPTION_MAX_LENGTH} caracteres.");
// //         }
// //     }
// //
// //     private void ValidatePrice()
// //     {
// //         var price = _product.Price;
// //
// //         if (price is null)
// //         {
// //             AddError("'preço' não pode ser nulo.");
// //             return;
// //         }
// //
// //         if (price.Amount <= 0)
// //         {
// //             AddError("'preço' deve ser maior que zero.");
// //         }
// //     }
// //
// //     private void ValidateOldPrice()
// //     {
// //         var oldPrice = _product.OldPrice;
// //
// //         if (oldPrice is not null && oldPrice.Amount < 0)
// //         {
// //             AddError("'preço antigo' não pode ser negativo.");
// //         }
// //     }
// //
// //     private void ValidateImages()
// //     {
// //         var images = _product.Images;
// //
// //         if (images is null || !images.Any())
// //         {
// //             AddError("O produto deve ter pelo menos uma imagem.");
// //             return;
// //         }
// //
// //         int index = 0;
// //         foreach (var image in images)
// //         {
// //             if (image is null)
// //             {
// //                 AddError($"'imagens[{index}]' não pode ser nula.");
// //                 continue;
// //             }
// //
// //             if (string.IsNullOrWhiteSpace(image.Url))
// //             {
// //                 AddError($"'imagens[{index}].url' não pode estar em branco.");
// //                 continue;
// //             }
// //
// //             var valid = Regex.IsMatch(image.Url, @"^https?:\/\/.+\.(jpg|jpeg|png|webp|svg)$", RegexOptions.IgnoreCase);
// //             if (!valid)
// //             {
// //                 AddError($"'imagens[{index}].url' possui formato inválido.");
// //             }
// //
// //             index++;
// //         }
// //     }
// //
// //     private void ValidateCategory()
// //     {
// //         var category = _product.Category;
// //
// //         if (category is null)
// //         {
// //             AddError("'categoria' do produto não pode ser nula.");
// //             return;
// //         }
// //
// //         if (string.IsNullOrWhiteSpace(category.Name))
// //         {
// //             AddError("'categoria' do produto não pode estar em branco.");
// //         }
// //     }
// //
// //     private void ValidateColors()
// //     {
// //         var colors = _product.Colors;
// //
// //         if (colors is null || !colors.Any())
// //         {
// //             AddError("O produto deve conter pelo menos uma cor.");
// //             return;
// //         }
// //
// //         int index = 0;
// //         foreach (var color in colors)
// //         {
// //             if (color is null)
// //             {
// //                 AddError($"'cores[{index}]' não pode ser nula.");
// //                 continue;
// //             }
// //
// //             if (string.IsNullOrWhiteSpace(color.Value))
// //             {
// //                 AddError($"'cores[{index}]' não pode estar em branco.");
// //             }
// //
// //             index++;
// //         }
// //     }
// //
// //     private void ValidateStock()
// //     {
// //         if (_product.Stock is null)
// //         {
// //             AddError("'estoque' não pode ser nulo.");
// //             return;
// //         }
// //
// //         if (_product.Stock.Quantity < 0)
// //         {
// //             AddError("'estoque' não pode ser negativo.");
// //         }
// //     }
// //
// //     private void ValidateSold()
// //     {
// //         if (_product.Sold < 0)
// //         {
// //             AddError("'vendidos' não pode ser negativo.");
// //         }
// //     }
// //
// //     private void ValidateDerivedFlags()
// //     {
// //         if (_product.OldPrice is not null && _product.OldPrice.Amount <= _product.Price.Amount)
// //         {
// //             AddError("'preço antigo' deve ser maior que o preço atual para o produto estar em promoção.");
// //         }
// //
// //         if (_product.CreatedAt < DateTime.UtcNow.AddDays(-30))
// //         {
// //             AddError("Produto com mais de 30 dias não pode ser considerado 'novo'.");
// //         }
// //     }
// //
// //     private void AddError(string message)
// //     {
// //         ValidationHandler.Append(new Error(message));
// //     }
// // }
// //
// // // using System.Text.RegularExpressions;
// // // using bcommerce_server.Domain.Products;
// // // using bcommerce_server.Domain.Validations;
// // //
// // // namespace bcommerce_server.Domain.Products.Validators;
// // //
// // // /// <summary>
// // // /// Validador da entidade Product.
// // // /// </summary>
// // // public class ProductValidator : Validator
// // // {
// // //     private const int NAME_MIN_LENGTH = 3;
// // //     private const int NAME_MAX_LENGTH = 150;
// // //
// // //     private const int DESCRIPTION_MIN_LENGTH = 10;
// // //     private const int DESCRIPTION_MAX_LENGTH = 1000;
// // //
// // //     private readonly Product _product;
// // //
// // //     public ProductValidator(Product product, IValidationHandler handler)
// // //         : base(handler)
// // //     {
// // //         _product = product ?? throw new ArgumentNullException(nameof(product));
// // //     }
// // //
// // //     public override void Validate()
// // //     {
// // //         ValidateName();
// // //         ValidateDescription();
// // //         ValidatePrice();
// // //         ValidateOldPrice();
// // //         ValidateImages();
// // //         ValidateCategory();
// // //         ValidateColors();
// // //         ValidateStock();
// // //         ValidateSold();
// // //         ValidateDerivedFlags(); // ← atualizado
// // //     }
// // //
// // //     private void ValidateName()
// // //     {
// // //         var name = _product.Name;
// // //
// // //         if (string.IsNullOrWhiteSpace(name))
// // //         {
// // //             AddError("'nome' do produto não pode estar em branco.");
// // //             return;
// // //         }
// // //
// // //         var length = name.Trim().Length;
// // //         if (length < NAME_MIN_LENGTH || length > NAME_MAX_LENGTH)
// // //         {
// // //             AddError($"'nome' deve ter entre {NAME_MIN_LENGTH} e {NAME_MAX_LENGTH} caracteres.");
// // //         }
// // //     }
// // //
// // //     private void ValidateDescription()
// // //     {
// // //         var desc = _product.Description;
// // //
// // //         if (string.IsNullOrWhiteSpace(desc))
// // //         {
// // //             AddError("'descrição' do produto não pode estar em branco.");
// // //             return;
// // //         }
// // //
// // //         var length = desc.Trim().Length;
// // //         if (length < DESCRIPTION_MIN_LENGTH || length > DESCRIPTION_MAX_LENGTH)
// // //         {
// // //             AddError($"'descrição' deve ter entre {DESCRIPTION_MIN_LENGTH} e {DESCRIPTION_MAX_LENGTH} caracteres.");
// // //         }
// // //     }
// // //
// // //     private void ValidatePrice()
// // //     {
// // //         var price = _product.Price;
// // //
// // //         if (price is null)
// // //         {
// // //             AddError("'preço' não pode ser nulo.");
// // //             return;
// // //         }
// // //
// // //         if (price.Amount <= 0)
// // //         {
// // //             AddError("'preço' deve ser maior que zero.");
// // //         }
// // //     }
// // //
// // //     private void ValidateOldPrice()
// // //     {
// // //         var oldPrice = _product.OldPrice;
// // //
// // //         if (oldPrice is not null && oldPrice.Amount < 0)
// // //         {
// // //             AddError("'preço antigo' não pode ser negativo.");
// // //         }
// // //     }
// // //
// // //     private void ValidateImages()
// // //     {
// // //         var images = _product.Images;
// // //
// // //         if (images is null || !images.Any())
// // //         {
// // //             AddError("O produto deve ter pelo menos uma imagem.");
// // //             return;
// // //         }
// // //
// // //         int index = 0;
// // //         foreach (var image in images)
// // //         {
// // //             if (image is null)
// // //             {
// // //                 AddError($"'imagens[{index}]' não pode ser nula.");
// // //                 continue;
// // //             }
// // //
// // //             if (string.IsNullOrWhiteSpace(image.Url))
// // //             {
// // //                 AddError($"'imagens[{index}].url' não pode estar em branco.");
// // //                 continue;
// // //             }
// // //
// // //             var valid = Regex.IsMatch(image.Url, @"^https?:\/\/.+\.(jpg|jpeg|png|webp|svg)$", RegexOptions.IgnoreCase);
// // //             if (!valid)
// // //             {
// // //                 AddError($"'imagens[{index}].url' possui formato inválido.");
// // //             }
// // //
// // //             index++;
// // //         }
// // //     }
// // //
// // //     private void ValidateCategory()
// // //     {
// // //         var category = _product.Category;
// // //
// // //         if (category is null)
// // //         {
// // //             AddError("'categoria' do produto não pode ser nula.");
// // //             return;
// // //         }
// // //
// // //         if (string.IsNullOrWhiteSpace(category.Name))
// // //         {
// // //             AddError("'categoria' do produto não pode estar em branco.");
// // //         }
// // //     }
// // //
// // //     private void ValidateColors()
// // //     {
// // //         var colors = _product.Colors;
// // //
// // //         if (colors is null || !colors.Any())
// // //         {
// // //             AddError("O produto deve conter pelo menos uma cor.");
// // //             return;
// // //         }
// // //
// // //         int index = 0;
// // //         foreach (var color in colors)
// // //         {
// // //             if (color is null)
// // //             {
// // //                 AddError($"'cores[{index}]' não pode ser nula.");
// // //                 continue;
// // //             }
// // //
// // //             if (string.IsNullOrWhiteSpace(color.Value))
// // //             {
// // //                 AddError($"'cores[{index}]' não pode estar em branco.");
// // //             }
// // //
// // //             index++;
// // //         }
// // //     }
// // //
// // //     private void ValidateStock()
// // //     {
// // //         if (_product.Stock.Quantity < 0)
// // //         {
// // //             AddError("'estoque' não pode ser negativo.");
// // //         }
// // //     }
// // //
// // //     private void ValidateSold()
// // //     {
// // //         if (_product.Sold < 0)
// // //         {
// // //             AddError("'vendidos' não pode ser negativo.");
// // //         }
// // //     }
// // //
// // //     /// <summary>
// // //     /// Valida coerência lógica de propriedades derivadas como IsNew e Sale.
// // //     /// </summary>
// // //     private void ValidateDerivedFlags()
// // //     {
// // //         // 🟠 Sale: só faz sentido se oldPrice > price
// // //         if (_product.OldPrice is not null && _product.OldPrice.Amount <= _product.Price.Amount)
// // //         {
// // //             AddError("'preço antigo' deve ser maior que o preço atual para o produto estar em promoção.");
// // //         }
// // //
// // //         // 🟠 IsNew: apenas se o produto tem até 30 dias
// // //         if (_product.CreatedAt < DateTime.UtcNow.AddDays(-30))
// // //         {
// // //             AddError("Produto com mais de 30 dias não pode ser considerado 'novo'.");
// // //         }
// // //     }
// // //
// // //     private void AddError(string message)
// // //     {
// // //         ValidationHandler.Append(new Error(message));
// // //     }
// // // }
// //
// // // using System.Text.RegularExpressions;
// // // using bcommerce_server.Domain.Products;
// // // using bcommerce_server.Domain.Validations;
// // //
// // // namespace bcommerce_server.Domain.Products.Validators;
// // //
// // // /// <summary>
// // // /// Validador da entidade Product.
// // // /// </summary>
// // // public class ProductValidator : Validator
// // // {
// // //     private const int NAME_MIN_LENGTH = 3;
// // //     private const int NAME_MAX_LENGTH = 150;
// // //
// // //     private const int DESCRIPTION_MIN_LENGTH = 10;
// // //     private const int DESCRIPTION_MAX_LENGTH = 1000;
// // //
// // //     private readonly Product _product;
// // //
// // //     public ProductValidator(Product product, IValidationHandler handler)
// // //         : base(handler)
// // //     {
// // //         _product = product ?? throw new ArgumentNullException(nameof(product));
// // //     }
// // //
// // //     public override void Validate()
// // //     {
// // //         ValidateName();
// // //         ValidateDescription();
// // //         ValidatePrice();
// // //         ValidateOldPrice();
// // //         ValidateImages();
// // //         ValidateCategory();
// // //         ValidateColors();
// // //         ValidateStock();
// // //         ValidateSold();
// // //         ValidateActivationFlags();
// // //     }
// // //
// // //     private void ValidateName()
// // //     {
// // //         var name = _product.Name;
// // //
// // //         if (string.IsNullOrWhiteSpace(name))
// // //         {
// // //             AddError("'nome' do produto não pode estar em branco.");
// // //             return;
// // //         }
// // //
// // //         var length = name.Trim().Length;
// // //         if (length < NAME_MIN_LENGTH || length > NAME_MAX_LENGTH)
// // //         {
// // //             AddError($"'nome' deve ter entre {NAME_MIN_LENGTH} e {NAME_MAX_LENGTH} caracteres.");
// // //         }
// // //     }
// // //
// // //     private void ValidateDescription()
// // //     {
// // //         var desc = _product.Description;
// // //
// // //         if (string.IsNullOrWhiteSpace(desc))
// // //         {
// // //             AddError("'descrição' do produto não pode estar em branco.");
// // //             return;
// // //         }
// // //
// // //         var length = desc.Trim().Length;
// // //         if (length < DESCRIPTION_MIN_LENGTH || length > DESCRIPTION_MAX_LENGTH)
// // //         {
// // //             AddError($"'descrição' deve ter entre {DESCRIPTION_MIN_LENGTH} e {DESCRIPTION_MAX_LENGTH} caracteres.");
// // //         }
// // //     }
// // //
// // //     private void ValidatePrice()
// // //     {
// // //         var price = _product.Price;
// // //
// // //         if (price is null)
// // //         {
// // //             AddError("'preço' não pode ser nulo.");
// // //             return;
// // //         }
// // //
// // //         if (price.Amount <= 0)
// // //         {
// // //             AddError("'preço' deve ser maior que zero.");
// // //         }
// // //     }
// // //
// // //     private void ValidateOldPrice()
// // //     {
// // //         var oldPrice = _product.OldPrice;
// // //
// // //         if (oldPrice is not null && oldPrice.Amount < 0)
// // //         {
// // //             AddError("'preço antigo' não pode ser negativo.");
// // //         }
// // //     }
// // //
// // //     private void ValidateImages()
// // //     {
// // //         var images = _product.Images;
// // //
// // //         if (images is null || !images.Any())
// // //         {
// // //             AddError("O produto deve ter pelo menos uma imagem.");
// // //             return;
// // //         }
// // //
// // //         int index = 0;
// // //         foreach (var image in images)
// // //         {
// // //             if (image is null)
// // //             {
// // //                 AddError($"'imagens[{index}]' não pode ser nula.");
// // //                 continue;
// // //             }
// // //
// // //             if (string.IsNullOrWhiteSpace(image.Url))
// // //             {
// // //                 AddError($"'imagens[{index}].url' não pode estar em branco.");
// // //                 continue;
// // //             }
// // //
// // //             var valid = Regex.IsMatch(image.Url, @"^https?:\/\/.+\.(jpg|jpeg|png|webp|svg)$", RegexOptions.IgnoreCase);
// // //             if (!valid)
// // //             {
// // //                 AddError($"'imagens[{index}].url' possui formato inválido.");
// // //             }
// // //
// // //             index++;
// // //         }
// // //     }
// // //
// // //     private void ValidateCategory()
// // //     {
// // //         var category = _product.Category;
// // //
// // //         if (category is null)
// // //         {
// // //             AddError("'categoria' do produto não pode ser nula.");
// // //             return;
// // //         }
// // //
// // //         if (string.IsNullOrWhiteSpace(category.Name))
// // //         {
// // //             AddError("'categoria' do produto não pode estar em branco.");
// // //         }
// // //     }
// // //
// // //     private void ValidateColors()
// // //     {
// // //         var colors = _product.Colors;
// // //
// // //         if (colors is null || !colors.Any())
// // //         {
// // //             AddError("O produto deve conter pelo menos uma cor.");
// // //             return;
// // //         }
// // //
// // //         int index = 0;
// // //         foreach (var color in colors)
// // //         {
// // //             if (color is null)
// // //             {
// // //                 AddError($"'cores[{index}]' não pode ser nula.");
// // //                 continue;
// // //             }
// // //
// // //             if (string.IsNullOrWhiteSpace(color.Value))
// // //             {
// // //                 AddError($"'cores[{index}]' não pode estar em branco.");
// // //             }
// // //
// // //             index++;
// // //         }
// // //     }
// // //
// // //     private void ValidateStock()
// // //     {
// // //         if (_product.Stock < 0)
// // //         {
// // //             AddError("'estoque' não pode ser negativo.");
// // //         }
// // //     }
// // //
// // //     private void ValidateSold()
// // //     {
// // //         if (_product.Sold < 0)
// // //         {
// // //             AddError("'vendidos' não pode ser negativo.");
// // //         }
// // //     }
// // //
// // //     private void ValidateActivationFlags()
// // //     {
// // //         // Sale depende de OldPrice < Price
// // //         if (_product.OldPrice != null && _product.OldPrice.Amount <= _product.Price.Amount)
// // //         {
// // //             AddError("'preço antigo' deve ser maior que o preço atual para caracterizar uma promoção.");
// // //         }
// // //
// // //         // IsNew depende da data de criação
// // //         if (_product.CreatedAt < DateTime.UtcNow.AddDays(-30))
// // //         {
// // //             AddError("Produto com mais de 30 dias não deve ser considerado 'novo'.");
// // //         }
// // //     }
// // //
// // //     private void AddError(string message)
// // //     {
// // //         ValidationHandler.Append(new Error(message));
// // //     }
// // // }
// // //
// // //
// // // // using System.Text.RegularExpressions;
// // // // using bcommerce_server.Domain.Validations;
// // // //
// // // // namespace bcommerce_server.Domain.Products.Validators;
// // // //
// // // // /// <summary>
// // // // /// Validador da entidade Product.
// // // // /// </summary>
// // // // public class ProductValidator : Validator
// // // // {
// // // //     private const int NAME_MIN_LENGTH = 3;
// // // //     private const int NAME_MAX_LENGTH = 150;
// // // //
// // // //     private const int DESCRIPTION_MIN_LENGTH = 10;
// // // //     private const int DESCRIPTION_MAX_LENGTH = 1000;
// // // //
// // // //     private readonly Product _product;
// // // //
// // // //     public ProductValidator(Product product, IValidationHandler handler)
// // // //         : base(handler)
// // // //     {
// // // //         _product = product ?? throw new ArgumentNullException(nameof(product));
// // // //     }
// // // //
// // // //     public override void Validate()
// // // //     {
// // // //         ValidateName();
// // // //         ValidateDescription();
// // // //         ValidatePrice();
// // // //         ValidateImages();
// // // //         ValidateCategory();
// // // //         ValidateColors();
// // // //     }
// // // //
// // // //     private void ValidateName()
// // // //     {
// // // //         var name = _product.Name;
// // // //
// // // //         if (string.IsNullOrWhiteSpace(name))
// // // //         {
// // // //             AddError("'nome' do produto não pode estar em branco.");
// // // //             return;
// // // //         }
// // // //
// // // //         var length = name.Trim().Length;
// // // //         if (length < NAME_MIN_LENGTH || length > NAME_MAX_LENGTH)
// // // //         {
// // // //             AddError($"'nome' deve ter entre {NAME_MIN_LENGTH} e {NAME_MAX_LENGTH} caracteres.");
// // // //         }
// // // //     }
// // // //
// // // //     private void ValidateDescription()
// // // //     {
// // // //         var desc = _product.Description;
// // // //
// // // //         if (string.IsNullOrWhiteSpace(desc))
// // // //         {
// // // //             AddError("'descrição' do produto não pode estar em branco.");
// // // //             return;
// // // //         }
// // // //
// // // //         var length = desc.Trim().Length;
// // // //         if (length < DESCRIPTION_MIN_LENGTH || length > DESCRIPTION_MAX_LENGTH)
// // // //         {
// // // //             AddError($"'descrição' deve ter entre {DESCRIPTION_MIN_LENGTH} e {DESCRIPTION_MAX_LENGTH} caracteres.");
// // // //         }
// // // //     }
// // // //
// // // //     private void ValidatePrice()
// // // //     {
// // // //         var price = _product.Price;
// // // //
// // // //         if (price is null)
// // // //         {
// // // //             AddError("'preço' não pode ser nulo.");
// // // //             return;
// // // //         }
// // // //
// // // //         if (price.Amount < 0)
// // // //         {
// // // //             AddError("'preço' não pode ser negativo.");
// // // //         }
// // // //
// // // //         if (price.Amount == 0)
// // // //         {
// // // //             AddError("'preço' deve ser maior que zero.");
// // // //         }
// // // //     }
// // // //
// // // //     private void ValidateImages()
// // // //     {
// // // //         var images = _product.Images;
// // // //
// // // //         if (images is null || !images.Any())
// // // //         {
// // // //             AddError("O produto deve ter pelo menos uma imagem.");
// // // //             return;
// // // //         }
// // // //
// // // //         int index = 0;
// // // //         foreach (var image in images)
// // // //         {
// // // //             if (image is null)
// // // //             {
// // // //                 AddError($"'imagens[{index}]' não pode ser nula.");
// // // //                 continue;
// // // //             }
// // // //
// // // //             if (string.IsNullOrWhiteSpace(image.Url))
// // // //             {
// // // //                 AddError($"'imagens[{index}].url' não pode estar em branco.");
// // // //                 continue;
// // // //             }
// // // //
// // // //             var valid = Regex.IsMatch(image.Url, @"^https?:\/\/.+\.(jpg|jpeg|png|webp|svg)$", RegexOptions.IgnoreCase);
// // // //             if (!valid)
// // // //             {
// // // //                 AddError($"'imagens[{index}].url' possui formato inválido.");
// // // //             }
// // // //
// // // //             index++;
// // // //         }
// // // //     }
// // // //
// // // //     private void ValidateCategory()
// // // //     {
// // // //         var category = _product.Category;
// // // //
// // // //         if (category is null)
// // // //         {
// // // //             AddError("'categoria' do produto não pode ser nula.");
// // // //             return;
// // // //         }
// // // //
// // // //         if (string.IsNullOrWhiteSpace(category.Name))
// // // //         {
// // // //             AddError("'categoria' do produto não pode estar em branco.");
// // // //         }
// // // //     }
// // // //
// // // //     private void ValidateColors()
// // // //     {
// // // //         var colors = _product.Colors;
// // // //
// // // //         if (colors is null || !colors.Any())
// // // //         {
// // // //             AddError("O produto deve conter pelo menos uma cor.");
// // // //             return;
// // // //         }
// // // //
// // // //         int index = 0;
// // // //         foreach (var color in colors)
// // // //         {
// // // //             if (color is null)
// // // //             {
// // // //                 AddError($"'cores[{index}]' não pode ser nula.");
// // // //                 continue;
// // // //             }
// // // //
// // // //             if (string.IsNullOrWhiteSpace(color.Value))
// // // //             {
// // // //                 AddError($"'cores[{index}]' não pode estar em branco.");
// // // //             }
// // // //
// // // //             index++;
// // // //         }
// // // //     }
// // // //
// // // //     private void AddError(string message)
// // // //     {
// // // //         ValidationHandler.Append(new Error(message));
// // // //     }
// // // // }