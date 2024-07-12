using Api.Domain.DataTransfer.Answer.BookAnswers;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Representations;

namespace Api.Domain.DataTransfer.Answer.AuthorAnswers
{
    public record AuthorSingleAnswer(
        Guid id,
        string Name, 
        PersonGender Gender,
        ICollection<IDataTransfer<Book>> Books) : IDataTransfer<Author>
    {
        static public IDataTransfer<Author> BuildFromEntity(Author author)
        {
            var books = author.Books.Select(b => 
                    BookAnswer.BuildFromEntity(b))
                .ToList();

            return new AuthorSingleAnswer(
                author.Id,
                author.Name,
                author.Gender,
                books);
        }

        IDataTransfer<Author> IDataTransfer<Author>.BuildFromEntity(Author author)
        {
            return BuildFromEntity(author);
        }
    }
}
