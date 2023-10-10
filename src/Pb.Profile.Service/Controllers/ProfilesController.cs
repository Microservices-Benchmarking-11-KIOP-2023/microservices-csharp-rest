using Microsoft.AspNetCore.Mvc;
using Pb.Common.Models;
using Pb.Profile.Service.Services;

namespace Pb.Profile.Service.Controllers;

public class ProfilesController : ControllerBase
{
    private readonly IProfileService _profileService;
    
    public ProfilesController(IProfileService profileService)
    {
        _profileService = profileService;
    }
    
    [HttpPost("profile/profiles")]
    public ProfileResult GetProfiles([FromBody] ProfileRequest profileRequest)
    {
        return _profileService.GetProfiles(profileRequest);
    }
}