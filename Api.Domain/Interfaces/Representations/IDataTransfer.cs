using Api.Domain.Entities;

namespace Api.Domain.Interfaces.Representations
{
    public interface IDataTransfer<T> where T : BaseEntity
    {
        IDataTransfer<T> BuildFromEntity(T entity);
    }
}
