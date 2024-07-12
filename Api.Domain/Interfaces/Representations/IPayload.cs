using Api.Domain.Entities;

namespace Api.Domain.Interfaces.Representations
{
    public interface IPayload<T> where T : BaseEntity
    {
        T BuildEntity();
    }
}
