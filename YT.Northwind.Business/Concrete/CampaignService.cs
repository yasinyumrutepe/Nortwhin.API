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
    }
}
