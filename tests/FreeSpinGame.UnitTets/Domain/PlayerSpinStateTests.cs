using FreeSpinGame.Domain.Entities;
using FreeSpinGame.Domain.Exceptions;
using Xunit;

namespace FreeSpinGame.Application.UnitTets.Domain;

public class PlayerSpinStateTests
{
    private readonly string _campaignId = "1";
    private readonly string _playerId = "1";
    private readonly int _maxSpinCount = 3;
    
    [Fact]
    public void IncrementSpinCount_ShouldIncreaseCount_WhenBelowLimit()
    {
        var playerSpinState = new PlayerSpinState(_campaignId, _playerId);
        
        playerSpinState.IncrementSpinCount(_maxSpinCount);
        
        Assert.Equal(1, playerSpinState.SpinCount);
    }

    [Fact]
    public void IncrementSpinCount_ShouldThrowException_WhenLimitReached()
    {
        var playerSpinState = new PlayerSpinState(_campaignId, _playerId);

        int maxSpinCount = 1;
        
        playerSpinState.IncrementSpinCount(_maxSpinCount);
        
        Assert.Throws<SpinLimitReachedException>(() => playerSpinState.IncrementSpinCount(maxSpinCount));
    }

    [Fact]
    public void IncrementSpinCount_ShouldRotateConcurrencyKey()
    {
        var playerSpinState = new PlayerSpinState(_campaignId, _playerId);

        var oldKey = playerSpinState.ConcurrencyKey;
        
        playerSpinState.IncrementSpinCount(_maxSpinCount);
        
        Assert.NotEqual(oldKey, playerSpinState.ConcurrencyKey);
    }
}