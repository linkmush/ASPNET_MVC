using Infrastructure.Contexts;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class AddressManager(DataContext context)
{
    public readonly DataContext _context = context;

    public async Task<AddressEntity?> GetAddressAsync(string UserId)
    {
        var user = await _context.Users.Include(u => u.Address).FirstOrDefaultAsync(u => u.Id == UserId);
        return user?.Address;
    }

    public async Task<bool> CreateAddressAsync(AddressEntity entity)
    {
        _context.Addresses.Add(entity);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UpdateAddressAsync(AddressEntity entity)
    {

        var existing = await _context.Addresses.FirstOrDefaultAsync(u => u.Id == entity.Id);
        if (existing != null)
        {
            _context.Entry(entity).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
            return true;
        }
        
        return false;
    }
}
