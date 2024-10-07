

namespace Northwind.Core.Models.Request.Campaign
{
    public class ChangeStatusCampaignRequestModel
    {
        public int CampaignID { get; set; }
        public bool IsActive { get; set; }
    }
}
