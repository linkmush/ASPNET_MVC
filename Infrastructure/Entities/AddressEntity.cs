using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Entities;

public class AddressEntity
{
    public int Id { get; set; }

    [ProtectedPersonalData]
    public string AddressLine_1 { get; set; } = null!;

    [ProtectedPersonalData]
    public string? AddressLine_2 { get; set; }

    [ProtectedPersonalData]
    public string PostalCode { get; set; } = null!;

    [ProtectedPersonalData]
    public string City { get; set; } = null!;

    public ICollection<UserEntity> Users { get; set; } = [];
}