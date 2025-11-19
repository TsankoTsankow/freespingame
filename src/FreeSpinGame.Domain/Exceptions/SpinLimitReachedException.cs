namespace FreeSpinGame.Domain.Exceptions;

public class SpinLimitReachedException : Exception
{
    public  SpinLimitReachedException(string playerId, string campaignId) : base($"Player {playerId} has reached maximum number of spins for the {campaignId} campaign.") {}
}