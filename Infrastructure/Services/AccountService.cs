using Infrastructure.Contexts;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class AccountService
{
    private readonly DataContext _context;
    private readonly UserManager<UserEntity> _userManager;

    public AccountService(DataContext context, UserManager<UserEntity> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    //public async Task<bool> UpdateUserAsync(UserEntity user)
    //{
    //    await _context.Users.FirstOrDefaultAsync(u => u.Id == user.Id);

    //    await _userManager.Users.FirstOrDefaultAsync(user => user.Email == user.Email);

    //    //await _context.SaveChangesAsync();
    //    //return true;
    //}
}
