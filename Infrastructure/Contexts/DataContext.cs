using Infrastructure.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Infrastructure.Contexts;

public class DataContext(DbContextOptions<DataContext> options) : IdentityDbContext<UserEntity>(options)
{
    public DbSet<AddressEntity> Addresses { get; set; }
    public DbSet<SavedCourseEntity> SavedCourses { get; set; }

    // vill jag ha fler entiteter lägger jag in dom här.


    //protected override void OnModelCreating(ModelBuilder builder)
    //{
    //    base.OnModelCreating(builder);

    //    builder.Entity<UserEntity>()
    //    .HasOne(u => u.Address)                             ---- här är för att göra så att antingen om addressen tas bort deletas inte users eller tvärtom.
    //    .WithOne(a => a.Users)
    //    .HasForeignKey<AddressEntity>(a => a.UserId)
    //    .OnDelete(DeleteBehavior.Restrict);
    //}
}
