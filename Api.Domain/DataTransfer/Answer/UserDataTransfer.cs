using Api.Domain.Entities;
using Api.Domain.Interfaces.Representations;

namespace Api.Domain.DataTransfer.Answer
{
    public record UserDataTransfer(Guid Id, string Name, string Email, PersonGender Gender) : IDataTransfer<User>
    {
        static public IDataTransfer<User> BuildFromEntity(User user)
        {
            return new UserDataTransfer(user.Id, user.Name, user.Email, user.Gender);
        }

        IDataTransfer<User> IDataTransfer<User>.BuildFromEntity(User user)
        {
            return BuildFromEntity(user);
        }
    }
}
