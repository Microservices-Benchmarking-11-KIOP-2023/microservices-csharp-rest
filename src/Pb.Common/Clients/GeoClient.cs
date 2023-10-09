using System.Text;
using Newtonsoft.Json;
using Pb.Common.Models;

namespace Pb.Common.Clients;

public interface IGeoClient
{
    public Task<GeoResult?> GetNearbyHotelsAsync(GeoRequest request);
}
public class GeoClient : IGeoClient
{
    private readonly HttpClient _httpClient;

    public GeoClient(HttpClient httpClient)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }

    public async Task<GeoResult?> GetNearbyHotelsAsync(GeoRequest request)
    {
        if (request == null) throw new ArgumentNullException(nameof(request));

        var jsonPayload = JsonConvert.SerializeObject(request);

        var httpContent = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync("nearbyhotels", httpContent);

        response.EnsureSuccessStatusCode();

        var jsonResponse = await response.Content.ReadAsStringAsync();
        var geoResult = JsonConvert.DeserializeObject<GeoResult>(jsonResponse, new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Auto,
            NullValueHandling = NullValueHandling.Ignore,
            ObjectCreationHandling = ObjectCreationHandling.Replace
        });

        return geoResult;
    }
}