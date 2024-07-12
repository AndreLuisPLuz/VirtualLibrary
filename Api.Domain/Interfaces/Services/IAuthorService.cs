using Api.Domain.DataTransfer.Payload.Author;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Representations;
using Api.Domain.Requests.Pagination;

namespace Api.Domain.Interfaces.Services
{
    public interface IAuthorService
    {
        Task<IDataTransfer<Author>?> create(AuthorPayload payload);
        Task<IDataTransfer<Author>?> update(Guid id, AuthorPayload payload);
        Task<IDataTransfer<Author>?> fetch(Guid id);
        Task<PaginatedResponse<IDataTransfer<Author>, Author>> getPaginated(
            PaginationParams pagination,
            string? name);
    }
}
