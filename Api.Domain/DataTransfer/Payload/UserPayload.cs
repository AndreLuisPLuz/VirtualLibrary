using Api.Domain.Entities;
using Api.Domain.Interfaces;

namespace Api.Domain.DataTransfer.Payload
{
    public record UserPayload(String Name,  String Email, String Password, PersonGender Gender) : IPayload<User>
    {
        public User BuildEntity()
        {
            return new User(Name, Email, Gender, Password);
        }
    }
}
