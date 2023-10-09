using System.Text;
using Newtonsoft.Json;
using Pb.Common.Models;

namespace Pb.Common.Clients;

public interface IRateClient
{
    public Task<RateResult?> GetRatesAsync(RateRequest request);
}

public class RateClient : IRateClient
{
    private readonly HttpClient _httpClient;

    public RateClient(HttpClient httpClient)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }

    public async Task<RateResult?> GetRatesAsync(RateRequest request)
    {
        if (request == null) throw new ArgumentNullException(nameof(request));

        var jsonPayload = JsonConvert.SerializeObject(request);

        var httpContent = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync("rates", httpContent);

        response.EnsureSuccessStatusCode();

        var jsonResponse = await response.Content.ReadAsStringAsync();
        var rateResult = JsonConvert.DeserializeObject<RateResult>(jsonResponse, new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Auto,
            NullValueHandling = NullValueHandling.Ignore,
            ObjectCreationHandling = ObjectCreationHandling.Replace
        });

        return rateResult;
    }
}