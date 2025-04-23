using System.Text.RegularExpressions;
using bcommerce_server.Domain.SeedWork;

namespace bcommerce_server.Domain.Products.ValueObjects;

/// <summary>
/// Representa uma URL de imagem de produto válida.
/// </summary>
public sealed class ImageUrl : ValueObject
{
    public string Url { get; }

    private ImageUrl(string url)
    {
        if (string.IsNullOrWhiteSpace(url) || !Regex.IsMatch(url, @"^https?:\/\/.+\.(jpg|jpeg|png|webp|svg)$", RegexOptions.IgnoreCase))
            throw new ArgumentException("URL de imagem inválida.");

        Url = url;
    }

    public static ImageUrl From(string url) => new(url);

    public override string ToString() => Url;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Url;
    }
}