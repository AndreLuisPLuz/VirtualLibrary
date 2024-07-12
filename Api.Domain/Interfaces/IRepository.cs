﻿using Api.Domain.Entities;
using Api.Domain.Requests.Pagination;

namespace Api.Domain.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<bool> ExistsAsync(T entity);
        Task<bool> ExistsAsync(Guid id);
        Task<T?> CreateAsync(T entity);
        Task<T?> UpdateAsync(Guid id, T entity);
        Task<bool> DeleteAsync(T entity);
        Task<bool> DeleteAsync(Guid id);
        Task<T?> FetchAsync(Guid id);
        Task<ICollection<T>> FetchAllAsync();
        Task<ICollection<T>> FetchPaginatedAsync(PaginationParams pagination);
        Task<int> Count();
    }
}
