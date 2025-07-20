using System.Security.Cryptography;
using System.Text;
using BookStoreAPI.Application.Common.Security;
using BookStoreAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookStoreAPI.Infrastructure.Data;
public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using var context = new DBContext(
            serviceProvider.GetRequiredService<DbContextOptions<DBContext>>());

        context.Database.EnsureCreated();
        var readerRole = new Role { Name = "Read" };
        var readerWriterRole = new Role { Name = "ReadWrite" };
        if (!context.Roles.Any())
        {
            context.Roles.AddRange(
                readerRole, readerWriterRole
            );
            context.SaveChanges();
        }
        var admin = new User
        {
            Username = "admin",
            Password = PasswordHelper.HashPassword("adminpassword")
        };

        var reader = new User
        {
            Username = "reader",
            Password = PasswordHelper.HashPassword("readerpassword")
        };

        if (!context.Users.Any())
        {
            context.Users.AddRange(admin, reader);
            context.SaveChanges();
        }

        if (!context.UserRoles.Any())
        {
            context.UserRoles.AddRange(
                new UserRole { UserId = reader.Id, RoleId = readerRole.Id },
                new UserRole { UserId = admin.Id, RoleId = readerRole.Id },
                new UserRole { UserId = admin.Id, RoleId = readerWriterRole.Id }
            );
            context.SaveChanges();
        }
    }

    
}
