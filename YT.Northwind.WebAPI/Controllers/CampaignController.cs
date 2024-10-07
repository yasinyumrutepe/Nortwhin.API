using Microsoft.AspNetCore.Mvc;
using Northwind.Business.Abstract;
using Northwind.Business.Filter;
using Northwind.Core.Models.Request.Campaign;

namespace Northwind.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CampaignController : Controller
    {
        private readonly ICampaignService _campaignService;

        public CampaignController(ICampaignService campaignService)
        {
            _campaignService = campaignService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var campaigns = await _campaignService.GetAllCampaignsAsync();
            return Ok(campaigns);
        }


        [HttpGet("{campaignName}")]
        public async Task<IActionResult>FindCampaing(string campaignName)
        {
            var campaign = await _campaignService.FindCampaignAsync(campaignName);
            if (campaign == null)
            {
                return NotFound();
            }

            return Ok(campaign);
        }



        [HttpPost]
        [ServiceFilter(typeof(CustomAuthorizationFilter))]
        public async Task<IActionResult> AddCampaign(CampaignRequestModel campaignRequest)
        {
            return Ok( await _campaignService.AddCampaignAsync(campaignRequest));
        }

        [HttpPut]
        [ServiceFilter(typeof(CustomAuthorizationFilter))]
        public async Task<IActionResult> ChangeStatusCampaign([FromBody] ChangeStatusCampaignRequestModel changeStatusCampaign )
        {
            return Ok(await _campaignService.ChangeStatusCampaingAsync(changeStatusCampaign));
        }

    }
}
