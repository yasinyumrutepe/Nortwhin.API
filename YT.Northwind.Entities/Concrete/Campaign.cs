

namespace Northwind.Entities.Concrete
{
    public class Campaign
    {
        public int CampaignID { get; set; }
        public string CampaignName { get; set; }
        public decimal DiscountAmount { get; set; }
        public bool IsPercent { get; set; }
        public bool IsActive { get; set; }
    }
}
