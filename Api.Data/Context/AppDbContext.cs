using Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Lending> Lendings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost,1433;Database=VirtualLibrary;User ID=VirtualLibraryApi;Password=Hobbiton;Trusted_Connection=False; TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var author1 = new Author { Name = "H. P. Lovecraft", Gender = PersonGender.MALE };
            var author2 = new Author { Name = "J. K. Rowling", Gender = PersonGender.FEMALE };

            modelBuilder.Entity<Author>().HasData(author1, author2);

            var gender1 = new Gender { Name = "Adventure" };
            var gender2 = new Gender { Name = "Fantasy" };
            var gender3 = new Gender { Name = "Horror" };
            var gender4 = new Gender { Name = "Thriller" };

            modelBuilder.Entity<Gender>().HasData(
                gender1,
                gender2,
                gender3,
                gender4);

            var book1 = new Book
            {
                Title = "Harry Potter & the Philosopher's Stone",
                Description = "A boy learns how to do magic.",
                ISBN = "9781781100349",
                PagesCount = 278
            };
            var book2 = new Book
            {
                Title = "The Call of Cthulhu",
                Description = "Dreams point to the return of an old horror.",
                ISBN = "9789583067341",
                PagesCount = 179
            };

            modelBuilder.Entity<Book>().HasData(book1, book2);

            modelBuilder.Entity<Book>()
                .HasMany(b => b.Authors)
                .WithMany(a => a.Books)
                .UsingEntity<Dictionary<string, object>>(
                    "AuthorBook",
                    d => d.HasOne<Author>().WithMany().HasForeignKey("AuthorId"),
                    d => d.HasOne<Book>().WithMany().HasForeignKey("BookId"),
                    d =>
                    {
                        d.HasKey("AuthorId", "BookId");
                        d.HasData(
                            new { AuthorId = author1.Id, BookId = book2.Id },
                            new { AuthorId = author2.Id, BookId = book1.Id });
                    });

            modelBuilder.Entity<Book>()
                .HasMany(b => b.Genders)
                .WithMany(g => g.Books)
                .UsingEntity<Dictionary<string, object>>(
                    "BookGender",
                    d => d.HasOne<Gender>().WithMany().HasForeignKey("GenderId"),
                    d => d.HasOne<Book>().WithMany().HasForeignKey("BookId"),
                    d =>
                    {
                        d.HasKey("GenderId", "BookId");
                        d.HasData(
                            new { BookId = book1.Id, GenderId = gender1.Id },
                            new { BookId = book1.Id, GenderId = gender2.Id },
                            new { BookId = book2.Id, GenderId = gender3.Id },
                            new { BookId = book2.Id, GenderId = gender4.Id });
                    });

            modelBuilder.Entity<User>().HasData(
                new User("André", "andreluispluz@gmail.com", PersonGender.MALE, "Andre123"),
                new User("Mateus", "mateus@gmail.com", PersonGender.MALE, "Mateus123"));
        }
    }
}
