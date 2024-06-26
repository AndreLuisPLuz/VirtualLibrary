using Api.Domain.DataTransfer.Payload;
using Api.Domain.Entities;

namespace Api.Domain.Interfaces.Services
{
    public interface IUserService
    {
        Task<IDataTransfer<User>?> CreateAsync(User user);
        Task<IDataTransfer<User>?> UpdateAsync(Guid id, UserPayload payload);
        Task<IDataTransfer<User>?> FetchAsync(Guid id);
        Task<ICollection<IDataTransfer<User>>> FetchAllAsync();
    }
}
