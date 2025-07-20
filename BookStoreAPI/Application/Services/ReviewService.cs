using BookStoreAPI.Domain.Entities;
using BookStoreAPI.Domain.Interfaces;
using BookStoreAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BookStoreAPI.Application.Services
{
    public class ReviewService(DBContext context) : IReviewService
    {
        public async Task<IEnumerable<Review>> GetAllAsync()
        {
            return await context.Reviews.Include(e => e.Book).ToListAsync();
        }

        public async Task<Review?> GetByIdAsync(Guid id)
        {
            var review = await context.Reviews.FindAsync(id);
            return review ?? null;
        }

        public async Task<int> CreateAsync(Review review)
        {
            context.Reviews.Add(review);
            return await context.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(Review review)
        {
            var fetchedReview = await context.Reviews.FindAsync(review.Id);
            if (fetchedReview == null)
            {
                return false;
            }

            fetchedReview.Description = review.Description;
            fetchedReview.Rating = review.Rating;
            fetchedReview.BookId = review.BookId;

            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var review = await context.Reviews.FindAsync(id);
            if (review == null)
            {
                return false;
            }
            context.Reviews.Remove(review);
            await context.SaveChangesAsync();
            return true;
        }
    }
}
