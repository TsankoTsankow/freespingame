using FreeSpinGame.Domain.Exceptions;

namespace FreeSpinGame.Domain.Entities;

public class PlayerSpinState
{
    public string PlayerId { get; private set; }
    public string CampaignId  { get; private set; }
    public int SpinCount { get; private set; }
    public Guid ConcurrencyKey { get; private set; }

    public PlayerSpinState(string playerId, string campaignId)
    {
        if (string.IsNullOrWhiteSpace(playerId)) throw new ArgumentNullException(nameof(playerId));
        if (string.IsNullOrWhiteSpace(campaignId)) throw new ArgumentNullException(nameof(campaignId));
        
        PlayerId = playerId;
        CampaignId = campaignId;
        SpinCount = 0;
        ConcurrencyKey = Guid.NewGuid();
    }

    private PlayerSpinState()
    {
    }

    public void IncrementSpinCount(int maxNumberOfSpins)
    {
        if (SpinCount >= maxNumberOfSpins)
        {
            throw new SpinLimitReachedException(PlayerId, CampaignId);
        }

        SpinCount++;
        
        ConcurrencyKey = Guid.NewGuid();
    }
}