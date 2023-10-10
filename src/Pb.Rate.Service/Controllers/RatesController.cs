using Microsoft.AspNetCore.Mvc;
using Pb.Common.Models;
using Pb.Rate.Service.Services;

namespace Pb.Rate.Service.Controllers;

public class RatesController : ControllerBase
{
    private readonly IRateService _rateService;
    
    public RatesController(IRateService profileService)
    {
        _rateService = profileService;
    }
    
    [HttpPost("rate/rates")]
    public RateResult? GetRates([FromBody] RateRequest rateRequest)
    {
        return _rateService.GetRates(rateRequest);
    }
}