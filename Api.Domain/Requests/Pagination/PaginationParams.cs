namespace Api.Domain.Requests.Pagination
{
    public record PaginationParams(
        int PageNumber = 1,
        int PageSize = 10) { }
}
