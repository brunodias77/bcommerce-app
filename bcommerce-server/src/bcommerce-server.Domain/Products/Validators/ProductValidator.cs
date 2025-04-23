using System.Text.RegularExpressions;
using bcommerce_server.Domain.Products;
using bcommerce_server.Domain.Validations;

namespace bcommerce_server.Domain.Products.Validators;

public class ProductValidator : Validator
{
    private const int NAME_MIN_LENGTH = 3;
    private const int NAME_MAX_LENGTH = 150;

    private const int DESCRIPTION_MIN_LENGTH = 10;
    private const int DESCRIPTION_MAX_LENGTH = 1000;

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
        ValidateImages();
        ValidateCategory();
        ValidateColors();
        ValidateStock();
        ValidateSold();
        ValidateTimestamps(); // 🆕
        ValidateDerivedFlags();
    }

    private void ValidateName()
    {
        var name = _product.Name;

        if (string.IsNullOrWhiteSpace(name))
        {
            AddError("'nome' do produto não pode estar em branco.");
            return;
        }

        var length = name.Trim().Length;
        if (length < NAME_MIN_LENGTH || length > NAME_MAX_LENGTH)
        {
            AddError($"'nome' deve ter entre {NAME_MIN_LENGTH} e {NAME_MAX_LENGTH} caracteres.");
        }
    }

    private void ValidateDescription()
    {
        var desc = _product.Description;

        if (string.IsNullOrWhiteSpace(desc))
        {
            AddError("'descrição' do produto não pode estar em branco.");
            return;
        }

        var length = desc.Trim().Length;
        if (length < DESCRIPTION_MIN_LENGTH || length > DESCRIPTION_MAX_LENGTH)
        {
            AddError($"'descrição' deve ter entre {DESCRIPTION_MIN_LENGTH} e {DESCRIPTION_MAX_LENGTH} caracteres.");
        }
    }

    private void ValidatePrice()
    {
        var price = _product.Price;

        if (price is null)
        {
            AddError("'preço' não pode ser nulo.");
            return;
        }

        if (price.Amount <= 0)
        {
            AddError("'preço' deve ser maior que zero.");
        }
    }

    private void ValidateOldPrice()
    {
        var oldPrice = _product.OldPrice;

        if (oldPrice is not null && oldPrice.Amount < 0)
        {
            AddError("'preço antigo' não pode ser negativo.");
        }
    }

    private void ValidateImages()
    {
        var images = _product.Images;

        if (images is null || !images.Any())
        {
            AddError("O produto deve ter pelo menos uma imagem.");
            return;
        }

        int index = 0;
        foreach (var image in images)
        {
            if (image is null)
            {
                AddError($"'imagens[{index}]' não pode ser nula.");
                continue;
            }

            if (string.IsNullOrWhiteSpace(image.Url))
            {
                AddError($"'imagens[{index}].url' não pode estar em branco.");
                continue;
            }

            var valid = Regex.IsMatch(image.Url, @"^https?:\/\/.+\.(jpg|jpeg|png|webp|svg)$", RegexOptions.IgnoreCase);
            if (!valid)
            {
                AddError($"'imagens[{index}].url' possui formato inválido.");
            }

            index++;
        }
    }

    private void ValidateCategory()
    {
        var category = _product.Category;

        if (category is null)
        {
            AddError("'categoria' do produto não pode ser nula.");
            return;
        }

        if (string.IsNullOrWhiteSpace(category.Name))
        {
            AddError("'categoria' do produto não pode estar em branco.");
        }
    }

    private void ValidateColors()
    {
        var colors = _product.Colors;

        if (colors is null || !colors.Any())
        {
            AddError("O produto deve conter pelo menos uma cor.");
            return;
        }

        int index = 0;
        foreach (var color in colors)
        {
            if (color is null)
            {
                AddError($"'cores[{index}]' não pode ser nula.");
                continue;
            }

            if (string.IsNullOrWhiteSpace(color.Value))
            {
                AddError($"'cores[{index}]' não pode estar em branco.");
            }

            index++;
        }
    }

    private void ValidateStock()
    {
        if (_product.Stock is null)
        {
            AddError("'estoque' não pode ser nulo.");
            return;
        }

        if (_product.Stock.Quantity < 0)
        {
            AddError("'estoque' não pode ser negativo.");
        }
    }

    private void ValidateSold()
    {
        if (_product.Sold < 0)
        {
            AddError("'vendidos' não pode ser negativo.");
        }
    }

    private void ValidateTimestamps()
    {
        if (_product.UpdatedAt < _product.CreatedAt)
        {
            AddError("'data de atualização' não pode ser anterior à 'data de criação'.");
        }

        if (_product.UpdatedAt > DateTime.UtcNow.AddDays(1))
        {
            AddError("'data de atualização' não pode estar no futuro.");
        }
    }

    private void ValidateDerivedFlags()
    {
        if (_product.OldPrice is not null && _product.OldPrice.Amount <= _product.Price.Amount)
        {
            AddError("'preço antigo' deve ser maior que o preço atual para o produto estar em promoção.");
        }

        if (_product.CreatedAt < DateTime.UtcNow.AddDays(-30))
        {
            AddError("Produto com mais de 30 dias não pode ser considerado 'novo'.");
        }
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
// /// <summary>
// /// Validador da entidade Product.
// /// </summary>
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
//         if (images is null || !images.Any())
//         {
//             AddError("O produto deve ter pelo menos uma imagem.");
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
// //         ValidateDerivedFlags(); // ← atualizado
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
// //     /// <summary>
// //     /// Valida coerência lógica de propriedades derivadas como IsNew e Sale.
// //     /// </summary>
// //     private void ValidateDerivedFlags()
// //     {
// //         // 🟠 Sale: só faz sentido se oldPrice > price
// //         if (_product.OldPrice is not null && _product.OldPrice.Amount <= _product.Price.Amount)
// //         {
// //             AddError("'preço antigo' deve ser maior que o preço atual para o produto estar em promoção.");
// //         }
// //
// //         // 🟠 IsNew: apenas se o produto tem até 30 dias
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
// //         ValidateActivationFlags();
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
// //         if (_product.Stock < 0)
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
// //     private void ValidateActivationFlags()
// //     {
// //         // Sale depende de OldPrice < Price
// //         if (_product.OldPrice != null && _product.OldPrice.Amount <= _product.Price.Amount)
// //         {
// //             AddError("'preço antigo' deve ser maior que o preço atual para caracterizar uma promoção.");
// //         }
// //
// //         // IsNew depende da data de criação
// //         if (_product.CreatedAt < DateTime.UtcNow.AddDays(-30))
// //         {
// //             AddError("Produto com mais de 30 dias não deve ser considerado 'novo'.");
// //         }
// //     }
// //
// //     private void AddError(string message)
// //     {
// //         ValidationHandler.Append(new Error(message));
// //     }
// // }
// //
// //
// // // using System.Text.RegularExpressions;
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
// // //         ValidateImages();
// // //         ValidateCategory();
// // //         ValidateColors();
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
// // //         if (price.Amount < 0)
// // //         {
// // //             AddError("'preço' não pode ser negativo.");
// // //         }
// // //
// // //         if (price.Amount == 0)
// // //         {
// // //             AddError("'preço' deve ser maior que zero.");
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
// // //     private void AddError(string message)
// // //     {
// // //         ValidationHandler.Append(new Error(message));
// // //     }
// // // }