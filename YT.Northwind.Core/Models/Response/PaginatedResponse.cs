
namespace Northwind.Core.Models.Response
{
    public class PaginatedResponse<T>
    {
        public List<T> Data { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalPage { get; set; }
        public int TotalCount { get; set; }


        public PaginatedResponse(List<T> data, int page, int pageSize,int totalPage, int totalCount)
        {
            Data = data;
            Page = page;
            PageSize = pageSize;
            TotalPage = totalPage;
            TotalCount = totalCount;
        }


    }
}
