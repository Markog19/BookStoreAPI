using BookStoreAPI.Domain.Entities;

namespace BookStoreAPI.Domain.Interfaces
{
    public interface IUserService
    {
        public Task Login(User user);
        public Task Register (User user);
    }
}
