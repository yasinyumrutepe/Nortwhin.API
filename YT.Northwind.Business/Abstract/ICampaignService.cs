

using Northwind.Core.Models.Request.Campaign;
using Northwind.Core.Models.Response.Campaign;

namespace Northwind.Business.Abstract
{
    public interface ICampaignService
    {
        public Task<CampaignResponseModel> FindCampaignAsync(string campaignName);
        public Task<CampaignResponseModel> AddCampaignAsync(CampaignRequestModel campaignRequestModel);
        
    }
}
