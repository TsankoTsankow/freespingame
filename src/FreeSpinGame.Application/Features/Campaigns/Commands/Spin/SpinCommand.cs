using MediatR;

namespace FreeSpinGame.Application.Features.Campaigns.Commands.Spin;

public record SpinCommand(string PlayerId, string CampaignId)
    :IRequest<SpinResult>;

public record SpinResult(int CurrentSpinCount);