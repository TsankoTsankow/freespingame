using FreeSpinGame.Domain.Entities;
using FreeSpinGame.Domain.Interfaces;

namespace FreeSpinGame.Infrastructure.Persistence;

public class SpinRepository (AppDbContext context) : ISpinRepository
{
    public async Task<Campaign?> GetCampaignAsync(string campaignId)
    {
        return await context.Campaigns.FindAsync(campaignId);
    }

    public async Task<PlayerSpinState?> GetPlayerSpinStateAsync(string campaignId, string playerId)
    {
        return await context.PlayerSpinStates.FindAsync(campaignId, playerId);
    }

    public void AddPlayerSpinState(PlayerSpinState playerSpinState)
    {
        context.PlayerSpinStates.Add(playerSpinState);
    }

    public void ClearChangeTrackers()
    {
        context.ChangeTracker.Clear();
    }

    public void AddSpinLog(SpinLog spinLog)
    {
        context.SpinLogs.Add(spinLog);
    }

    public async Task SaveChangesAsync()
    {
        await context.SaveChangesAsync();
    }
}