using Microsoft.AspNetCore.Mvc;
using Pb.Common.Models;
using Pb.Search.Service.Services;

namespace Pb.Search.Service.Controllers;

public class NearbyController : Controller
{
    private readonly ILogger<NearbyController> _log;
    private readonly ISearchService _searchService;
    
    public NearbyController(ILogger<NearbyController> log, ISearchService searchService)
    {
        _log = log;
        _searchService = searchService;
    }
    
    [HttpPost]
    [Route("nearby")]
    public async Task<SearchResult?> Nearby([FromBody] NearbyRequest nearbyRequest)
    {
        return await _searchService.Nearby(nearbyRequest);
    }
}