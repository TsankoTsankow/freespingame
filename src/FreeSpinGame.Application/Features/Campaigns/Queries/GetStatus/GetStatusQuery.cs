using MediatR;

namespace FreeSpinGame.Application.Features.Campaigns.Queries.GetStatus;

public record GetStatusQuery(string CampaignId, string PlayerId) : IRequest<PlayerSpinStatusViewModel>;

public record PlayerSpinStatusViewModel(string PlayerId, string CampaignId, int SpinCount, int MaxSpinsAllowed);