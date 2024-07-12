using Api.Data.Context;
using Api.Data.Repository;
using Api.Domain.DataTransfer.Answer;
using Api.Domain.DataTransfer.Payload.Author;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Representations;
using Api.Domain.Interfaces.Services;
using Api.Domain.Requests.Pagination;
using AutoMapper;

namespace Api.Services.Services
{
    public class AuthorService : IAuthorService
    {
        private AuthorRepository _repository;
        private IMapper _mapper;

        public AuthorService(IMapper mapper)
        {
            _repository = new AuthorRepository(new AppDbContext());
            _mapper = mapper;
        }

        public async Task<IDataTransfer<Author>?> create(AuthorPayload payload)
        {
            try
            {
                Author? author = new();
                _mapper.Map(payload, author);

                Author? newAuthor = await _repository.CreateAsync(author);
                if (newAuthor is not null)
                {
                    return AuthorAnswer.BuildFromEntity(newAuthor);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<IDataTransfer<Author>?> fetch(Guid id)
        {
            try
            {
                Author? fetchedAuthor = await _repository.FetchAsync(id);
                if (fetchedAuthor is not null)
                {
                    return AuthorAnswer.BuildFromEntity(fetchedAuthor);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<PaginatedResponse<IDataTransfer<Author>, Author>> getPaginated(
            PaginationParams pagination,
            string? name)
        {
            ICollection<IDataTransfer<Author>> paginatedData;

            try
            {
                var fetchedAuthors = await _repository.FetchPaginatedByName(
                    pagination,
                    name);

                paginatedData = fetchedAuthors.Select(a =>
                        AuthorAnswer.BuildFromEntity(a))
                    .ToList();
            }
            catch
            {
                paginatedData = [];
            }

            var totalCount = await _repository.CountWithName(name);

            return new PaginatedResponse<IDataTransfer<Author>, Author>(
                paginatedData,
                totalCount,
                pagination.PageNumber,
                pagination.PageSize);
        }

        public async Task<IDataTransfer<Author>?> update(
            Guid id,
            AuthorPayload payload)
        {
            Author? currentAuthor = await _repository.FetchAsync(id);
            if (currentAuthor is null)
            {
                return null;
            }

            _mapper.Map(payload, currentAuthor);

            Author? updatedAuthor = await _repository.UpdateAsync(id, currentAuthor);
            if (updatedAuthor is not null)
            {
                return AuthorAnswer.BuildFromEntity(updatedAuthor);
            }
            else
            {
                return null;
            }
        }
    }
}
