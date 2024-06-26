using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Entities
{
    public class Lending : BaseEntity
    {
        [Required]
        public DateTime StartingAt { get; set; }

        [Required]
        public DateTime EndingAt { get; set; }

        public DateTime? DevolutionAt { get; set; }

        public Double? FineApplied { get; set; }

        public DateTime? FinePaidAt { get; set; }

        public User User { get; set; }
        public Book Book { get; set; }

        public Lending() : base()
        {
            StartingAt = DateTime.UtcNow;
        }

        public Lending(User user, Book book, DateTime endingAt) : base()
        {
            User = user;
            Book = book;
            StartingAt = DateTime.UtcNow;
            EndingAt = endingAt;
        }
    }
}
