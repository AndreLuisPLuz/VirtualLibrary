using Api.Domain.Entities;
using Api.Domain.Interfaces.Representations;

namespace Api.Domain.DataTransfer.Payload.UserPayloads
{
    public record UserPayload(string Name, string Email, string Password, PersonGender Gender) : IPayload<User>
    {
        public User BuildEntity()
        {
            return new User(Name, Email, Gender, Password);
        }
    }
}
