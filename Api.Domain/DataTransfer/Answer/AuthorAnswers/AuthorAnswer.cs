using Api.Domain.Entities;
using Api.Domain.Interfaces.Representations;

namespace Api.Domain.DataTransfer.Answer.AuthorAnswers
{
    public record AuthorAnswer(
        Guid Id,
        string Name,
        PersonGender Gender) : IDataTransfer<Author>
    {
        static public IDataTransfer<Author> BuildFromEntity(Author author)
        {
            return new AuthorAnswer(author.Id, author.Name, author.Gender);
        }

        IDataTransfer<Author> IDataTransfer<Author>.BuildFromEntity(Author author)
        {
            return BuildFromEntity(author);
        }
    }
}
