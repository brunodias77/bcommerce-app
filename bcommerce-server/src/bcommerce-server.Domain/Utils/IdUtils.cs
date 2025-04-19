namespace bcommerce_server.Domain.Utils;

/// <summary>
/// Utilitário para geração de IDs únicos.
/// </summary>
public static class IdUtils
{
    /// <summary>
    /// Gera um UUID (GUID) em formato string, minúsculo e sem hífens.
    /// </summary>
    public static string Uuid()
    {
        return Guid.NewGuid().ToString("N").ToLowerInvariant();
        // "N" = formato sem hífens
    }
}