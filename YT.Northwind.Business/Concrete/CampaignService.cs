using AutoMapper;
using Northwind.Business.Abstract;
using Northwind.Core.Models.Request.Campaign;
using Northwind.Core.Models.Response.Campaign;
using Northwind.DataAccess.Repositories.Abstract;
using Northwind.Entities.Concrete;

namespace Northwind.Business.Concrete
{
    public class CampaignService : ICampaignService
    {
        private readonly ICampaignRepository _campaignRepository;
        private readonly IMapper _mapper;

        public CampaignService(ICampaignRepository campaignRepository,IMapper mapper)
        {
            _campaignRepository = campaignRepository;
            _mapper = mapper;
        }
        public async Task<List<CampaignResponseModel>> GetAllCampaignsAsync()
        {
            var campaigns = await _campaignRepository.GetAllAsync();
            return _mapper.Map<List<CampaignResponseModel>>(campaigns);
        }

        public async Task<CampaignResponseModel> FindCampaignAsync(string campaignName)
        {
            var campaign = await _campaignRepository.GetAsync(c=>c.CampaignName == campaignName);
            return _mapper.Map<CampaignResponseModel>(campaign);

        }

        public async Task<CampaignResponseModel> AddCampaignAsync(CampaignRequestModel campaignRequestModel)
        {           
            var campaign = _mapper.Map<Campaign>(campaignRequestModel);
            var addedCampaign = await _campaignRepository.AddAsync(campaign);
            return _mapper.Map<CampaignResponseModel>(addedCampaign);
        }

        public async Task<CampaignResponseModel> ChangeStatusCampaingAsync(ChangeStatusCampaignRequestModel changeStatusCampaignRequestModel)
        {
            var campaign = await _campaignRepository.GetAsync(filter:u=>u.CampaignID == changeStatusCampaignRequestModel.CampaignID) ;
            if (campaign == null)
            {
                throw new Exception("Campaign not found");
            }
            campaign.IsActive = changeStatusCampaignRequestModel.IsActive;
            var updatedCampaign = await _campaignRepository.UpdateAsync(campaign);
            return _mapper.Map<CampaignResponseModel>(updatedCampaign);
        }
    }
}
