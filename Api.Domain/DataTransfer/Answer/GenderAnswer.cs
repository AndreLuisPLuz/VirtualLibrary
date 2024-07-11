using Api.Domain.Entities;
using Api.Domain.Interfaces;

namespace Api.Domain.DataTransfer.Answer
{
    public record GenderAnswer(Guid Id, String Name) : IDataTransfer<Gender>
    {
        static public IDataTransfer<Gender> BuildFromEntity(Gender gender)
        {
            return new GenderAnswer(gender.Id, gender.Name);
        }

        IDataTransfer<Gender> IDataTransfer<Gender>.BuildFromEntity(Gender gender)
        {
            return BuildFromEntity(gender);
        }
    }
}
