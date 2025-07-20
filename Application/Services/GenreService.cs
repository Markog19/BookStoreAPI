using BookStoreAPI.Domain.Entities;
using BookStoreAPI.Domain.Interfaces;
using BookStoreAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BookStoreAPI.Application.Services
{
    public class GenreService(DBContext context) : IGenreService
    {
        public async Task<IEnumerable<Genre>> GetAllAsync()
        {
            return await context.Genres.Include(e => e.BookGenres).ToListAsync();
        }

        public async Task<Genre?> GetByIdAsync(Guid id)
        {
            var genre = await context.Genres.FindAsync(id);
            return genre ?? null;
        }

        public async Task<int> CreateAsync(Genre genre)
        {
            context.Genres.Add(genre);
            return await context.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(Genre genre)
        {
            var fetchedGenre = await context.Genres.FindAsync(genre.Id);
            if (genre == null)
            {
                return false;
            }
            fetchedGenre.Name = genre.Name;

            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var genre = await context.Genres.FindAsync(id);
            if (genre == null)
            {
                return false;
            }
            context.Genres.Remove(genre);
            await context.SaveChangesAsync();
            return true;
        }
    }
}
