using FreeSpinGame.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FreeSpinGame.Application.Features.Campaigns.Queries.GetStatus;

public class GetStatusQueryHandler (IAppDbContext context) : IRequestHandler<GetStatusQuery, PlayerSpinStatusViewModel>
{
    public async Task<PlayerSpinStatusViewModel> Handle(GetStatusQuery request, CancellationToken cancellationToken)
    {
        var campaign = await context.Campaigns
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == request.CampaignId, cancellationToken);

        int maxSpins = campaign?.MaxSpinsPerPlayer ?? 0;

        var currentSpinCount = await context.PlayerSpinStates
            .AsNoTracking()
            .Where(p => p.CampaignId == request.CampaignId && p.PlayerId == request.PlayerId)
            .Select(p => p.SpinCount)
            .FirstOrDefaultAsync(cancellationToken);

        return new PlayerSpinStatusViewModel(
            request.CampaignId,
            request.PlayerId,
            currentSpinCount,
            maxSpins);
    }
}