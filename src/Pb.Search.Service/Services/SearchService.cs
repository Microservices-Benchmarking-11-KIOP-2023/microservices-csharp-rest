using Pb.Common.Clients;
using Pb.Common.Models;

namespace Pb.Search.Service.Services;

public interface ISearchService
{
    public Task<SearchResult?> Nearby(NearbyRequest request);
}

public class SearchService : ISearchService
{
    private readonly IGeoClient _geoClient;
    private readonly IRateClient _rateClient;

    public SearchService(IGeoClient geoClient, IRateClient rateClient)
    {
        _geoClient = geoClient ?? throw new NullReferenceException("Geo client was not specified. You need to do that before you proceed");
        _rateClient = rateClient ?? throw new NullReferenceException("Rate client was not specified. You need to do that before you proceed");;
    }

    public async Task<SearchResult?> Nearby(NearbyRequest request)
    {
        try
        {
            var nearbyHotels = await _geoClient.GetNearbyHotelsAsync(new GeoRequest()
            {
                Lat = request.Lat,
                Lon = request.Lon
            });
            
            var hotelRates = await _rateClient.GetRatesAsync(new RateRequest()
            {
                HotelIds = nearbyHotels?.HotelIds,
                InDate = request.InDate,
                OutDate = request.OutDate
            }); 
            
            return new SearchResult()
            {
                HotelIds = hotelRates.RatePlans.Select(x => x.HotelId)
            };
        }
        catch (Exception e)
        {
            return null;
        }
    }
}