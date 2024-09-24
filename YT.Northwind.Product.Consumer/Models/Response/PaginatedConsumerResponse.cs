namespace Northwind.Product.Consumer.Models.Response
{
    public class PaginatedConsumerResponse<T>
    {

        public List<T> Data { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalPage { get; set; }
        public int TotalCount { get; set; }


        public PaginatedConsumerResponse(List<T> data, int page, int pageSize, int totalPage, int totalCount)
        {
            Data = data;
            Page = page;
            PageSize = pageSize;
            TotalPage = totalPage;
            TotalCount = totalCount;
        }


    }
}
