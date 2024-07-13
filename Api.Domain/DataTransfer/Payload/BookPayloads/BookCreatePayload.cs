using Api.Domain.Entities;
using Api.Domain.Interfaces.Representations;

namespace Api.Domain.DataTransfer.Payload.BookPayloads
{
    public record BookCreatePayload(
        string Title,
        string ISBN,
        string Description,
        int? PagesCount,
        BookLendingOption LendingOption) : IPayload<Book>
    {
        public Book BuildEntity()
        {
            throw new NotImplementedException();
        }
    }
}
