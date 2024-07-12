using Api.Data.Context;
using Api.Domain.Entities;
using Api.Domain.Requests.Pagination;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Api.Data.Repository
{
    public class AuthorRepository : BaseRepository<Author>
    {
        public AuthorRepository(AppDbContext context) : base(context) { }

        public async Task<ICollection<Author>> FetchPaginatedByName(
            PaginationParams pagination,
            string? name = null)
        {
            try
            {
                var query = _dbSet.AsQueryable();

                if (!name.IsNullOrEmpty())
                    query = query.Where(a => a.Name.Contains(name));

                ICollection<Author> records = await query.Skip(
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

        public async Task<int> CountWithName(string? name = null)
        {
            var query = _dbSet.AsQueryable();

            if (!name.IsNullOrEmpty())
                query = query.Where(u => u.Name.Contains(name));

            return await query.CountAsync();
        }
    }
}
