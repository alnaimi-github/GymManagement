using GymManagement.Application.Profiles.Commands.CreateAdminProfile;
using GymManagement.Application.Profiles.Queries.ListProfiles;
using GymManagement.Contracts.Profiles;
using Microsoft.AspNetCore.Authorization;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GymManagement.Api.Controllers;

[Route("users/{userId:guid}/profiles")]
public class ProfilesController(ISender _mediator) : ApiController
{
    [HttpPost("admin")]
    [Authorize]
    public async Task<IActionResult> CreateAdminProfile(Guid userId)
    {
        var command = new CreateAdminProfileCommand(userId);

        var createProfileResult = await _mediator.Send(command);

        return createProfileResult.Match(
            id => Ok(new ProfileResponse(id)),
            Problem);
    }

    [HttpGet]
    public async Task<IActionResult> ListProfiles(Guid userId)
    {
        var listProfilesQuery = new ListProfilesQuery(userId);

        var listProfilesResult = await _mediator.Send(listProfilesQuery);

        return listProfilesResult.Match(
            profiles => Ok(new ListProfilesResponse(
                profiles.AdminId,
                profiles.ParticipantId,
                profiles.TrainerId)),
            Problem);
    }
}