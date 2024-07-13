using Api.Data.Context;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Representations;
using Api.Domain.Requests.Pagination;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Api.Data.Repository
{
    public class BookRepository : BaseRepository<Book>
    {
        public BookRepository(AppDbContext context) : base(context) { }

        async public Task<ICollection<Book>> FetchPaginatedByCriteria(
            PaginationParams pagination,
            string? title,
            string? authorName,
            string? ISBN)
        {
            try
            {
                var query = _dbSet.AsQueryable();

                if (!title.IsNullOrEmpty())
                    query = query.Where(b => b.Title.Contains(title));

                if (!authorName.IsNullOrEmpty())
                {
                    query = query.Where(b => 
                        b.Authors.Where(a => 
                            !a.Name.Contains(authorName)).IsNullOrEmpty());
                }

                if (!ISBN.IsNullOrEmpty())
                    query = query.Where(b => b.ISBN.Equals(ISBN));

                ICollection<Book> records = await query.Skip(
                        pagination.PageSize
                        * (pagination.PageNumber - 1))
                    .Take(pagination.PageSize)
                    .ToListAsync();

                return records;
            }
            catch
            {
                return [];
            }
        }

        async public Task<int> CountWithCriteria(
            string? title,
            string? authorName,
            string? ISBN)
        {
            var query = _dbSet.AsQueryable();

            if (!title.IsNullOrEmpty())
                query = query.Where(b => b.Title.Contains(title));

            if (!authorName.IsNullOrEmpty())
                query = query.Where(b =>
                    b.Authors.Where(a =>
                        !a.Name.Contains(authorName)).IsNullOrEmpty());

            if (!ISBN.IsNullOrEmpty())
                query = query.Where(b => b.ISBN.Equals(ISBN));

            return await query.CountAsync();
        }

        async public Task<Book?> FetchByIdRelationsAsync(Guid id)
        {
            var book = await _dbSet
                .Include(b => b.Authors)
                .Include(b => b.Genders)
                .SingleOrDefaultAsync(b => b.Id.Equals(id));

            return book;
        }
    }
}
