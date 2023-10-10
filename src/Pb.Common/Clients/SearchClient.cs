using System.Net.Http.Json;
using System.Text;
using Newtonsoft.Json;
using Pb.Common.Models;

namespace Pb.Common.Clients;

public interface ISearchClient
{
    public Task<SearchResult?> GetNearbyHotelsAsync(NearbyRequest request);
}
public class SearchClient : ISearchClient
{
    private readonly HttpClient _httpClient;

    public SearchClient(HttpClient httpClient)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }

    public async Task<SearchResult?> GetNearbyHotelsAsync(NearbyRequest request)
    {
        if (request == null) throw new ArgumentNullException(nameof(request));

        var jsonPayload = JsonConvert.SerializeObject(request);

        var httpContent = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync($"{_httpClient.BaseAddress}search/nearby", httpContent);

        response.EnsureSuccessStatusCode();
        
        var jsonResponse = await response.Content.ReadAsStringAsync();
        var searchResult = JsonConvert.DeserializeObject<SearchResult>(jsonResponse, new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Auto,
            NullValueHandling = NullValueHandling.Ignore,
            ObjectCreationHandling = ObjectCreationHandling.Replace
        });

        return searchResult;
    }
}