using System.Threading.Tasks;
using BookStoreAPI.Domain.Entities;

namespace BookStoreAPI.Domain.Interfaces
{
    public interface IAuthorService
    {
        Task<IEnumerable<Author>> GetAllAsync();
        Task<Author?> GetByIdAsync(Guid id);
        Task<int> CreateAsync(Author author);
        Task<bool> UpdateAsync(Author author);
        Task<bool> DeleteAsync(Guid id);
    }
}
