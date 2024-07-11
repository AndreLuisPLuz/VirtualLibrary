using Api.Domain.DataTransfer.Payload;
using Api.Domain.Entities;

namespace Api.Domain.Interfaces.Services
{
    public interface IGenderService
    {
        Task<IDataTransfer<Gender>?> CreateAsync(GenderPayload payload);
        Task<ICollection<IDataTransfer<Gender>>> GetAsync();
        Task<IDataTransfer<Gender>> UpdateAsync(Guid Id, GenderPayload payload);
    }
}
