using BookStoreAPI.Domain.Entities;
using BookStoreAPI.Domain.Interfaces;
using BookStoreAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BookStoreAPI.Application.Services
{
    public class AuthorService(DBContext context) : IAuthorService
    {
        public async Task<int> CreateAsync(Author author)
        {
            context.Authors.Add(author);
            return await context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var author = await context.Authors.FindAsync(id);
            if (author == null)
            {
                return false;
            }
            context.Authors.Remove(author);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Author>> GetAllAsync()
        {
            return await context.Authors.Include(e => e.BookAuthors).ToListAsync();
        }

        public async Task<Author?> GetByIdAsync(Guid id)
        {
            var author = await context.Authors.FindAsync(id);
            return author == null ? null : author;
        }

        public async Task<bool> UpdateAsync(Author author)
        {
            var fetchedAuthor = await context.Authors.FindAsync(author.Id);
            if (fetchedAuthor == null)
            {
                return false;
            }

            fetchedAuthor.YearOfBirth = author.YearOfBirth;
            fetchedAuthor.Name = author.Name;
            fetchedAuthor.Id = author.Id;

            await context.SaveChangesAsync();
            return true;
        }
    }
}
