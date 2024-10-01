

namespace Northwind.Core.Models.Request.Campaign
{
    public class CampaignRequestModel
    {
        public string CampaignName { get; set; }
        public decimal DiscountAmount { get; set; }
        public bool IsPercent { get; set; }
    }
}
