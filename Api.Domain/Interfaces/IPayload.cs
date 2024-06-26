using Api.Domain.Entities;

namespace Api.Domain.Interfaces
{
    public interface IPayload<T> where T : BaseEntity
    {
        T BuildEntity();
    }
}
