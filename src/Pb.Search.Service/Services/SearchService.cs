using Pb.Common.Clients;
using Pb.Common.Models;

namespace Pb.Search.Service.Services;

public interface ISearchService
{
    public Task<SearchResult?> Nearby(NearbyRequest request);
}

public class SearchService : ISearchService
{
    private readonly ILogger<SearchService> _log;
    private readonly IGeoClient _geoClient;
    private readonly IRateClient _rateClient;

    public SearchService(ILogger<SearchService> log, IGeoClient geoClient, IRateClient rateClient)
    {
        _log = log;
        _geoClient = geoClient ?? throw new NullReferenceException("Geo client was not specified. You need to do that before you proceed");
        _rateClient = rateClient ?? throw new NullReferenceException("Rate client was not specified. You need to do that before you proceed");;
    }

    public async Task<SearchResult?> Nearby(NearbyRequest request)
    {
        try
        {
#if DEBUG
            _log.LogInformation("Search service called with parameters: {Request}", request);
            _log.LogInformation("Trying to call Geo service...");
#endif

            var nearbyHotels = await _geoClient.GetNearbyHotelsAsync(new GeoRequest()
            {
                Lat = request.Lat,
                Lon = request.Lon
            });
            
#if DEBUG
            _log.LogInformation("Successfully retrieved data from Geo Service");
            _log.LogInformation("Trying to call Geo service...");
#endif

            var hotelRates = await _rateClient.GetRatesAsync(new RateRequest()
            {
                HotelIds = nearbyHotels?.HotelIds,
                InDate = request.InDate,
                OutDate = request.OutDate
            }); 
            
#if DEBUG
            _log.LogInformation("Successfully retrieved data from Rates Service");
#endif
            
            return new SearchResult()
            {
                HotelIds = hotelRates.RatePlans.Select(x => x.HotelId)
            };
        }
        catch (Exception e)
        {
            _log.LogError("Invalid response code or parameters: {Exception}", e);
            return null;
        }
    }
}