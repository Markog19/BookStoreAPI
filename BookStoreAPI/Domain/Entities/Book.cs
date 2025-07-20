using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookStoreAPI.Domain.Entities
{
    public class Book
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public string? Title { get; set; }
        public decimal Price { get; set; }

        public ICollection<BookAuthor> BookAuthors { get; set; } = new List<BookAuthor>();
        public ICollection<BookGenre> BookGenres { get; set; } = new List<BookGenre>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}
