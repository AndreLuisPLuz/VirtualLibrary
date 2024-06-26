using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Entities
{
    public class Author : BaseEntity
    {
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required]
        public PersonGender Gender { get; set; }

        public ICollection<Book> Books { get; } = new List<Book>();

        public Author() : base() { }

        public Author(string name, PersonGender gender) : base()
        {
            Name = name;
            Gender = gender;
        }
    }
}
