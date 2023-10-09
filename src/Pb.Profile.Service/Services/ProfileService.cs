using Pb.Common.Models;
using Pb.Profile.Service.Models;

namespace Pb.Profile.Service.Services;

public interface IProfileService
{
    public Task<ProfileResult> GetProfiles(ProfileRequest request);
}
public class ProfileService : IProfileService
{
    private readonly ILogger<ProfileService> _log;
    private readonly IDictionary<string?, Hotel> _profiles;

    public ProfileService(ILogger<ProfileService> log, IHotelLoader hotelLoader)
    {
        _log = log;
        _profiles = InitializeProfiles(hotelLoader.Hotels);
    }

    public Task<ProfileResult> GetProfiles(ProfileRequest request)
    {
        return Task.FromResult(new ProfileResult()
            {
                Hotels =
                    _profiles
                        .Where(p => request.HotelIds.Contains(p.Key))
                        .Select(p => p.Value)
                
            }
        );
    }

    private Dictionary<string?, Hotel> InitializeProfiles(IEnumerable<Hotel> hotels)
    {
        var profiles = new Dictionary<string?, Hotel>();

        foreach (var hotel in hotels)
        {
            profiles[hotel.Id] = hotel;
        }

        return profiles;
    }
}