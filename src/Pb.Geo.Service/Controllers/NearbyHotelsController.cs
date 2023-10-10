using Microsoft.AspNetCore.Mvc;
using Pb.Common.Models;
using Pb.Geo.Service.Services;

namespace Pb.Geo.Service.Controllers;

public class GeoController : ControllerBase
{
    private readonly IGeoService _geoService;
    
    public GeoController(IGeoService searchService)
    {
        _geoService = searchService;
    }
    
    [HttpPost("geo/nearby")]
    public GeoResult? Nearby([FromBody] GeoRequest nearbyRequest)
    {
        return _geoService.Nearby(nearbyRequest);
    }
}