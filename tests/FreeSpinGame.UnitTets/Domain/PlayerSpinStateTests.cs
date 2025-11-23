using FreeSpinGame.Domain.Entities;
using FreeSpinGame.Domain.Exceptions;
using Xunit;

namespace FreeSpinGame.Application.UnitTets.Domain;

public class PlayerSpinStateTests
{
    private const string PlayerId = "1";
    private const string CampaignId = "2";
    private const int MaxSpinCount = 3;
    private PlayerSpinState CreatePlayerSpinState() => new PlayerSpinState(PlayerId, CampaignId, MaxSpinCount);
    
    [Fact]
    public void IncrementSpinCount_ShouldIncreaseCount_WhenBelowLimit()
    {
        //Arrange
        var playerSpinState = CreatePlayerSpinState();
        
        //Act
        playerSpinState.IncrementSpinCount();
        
        
        //Assert
        Assert.Equal(1, playerSpinState.SpinCount);
    }

    [Fact]
    public void IncrementSpinCount_ShouldThrowException_WhenLimitReached()
    {
        //Arrange
        var playerSpinState = new PlayerSpinState(PlayerId, CampaignId, 1);

        //Act
        playerSpinState.IncrementSpinCount();
        
        //Assert
        Assert.Throws<SpinLimitReachedException>(() => playerSpinState.IncrementSpinCount());
    }

    [Fact]
    public void IncrementSpinCount_ShouldRotateConcurrencyKey()
    {
        //Arrange
        var playerSpinState = CreatePlayerSpinState();

        var oldKey = playerSpinState.ConcurrencyKey;
        
        //Act
        playerSpinState.IncrementSpinCount();
        
        //Assert
        Assert.NotEqual(oldKey, playerSpinState.ConcurrencyKey);
    }
}