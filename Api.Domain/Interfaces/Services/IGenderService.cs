using Api.Domain.DataTransfer.Payload.Gender;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Representations;

namespace Api.Domain.Interfaces.Services
{
    public interface IGenderService
    {
        Task<IDataTransfer<Gender>?> CreateAsync(GenderPayload payload);
        Task<ICollection<IDataTransfer<Gender>>> GetAsync();
        Task<IDataTransfer<Gender>> UpdateAsync(Guid Id, GenderPayload payload);
    }
}
