using Api.Domain.Entities;
using Api.Domain.Interfaces.Representations;

namespace Api.Domain.Requests.Pagination
{
    public class PaginatedResponse<DT, E> where DT : IDataTransfer<E> where E : BaseEntity
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public ICollection<DT> Data { get; set; }

        public PaginatedResponse(ICollection<DT> data, int count, int pageNumber, int pageSize)
        {
            Data = data;
            TotalCount = count;
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        }
    }

}
