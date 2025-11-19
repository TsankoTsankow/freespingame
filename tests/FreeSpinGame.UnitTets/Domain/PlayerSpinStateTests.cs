using FreeSpinGame.Domain.Entities;
using FreeSpinGame.Domain.Exceptions;
using Xunit;

namespace FreeSpinGame.Application.UnitTets.Domain;

public class PlayerSpinStateTests
{
    private const string _campaignId = "1";
    private const string _playerId = "1";
    private const int _maxSpinCount = 3;
    private PlayerSpinState CreatePlayerSpinState() => new PlayerSpinState(_campaignId, _playerId);
    
    [Fact]
    public void IncrementSpinCount_ShouldIncreaseCount_WhenBelowLimit()
    {
        var playerSpinState = CreatePlayerSpinState();
        
        playerSpinState.IncrementSpinCount(_maxSpinCount);
        
        Assert.Equal(1, playerSpinState.SpinCount);
    }

    [Fact]
    public void IncrementSpinCount_ShouldThrowException_WhenLimitReached()
    {
        var playerSpinState = CreatePlayerSpinState();

        int maxSpinCount = 1;
        
        playerSpinState.IncrementSpinCount(_maxSpinCount);
        
        Assert.Throws<SpinLimitReachedException>(() => playerSpinState.IncrementSpinCount(maxSpinCount));
    }

    [Fact]
    public void IncrementSpinCount_ShouldRotateConcurrencyKey()
    {
        var playerSpinState = CreatePlayerSpinState();

        var oldKey = playerSpinState.ConcurrencyKey;
        
        playerSpinState.IncrementSpinCount(_maxSpinCount);
        
        Assert.NotEqual(oldKey, playerSpinState.ConcurrencyKey);
    }
}