using Api.Domain.DataTransfer.Answer.BookAnswers;
using Api.Domain.DataTransfer.Payload.BookPayloads;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Representations;
using Api.Domain.Requests.Pagination;

namespace Api.Domain.Interfaces.Services
{
    public interface IBookService
    {
        Task<IDataTransfer<Book>?> createAsync(BookCreatePayload payload);
        Task<Boolean> addAuthorAsync(Guid id, Guid authorId);
        Task<Boolean> addGenderAsync(Guid id, Guid genderId);
        Task<Boolean> removeGenderAsync(Guid id, Guid genderId);
        Task<PaginatedResponse<IDataTransfer<Book>, Book>> fetchPaginatedAsync(
            PaginationParams pagination,
            string? title,
            string? authorName,
            string? ISBN);
        Task<IDataTransfer<Book>> getBookByIdAsync(Guid id);
        Task<IDataTransfer<Book>?> updateBookAsync(Guid id, BookCreatePayload payload);
        Task<Boolean> deleteBookByIdAsync(Guid id);
    }
}
