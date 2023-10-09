using Microsoft.AspNetCore.Mvc;
using Pb.Common.Models;
using Pb.Profile.Service.Services;

namespace Pb.Profile.Service.Controllers;

public class ProfilesController : Controller
{
    private readonly ILogger<ProfilesController> _log;
    private readonly IProfileService _profileService;
    
    public ProfilesController(ILogger<ProfilesController> log, IProfileService profileService)
    {
        _log = log;
        _profileService = profileService;
    }
    
    [HttpGet]
    [Route("/profiles")]
    public async Task<ProfileResult?> GetProfiles([FromBody] ProfileRequest profileRequest)
    {
        return await _profileService.GetProfiles(profileRequest);
    }
}