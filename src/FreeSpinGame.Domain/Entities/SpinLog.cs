namespace FreeSpinGame.Domain.Entities;

public class SpinLog
{
    public int Id { get; private set; }
    public string CampaignId { get; private set; }
    public string PlayerId {get; private set;}
    public DateTime Timestamp { get; private set; }

    public SpinLog(string campaignId, string playerId)
    {
        if (string.IsNullOrWhiteSpace(campaignId)) throw new ArgumentNullException(nameof(campaignId));
        if (string.IsNullOrWhiteSpace(playerId)) throw new ArgumentNullException(nameof(playerId));
        CampaignId = campaignId;
        PlayerId = playerId;
    }

    private SpinLog() {}
}