using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookStoreAPI.Domain.Entities
{
    public class Author
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;
        public int YearOfBirth { get; set; }

        public ICollection<BookAuthor> BookAuthors { get; set; } = new List<BookAuthor>();
    }
}
