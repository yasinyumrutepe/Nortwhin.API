
namespace Northwind.Core.Models.Response
{
    public class PaginatedResponse<T>(List<T> data, int page, int pageSize, int totalPage, int totalCount)
    {
        public List<T> Data { get; set; } = data;
        public int Page { get; set; } = page;
        public int PageSize { get; set; } = pageSize;
        public int TotalPage { get; set; } = totalPage;
        public int TotalCount { get; set; } = totalCount;
    }
}
