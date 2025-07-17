using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookStoreAPI.Domain.Entities
{
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string Title { get; set; } = null!;
        public decimal Price { get; set; }

        public ICollection<BookAuthor> BookAuthors { get; set; } = new List<BookAuthor>();
        public ICollection<BookGenre> BookGenres { get; set; } = new List<BookGenre>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}
