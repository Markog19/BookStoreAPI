using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookStoreAPI.Domain.Entities
{
    public class Author
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Name { get; set; }
        public int YearOfBirth { get; set; }

        public ICollection<BookAuthor> BookAuthors { get; set; } = new List<BookAuthor>();
    }
}
