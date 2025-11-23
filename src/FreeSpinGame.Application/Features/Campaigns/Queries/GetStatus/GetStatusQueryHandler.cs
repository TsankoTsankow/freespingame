using FreeSpinGame.Application.Common.Interfaces;
using FreeSpinGame.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FreeSpinGame.Application.Features.Campaigns.Queries.GetStatus;

public class GetStatusQueryHandler (IAppDbContext context) : IRequestHandler<GetStatusQuery, PlayerSpinStatusViewModel>
{
    public async Task<PlayerSpinStatusViewModel> Handle(GetStatusQuery request, CancellationToken cancellationToken)
    {
        var playerSpinStatus = await context.PlayerSpinStates
            .AsNoTracking()
            .Where(p => p.CampaignId == request.CampaignId && p.PlayerId == request.PlayerId)
            .FirstOrDefaultAsync(cancellationToken);

        if (playerSpinStatus is null) throw  new EntityNotFoundException("Player", request.PlayerId);
        
        return new PlayerSpinStatusViewModel(
            request.PlayerId,
            request.CampaignId,
            playerSpinStatus.SpinCount,
            playerSpinStatus.MaxSpinCount);
    }
}