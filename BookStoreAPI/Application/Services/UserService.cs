using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BookStoreAPI.Application.Common.Security;
using BookStoreAPI.Application.DTOs;
using BookStoreAPI.Domain.Entities;
using BookStoreAPI.Domain.Interfaces;
using BookStoreAPI.Infrastructure.Data;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace BookStoreAPI.Application.Services
{
    public class UserService(DBContext context, IOptions<AuthOptions> authOptions) : IUserService
    {
        public async Task<string> Authenticate(User user)
        {
            var fetchedUser = await context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .SingleOrDefaultAsync(u => u.Username == user.Username);

            if (fetchedUser == null || !PasswordHelper.Verify(fetchedUser.Password, user.Password))
            {
                return null;
            }
            var userRoles = await context.UserRoles
                .Where(ur => ur.UserId == fetchedUser.Id)
                .Include(ur => ur.Role)
                .Select(ur => ur.Role)
                .ToListAsync();

            return GenerateJwtToken(user.Username, userRoles);
        }

        public async Task<bool> Register(User user)
        {
            if(context.Users.Any(e => e.Username == user.Username))
            {
                return false;
            }
            user.Password = PasswordHelper.HashPassword(user.Password);
            var readRole = await context.Roles.FirstOrDefaultAsync(e => e.Name == "Read");
            var userRole = new UserRole()
            {
                UserId = user.Id,
                RoleId = readRole.Id
            };
            user.UserRoles.Add(userRole);
            context.Add(user);
            await context.SaveChangesAsync();
            return true;
        }
        public string GenerateJwtToken(string username, List<Role> roles)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, username),       
            };
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.Name));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authOptions.Value.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: authOptions.Value.Issuer,
                audience: authOptions.Value.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
