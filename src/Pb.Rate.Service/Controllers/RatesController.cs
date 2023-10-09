using Microsoft.AspNetCore.Mvc;
using Pb.Common.Models;
using Pb.Rate.Service.Services;

namespace Pb.Rate.Service.Controllers;

public class RatesController : Controller
{
    private readonly ILogger<RatesController> _log;
    private readonly IRateService _rateService;
    
    public RatesController(ILogger<RatesController> log, IRateService profileService)
    {
        _log = log;
        _rateService = profileService;
    }
    
    [HttpPost]
    [Route("/profiles")]
    public async Task<RateResult?> GetRates([FromBody] RateRequest rateRequest)
    {
        return await _rateService.GetRates(rateRequest);
    }
}