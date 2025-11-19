using FreeSpinGame.Domain.Entities;

namespace FreeSpinGame.Domain.Interfaces;

public interface ISpinRepository
{
    Task<Campaign?> GetCampaignAsync(string campaignId);
    Task<PlayerSpinState?> GetPlayerSpinStateAsync(string campaignId, string playerId);
    
    void AddPlayerSpinState(PlayerSpinState playerSpinState);
    void ClearChangeTrackers();
    void AddSpinLog(SpinLog spinLog);
    Task SaveChangesAsync();
}