using Api.Domain.DataTransfer.Answer.AuthorAnswers;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Representations;

namespace Api.Domain.DataTransfer.Answer.BookAnswers
{
    public record BookAnswer(
        Guid Id,
        string Title,
        string ISBN) : IDataTransfer<Book>
    {
        static public IDataTransfer<Book> BuildFromEntity(Book book)
        {
            return new BookAnswer(book.Id, book.Title, book.ISBN);
        }

        IDataTransfer<Book> IDataTransfer<Book>.BuildFromEntity(Book book)
        {
            return BuildFromEntity(book);
        }
    }
}
