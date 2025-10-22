namespace E_Commerce.Shared.DataTransferObjects;
public record PaginatedResult<TResult>(
    int PageIndex, int PageCount, int TotalCount, IEnumerable<TResult> Data);
