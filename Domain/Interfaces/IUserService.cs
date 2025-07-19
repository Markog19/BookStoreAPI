using BookStoreAPI.Domain.Entities;

namespace BookStoreAPI.Domain.Interfaces
{
    public interface IUserService
    {
        public Task<string> Authenticate(User user);
        public Task<bool> Register (User user);
        public string GenerateJwtToken(string username, List<Role> role);

    }
}
