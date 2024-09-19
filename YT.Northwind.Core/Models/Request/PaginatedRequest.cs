

namespace Northwind.Core.Models.Request
{
    public class PaginatedRequest
    {
        public int Page { get; set; } = 0;
        public int Limit { get; set; } = 0;
    }
}
