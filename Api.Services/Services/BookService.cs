using Api.Data.Context;
using Api.Data.Repository;
using Api.Domain.DataTransfer.Answer.BookAnswers;
using Api.Domain.DataTransfer.Payload.BookPayloads;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Representations;
using Api.Domain.Interfaces.Services;
using Api.Domain.Requests.Pagination;
using AutoMapper;

namespace Api.Services.Services
{
    public class BookService : IBookService
    {
        private readonly BookRepository _repository;
        private readonly BaseRepository<Author> _authorRepository;
        private readonly BaseRepository<Gender> _genderRepository;
        private readonly IMapper _mapper;

        public BookService (IMapper mapper)
        {
            _mapper = mapper;
            _repository = new BookRepository(new AppDbContext());
            _authorRepository = new AuthorRepository(new AppDbContext());
            _genderRepository = new BaseRepository<Gender> (new AppDbContext());
        }

        async public Task<bool> addAuthorAsync(Guid id, Guid authorId)
        {
            var book = await _repository.FetchAsync(id);

            if (book == null)
                return false;
     
            var author = await _authorRepository.FetchAsync(authorId);

            if (author == null)
                return false;

            book.AddAuthor(author);
            await _repository.UpdateAsync(book.Id, book);

            return true;
        }

        async public Task<bool> addGenderAsync(Guid id, Guid genderId)
        {
            var book = await _repository.FetchAsync(id);

            if (book == null)
                return false;

            var gender = await _genderRepository.FetchAsync(genderId);

            if (gender == null)
                return false;

            book.AddGender(gender);
            await _repository.UpdateAsync(book.Id, book);

            return true;
        }

        async public Task<IDataTransfer<Book>?> createAsync(BookCreatePayload payload)
        {
            try
            {
                var book = new Book();
                _mapper.Map(payload, book);

                var savedBook = await _repository.CreateAsync(book);

                if (savedBook == null)
                    return null;

                return BookAnswer.BuildFromEntity(savedBook);
            }
            catch
            {
                return null;
            }
        }

        public Task<bool> deleteBookByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<PaginatedResponse<IDataTransfer<Book>, Book>> fetchPaginatedAsync(
                PaginationParams pagination,
                string? title,
                string? authorName,
                string? ISBN)
        {
            ICollection<IDataTransfer<Book>> paginatedData;

            try
            {
                var fetchedBooks = await _repository.FetchPaginatedByCriteria(
                    pagination,
                    title,
                    authorName,
                    ISBN);

                paginatedData = fetchedBooks.Select(b =>
                        BookAnswer.BuildFromEntity(b))
                    .ToList();
            }
            catch
            {
                paginatedData = [];
            }

            var totalCount = await _repository.CountWithCriteria(
                title,
                authorName,
                ISBN);

            return new PaginatedResponse<IDataTransfer<Book>, Book>(
                paginatedData,
                totalCount,
                pagination.PageNumber,
                pagination.PageSize);
        }

        async public Task<IDataTransfer<Book>> getBookByIdAsync(Guid id)
        {
            try
            {
                var book = await _repository.FetchByIdRelationsAsync(id);

                if (book == null)
                    throw new Exception("Book not found.");

                return BookAnswerDetailed.BuildFromEntity(book);
            }
            catch
            {
                throw;
            }
        }

        public Task<bool> removeGenderAsync(Guid id, Guid genderId)
        {
            throw new NotImplementedException();
        }

        public Task<IDataTransfer<Book>?> updateBookAsync(Guid id, BookCreatePayload payload)
        {
            throw new NotImplementedException();
        }
    }
}
