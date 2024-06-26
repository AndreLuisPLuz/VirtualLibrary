using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Entities
{
    public class Gender : BaseEntity
    {
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        public ICollection<Book> Books { get; } = new List<Book>();

        public Gender() : base() { }

        public Gender(string name) : base()
        {
            Name = name;
        }
    }
}
