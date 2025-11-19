using FreeSpinGame.Application.Features.Campaigns.Commands.Spin;
using FreeSpinGame.Application.Features.Campaigns.Queries.GetStatus;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FreeSpinGame.Api.Controllers;

[ApiController]
[Route("capmaigns/{campaignId}/players/{playerId}")]
public class SpinController (IMediator mediator) : ControllerBase
{
    [HttpPost("spin")]
    public async Task<IActionResult> Spin(string campaignId, string playerId)
    {
        var command = new SpinCommand(campaignId, playerId);
        var result = await mediator.Send(command);
        return Ok(new {count = result.CurrentSpinCount});
    }

    [HttpGet]
    public async Task<IActionResult> GetPlayerSpinStatus(string campaignId, string playerId)
    {
        var query = new GetStatusQuery(campaignId, playerId);
        var result = await mediator.Send(query);
        return Ok(result);
    }
}