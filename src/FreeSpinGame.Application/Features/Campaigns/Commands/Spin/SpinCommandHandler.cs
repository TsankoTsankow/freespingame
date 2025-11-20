using FreeSpinGame.Domain.Entities;
using FreeSpinGame.Domain.Exceptions;
using FreeSpinGame.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FreeSpinGame.Application.Features.Campaigns.Commands.Spin;

public class SpinCommandHandler (ISpinRepository repository) : IRequestHandler<SpinCommand, SpinResult>
{
    public async Task<SpinResult> Handle(SpinCommand request, CancellationToken cancellationToken)
    {
        int maxRetries = 3;
        int attempts = 0;
        
        while (attempts < maxRetries)
        {
            attempts++;
            
            try
            {
                var campaign = await repository.GetCampaignAsync(request.CampaignId);

                if (campaign is null) throw new  EntityNotFoundException("Campaign", request.CampaignId);

                var playerSpinState = await repository.GetPlayerSpinStateAsync(request.PlayerId, request.CampaignId);

                if (playerSpinState is null)
                {
                    playerSpinState = new PlayerSpinState(request.PlayerId, request.CampaignId);
                    repository.AddPlayerSpinState(playerSpinState);
                }

                playerSpinState.IncrementSpinCount(campaign.MaxSpinsPerPlayer);

                var spinLog = new SpinLog(request.CampaignId, request.PlayerId);
                repository.AddSpinLog(spinLog);

                await repository.SaveChangesAsync();

                return new SpinResult(playerSpinState.SpinCount);
            }
            catch (DbUpdateConcurrencyException)
            {
                repository.ClearChangeTrackers();
                if (attempts == maxRetries) throw;
            }
        }
        
        throw new Exception("System overloaded");
    }
}