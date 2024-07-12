using Api.Domain.DataTransfer.Answer.AuthorAnswers;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Representations;

namespace Api.Domain.DataTransfer.Answer.BookAnswers
{
    public record BookAnswerDetailed(
        Guid Id,
        string Title,
        string ISBN,
        string Description,
        ICollection<IDataTransfer<Gender>> Genders,
        ICollection<IDataTransfer<Author>> Authors,
        int? PagesCount,
        BookLendingOption LendingOption,
        int? AvailableQuantity) : IDataTransfer<Book>
    {
        static public IDataTransfer<Book> BuildFromEntity(Book book)
        {
            var genders = book.Genders.Select(
                    g => GenderAnswer.BuildFromEntity(g))
                .ToList();

            var authors = book.Authors.Select(
                    a => AuthorAnswer.BuildFromEntity(a))
                .ToList();

            return new BookAnswerDetailed(
                book.Id,
                book.Title,
                book.ISBN,
                book.Description,
                genders,
                authors,
                book.PagesCount,
                book.LendingOption,
                book.AvailableQuantity);
        }

        IDataTransfer<Book> IDataTransfer<Book>.BuildFromEntity(Book book)
        {
            return BuildFromEntity(book);
        }
    }
}
