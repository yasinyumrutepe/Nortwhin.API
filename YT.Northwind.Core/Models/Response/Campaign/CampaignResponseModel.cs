

namespace Northwind.Core.Models.Response.Campaign
{
    public class CampaignResponseModel
    {
        public string CampaignName { get; set; }
        public decimal DiscountAmount { get; set; }
        public bool IsPercent { get; set; }
    }
}
