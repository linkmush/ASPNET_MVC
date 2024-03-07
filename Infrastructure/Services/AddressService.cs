using Infrastructure.Entities;
using Infrastructure.Factories;
using Infrastructure.Models;
using Infrastructure.Repositories;

namespace Infrastructure.Services;

public class AddressService(AddressRepository repository)
{
    private readonly AddressRepository _repository = repository;


    public async Task<ResponseResult> GetOrCreateAddressAsync(string streetName, string postalCode, string city)
    {
        try
        {
            var result = await GetAddressAsync(streetName, postalCode, city);
            if (result.StatusCode == StatusCode.NOT_FOUND)
                result = await CreateAddressAsync(streetName, postalCode, city);

            return result;
        }
        catch (Exception ex) { return ResponseFactory.Error(ex.Message); }
    }

    public async Task<ResponseResult> CreateAddressAsync(string streetName, string postalCode, string city)
    {
        try
        {
            var exists = await _repository.ExistsAsync(x => x.StreetName == streetName && x.PostalCode == postalCode && x.City == city);
            if (exists == null)
            {
                var result = await _repository.CreateOneAsync(AddressFactory.Create(streetName, postalCode, city));
                if (result.StatusCode == StatusCode.OK)
                    return ResponseFactory.Ok(AddressFactory.Create((AddressEntity)result.ContentResult!));

                return result;
            }

            return exists;
        }
        catch (Exception ex) { return ResponseFactory.Error(ex.Message); }
    }

    public async Task<ResponseResult> GetAddressAsync(string streetName, string postalCode, string city)
    {
        try
        {
            var result = await _repository.GetOneAsync(x => x.StreetName == streetName && x.PostalCode == postalCode && x.City == city);
            return result;
        }
        catch (Exception ex) { return ResponseFactory.Error(ex.Message); }
    }
}
