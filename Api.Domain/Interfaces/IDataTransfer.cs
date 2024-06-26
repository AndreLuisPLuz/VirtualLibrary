using Api.Domain.Entities;

namespace Api.Domain.Interfaces
{
    public interface IDataTransfer<T> where T : BaseEntity
    {
        IDataTransfer<T> BuildFromEntity(T entity);
    }
}
