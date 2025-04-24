using System.Text.RegularExpressions;
using bcommerce_server.Domain.SeedWork;

namespace bcommerce_server.Domain.Products.ValueObjects;

/// <summary>
/// Representa uma URL de imagem de produto válida.
/// Suporta URLs absolutas (http/https) e caminhos relativos iniciando com '/'.
/// </summary>
public sealed class ImageUrl : ValueObject
{
    public string Url { get; }

    private ImageUrl(string url)
    {
        if (string.IsNullOrWhiteSpace(url))
            throw new ArgumentException("URL de imagem não pode ser vazia.");

        if (!IsValidImageUrl(url))
            throw new ArgumentException("URL de imagem inválida.");

        Url = url;
    }

    public static ImageUrl From(string url) => new(url);

    private static bool IsValidImageUrl(string url)
    {
        // Regex para validar extensão final de imagem
        var imageExtensions = @"\.(jpg|jpeg|png|webp|svg)$";

        // Caminho relativo válido: começa com / e termina com extensão de imagem
        if (url.StartsWith("/") && Regex.IsMatch(url, imageExtensions, RegexOptions.IgnoreCase))
            return true;

        // URL absoluta válida: http ou https e termina com extensão de imagem
        if (Uri.TryCreate(url, UriKind.Absolute, out var uri) &&
            (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps) &&
            Regex.IsMatch(url, imageExtensions, RegexOptions.IgnoreCase))
        {
            return true;
        }

        return false;
    }

    public override string ToString() => Url;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Url.ToLowerInvariant();
    }
}