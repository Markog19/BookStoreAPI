using BookStoreAPI.Domain.Entities;

namespace BookStoreAPI.Domain.Interfaces
{
    public interface IGenreService
    {
        Task<IEnumerable<Genre>> GetAllAsync();
        Task<Genre?> GetByIdAsync(Guid id);
        Task<int> CreateAsync(Genre genre);
        Task<bool> UpdateAsync(Genre genre);
        Task<bool> DeleteAsync(Guid id);
    }
}
