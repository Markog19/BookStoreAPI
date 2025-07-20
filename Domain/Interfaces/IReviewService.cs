using BookStoreAPI.Domain.Entities;

namespace BookStoreAPI.Domain.Interfaces
{
    public interface IReviewService
    {
        Task<IEnumerable<Review>> GetAllAsync();
        Task<Review?> GetByIdAsync(Guid id);
        Task<int> CreateAsync(Review review);
        Task<bool> UpdateAsync(Review review);
        Task<bool> DeleteAsync(Guid id);
    }
}
