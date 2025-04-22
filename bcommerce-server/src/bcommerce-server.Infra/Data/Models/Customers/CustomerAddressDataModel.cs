namespace bcommerce_server.Infra.Data.Models.Customers;

public class CustomerAddressDataModel
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public string Street { get; set; } = null!;
    public string Number { get; set; } = null!;
    public string City { get; set; } = null!;
    public string State { get; set; } = null!;
    public string ZipCode { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}