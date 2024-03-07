using Infrastructure.Factories;
using Infrastructure.Models;
using Infrastructure.Repositories;

namespace Infrastructure.Services;

public class UserService(UserRepository repository, AddressService addressService)
{
    public readonly UserRepository _repository = repository;
    private readonly AddressService _addressService = addressService;


    public async Task<ResponseResult> CreateUserAsync(SignUpModel model)
    {
        try
        {
            var exists = await _repository.ExistsAsync(x => x.Email == model.Email);
            if (exists.StatusCode == StatusCode.EXISTS)
                return exists;

            var result = await _repository.CreateOneAsync(UserFactory.Create(model));
            if (result.StatusCode != StatusCode.OK)
                return result;


            return ResponseFactory.Ok("User was created successfully");
        }
        catch (Exception ex) { return ResponseFactory.Error(ex.Message); }
    }
}
