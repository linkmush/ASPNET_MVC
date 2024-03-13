namespace Infrastructure.Entities;

public class AddressEntity
{
    public int Id { get; set; }
    public string AddressLine_1 { get; set; } = null!;   // ska byta ut streetname mot AddressLine_1 och lägga till en AddressLine_2
    public string? AddressLine_2 { get; set; }
    public string PostalCode { get; set; } = null!;
    public string City { get; set; } = null!;

    public ICollection<UserEntity> Users { get; set; } = [];
}