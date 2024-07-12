using Api.Data.Context;
using Api.Domain.Entities;
using Api.Domain.Interfaces;
using Api.Domain.Requests.Pagination;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections;

namespace Api.Data.Repository
{
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly AppDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public BaseRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            try
            {
                return await _dbSet.AnyAsync(e => e.Id.Equals(id));
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> ExistsAsync(T entity)
        {
            return await ExistsAsync(entity.Id);
        }

        public async Task<T?> CreateAsync(T entity)
        {
            try
            {
                EntityEntry<T> newEntry = _dbSet.Add(entity);
                await _context.SaveChangesAsync();

                return newEntry.Entity;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<T?> UpdateAsync(Guid id, T entity)
        {
            try
            {
                bool exists = await _dbSet.AnyAsync(e => e.Id.Equals(id));
                if (!exists)
                    throw new Exception($"{entity.GetType().Name} not found on the database, cannot be updated.");

                entity.UpdateTimeStamp();

                T currentEntity = await _dbSet.SingleAsync(e => e.Id.Equals(id));
                _context.Entry(currentEntity).CurrentValues.SetValues(entity);

                await _context.SaveChangesAsync();
                return currentEntity;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                bool exists = await _dbSet.AnyAsync(e => e.Id.Equals(id));
                if (!exists)
                    throw new Exception($"Entity not found on the database, cannot be deleted.");

                T entityAsIs = await _dbSet.SingleAsync(e => e.Id.Equals(id));

                _dbSet.Remove(entityAsIs);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            return await DeleteAsync(entity.Id);
        }

        public async Task<T?> FetchAsync(Guid id)
        {
            try
            {
                return await _dbSet.SingleOrDefaultAsync(e => e.Id.Equals(id));
            } catch (Exception)
            {
                return null;
            }
        }

        public async Task<ICollection<T>> FetchAllAsync()
        {
            try
            {
                return await _dbSet.ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ICollection<T>?> FetchPaginatedAsync(
            PaginationParams pagination)
        {
            try
            {
                ICollection<T> records = await _dbSet.Skip(
                        pagination.PageSize
                        * pagination.PageNumber - 1)
                    .Take(pagination.PageSize)
                    .ToListAsync();

                return records;
            }
            catch
            {
                return new List<T>();
            }
        }

        public async Task<int> Count()
        {
            return await _dbSet.CountAsync();
        }
    }
}
