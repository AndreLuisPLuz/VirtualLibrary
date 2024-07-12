using Api.Domain.DataTransfer.Payload.UserPayloads;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Representations;

namespace Api.Domain.Interfaces.Services
{
    public interface IUserService
    {
        Task<IDataTransfer<User>?> CreateAsync(UserPayload payload);
        Task<IDataTransfer<User>?> UpdateAsync(Guid id, UserPayload payload);
        Task<IDataTransfer<User>?> FetchAsync(Guid id);
        Task<ICollection<IDataTransfer<User>>> FetchAllAsync();
        Task<User?> FetchByUsernameAsync(String email);
    }
}
