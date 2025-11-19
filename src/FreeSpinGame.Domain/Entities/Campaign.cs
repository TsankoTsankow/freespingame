namespace FreeSpinGame.Domain.Entities;

public class Campaign
{
    public string Id { get; private set; }
    public int MaxSpinsPerPlayer { get; set; }

    public Campaign(string id, int maxSpinsPerPlayer)
    {
        if (string.IsNullOrWhiteSpace(id)) throw new ArgumentNullException(nameof(id));
        if (maxSpinsPerPlayer <= 0) throw new ArgumentException("Maximum number of spins cannot be zero or negative", nameof(maxSpinsPerPlayer));
        
        Id = id;
        MaxSpinsPerPlayer = maxSpinsPerPlayer;
    }

    private Campaign()
    {
    }
}