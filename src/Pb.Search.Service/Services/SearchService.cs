using Pb.Common.Clients;
using Pb.Common.Models;
namespace Pb.Search.Service.Services;

public interface ISearchService
{
    public Task<SearchResult> Nearby(NearbyRequest request);
}
public class SearchService : ISearchService
{
    private readonly ILogger<SearchService> _log;
    private readonly IGeoClient _geoClient;
    private readonly IRateClient _rateClient;

    public SearchService(ILogger<SearchService> log, IGeoClient geoClient, IRateClient rateClient)
    {
        _log = log;
        _geoClient = geoClient;
        _rateClient = rateClient;
    }

    public async Task<SearchResult> Nearby(NearbyRequest request)
    {
        _log.LogInformation("Search service called with parameters: {Request}", request);

        try
        {
            var nearbyHotels = await _geoClient.GetNearbyHotelsAsync(new GeoRequest()
            {
                Lat = request.Lat,
                Lon = request.Lon
            }) ;//?? throw new RpcException(new Status(StatusCode.Internal,
                //"Profile gRPC service failed to respond in time or the response was null"));

            _log.LogInformation("Successfully retrieved data from Geo Service");

            var hotelRates = await _rateClient.GetRatesAsync(new RateRequest()
            {
                HotelIds =  nearbyHotels?.HotelIds ,
                InDate = request.InDate,
                OutDate = request.OutDate
            }); //?? throw new RpcException(new Status(StatusCode.Internal,
               // "Profile gRPC service failed to respond in time or the response was null"));

            _log.LogInformation("Successfully retrieved data from Rates Service");

            return new SearchResult()
            {
                HotelIds = hotelRates.RatePlans.Select(x => x.HotelId)
            };
        }
        catch (Exception e)
        {
            _log.LogError("One of gRPC services returned null: {Exception}", e);
            return null!;
        }
    }
}