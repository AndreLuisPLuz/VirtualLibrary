using Api.Data.Context;
using Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Repository
{
    public class UserRepository : BaseRepository<User>
    {
        public UserRepository(AppDbContext context) : base(context) { }

        public async Task<User?> FetchOneByEmailAsync(String email)
        {
            try
            {
                return await _dbSet.SingleAsync(u => u.Email.Equals(email));
            }
            catch
            {
                return null;
            }
        }
    }
}
