using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Entities
{
    public enum BookLendingOption
    {
        PHYSICAL = 0,
        VIRTUAL = 1,
    }

    public class Book : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        [MaxLength(1024)]
        public string Description { get; set; }

        [Required]
        [MaxLength(13)]
        public string ISBN { get; set; }

        public int? PagesCount { get; set; }

        [Required]
        public BookLendingOption LendingOption { get; set; }

        public int? AvailableQuantity { get; set; }

        public ICollection<Author> Authors { get; } = new List<Author>();
        public ICollection<Gender> Genders { get; } = new List<Gender>();

        public Book() : base() { }

        public Book(
            string title,
            string description,
            string isbn,
            BookLendingOption lendingOption) : base()
        {
            Title = title;
            Description = description;
            ISBN = isbn;
            LendingOption = lendingOption;
        }

        public void AddAuthor(Author author)
        {
            Authors.Add(author);
        }

        public void AddGender(Gender gender)
        {
            Genders.Add(gender);
        }
    }
}
