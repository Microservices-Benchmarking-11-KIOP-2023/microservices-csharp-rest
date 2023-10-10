using System.Text;
using Newtonsoft.Json;
using Pb.Common.Models;

namespace Pb.Common.Clients;

public interface IProfileClient
{
    public Task<ProfileResult?> GetProfilesAsync(ProfileRequest request);
}
public class ProfileClient : IProfileClient
{
    private readonly HttpClient _httpClient;

    public ProfileClient(HttpClient httpClient)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }

    public async Task<ProfileResult?> GetProfilesAsync(ProfileRequest request)
    {
        if (request == null) throw new ArgumentNullException(nameof(request));

        var jsonPayload = JsonConvert.SerializeObject(request);

        var httpContent = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync($"{_httpClient.BaseAddress}profile/profiles", httpContent);

        response.EnsureSuccessStatusCode();

        var jsonResponse = await response.Content.ReadAsStringAsync();
        var profileResult = JsonConvert.DeserializeObject<ProfileResult>(jsonResponse, new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Auto,
            NullValueHandling = NullValueHandling.Ignore,
            ObjectCreationHandling = ObjectCreationHandling.Replace
        });

        return profileResult;
    }
}