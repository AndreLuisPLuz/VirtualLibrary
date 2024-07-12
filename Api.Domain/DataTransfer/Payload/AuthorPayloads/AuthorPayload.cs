using Api.Domain.Entities;

namespace Api.Domain.DataTransfer.Payload.Author
{
    public record AuthorPayload(
        string Name,
        PersonGender Gender)
    { }
}
