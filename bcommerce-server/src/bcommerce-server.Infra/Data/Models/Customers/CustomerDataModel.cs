namespace bcommerce_server.Infra.Data.Models.Customers;

public class CustomerDataModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string? Cpf { get; set; }
    public DateTime? DeletedAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public List<CustomerAddressDataModel>? Addresses { get; set; } // opcional
}