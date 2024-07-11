using Api.Domain.DataTransfer.Payload;

namespace Api.Domain.Interfaces.Services
{
    public interface ILoginService
    {
        Task<Boolean> TryLogin(LoginPayload payload);
    }
}
