using MediatR;

namespace FreeSpinGame.Application.Features.Campaigns.Commands.Spin;

public record SpinCommand(string CampaignId, string PlayerId)
    :IRequest<SpinResult>;

public record SpinResult(int CurrentSpinCount);