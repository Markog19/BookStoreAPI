using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookStoreAPI.Domain.Entities
{
    public class Review
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; }

        public string? Description { get; set; } = null!;

        public Guid BookId { get; set; }
        public Book Book { get; set; }
    }
}
