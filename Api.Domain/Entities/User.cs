using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Entities
{
    public enum PersonGender
    {
        MALE = 0,
        FEMALE = 1
    }

    public class User : BaseEntity
    {
        private static readonly PasswordHasher<User> passwordHasher = new();
        private string _password;

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(255)]
        public string Email { get; set; }

        [Required]
        [MaxLength(255)]
        public string Password
        {
            get { return _password; }
            set
            {
                _password = passwordHasher.HashPassword(this, value);
            }
        }

        public PersonGender Gender { get; set; }
        public ICollection<Lending> Lendings { get; set; } = new List<Lending>();
        public User() : base() { }

        public User(
                string name,
                string email,
                PersonGender gender,
                string plainPassword) : base()
        {
            Name = name;
            Email = email;
            Gender = gender;
            Password = passwordHasher.HashPassword(this, plainPassword);
        }
    }
}
