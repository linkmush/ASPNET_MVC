using Infrastructure.Entities;
using Infrastructure.Models;

namespace Infrastructure.Factories;

public class AddressFactory
{

    public static AddressEntity Create()
    {
        try
        {
            return new AddressEntity();
        }
        catch { }
        return null!;
    }

    public static AddressEntity Create(string streetName, string postalCode, string city)
    {
        try
        {
            return new AddressEntity
            {
                StreetName = streetName,
                PostalCode = postalCode,
                City = city
            };
        }
        catch { }
        return null!;
    }

    public static AddressModel Create(AddressEntity entity)
    {
        try
        {
            return new AddressModel
            {
                Id = entity.Id,
                StreetName = entity.StreetName,
                PostalCode = entity.PostalCode,
                City = entity.City
            };
        }
        catch { }
        return null!;
    }
}
