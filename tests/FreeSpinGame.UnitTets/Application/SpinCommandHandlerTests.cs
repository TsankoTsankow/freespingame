using FakeItEasy;
using FreeSpinGame.Application.Features.Campaigns.Commands.Spin;
using FreeSpinGame.Domain.Entities;
using FreeSpinGame.Domain.Exceptions;
using FreeSpinGame.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace FreeSpinGame.Application.UnitTets.Application;

public class SpinCommandHandlerTests
{
    private readonly ISpinRepository _fakeRepository;
    private readonly SpinCommandHandler _sut;
    private const string PlayerId = "1";
    private const string CampaignId = "1";
    private const int MaxSpinCount = 3;

    public SpinCommandHandlerTests()
    {
        _fakeRepository = A.Fake<ISpinRepository>();
        _sut = new SpinCommandHandler(_fakeRepository);
    }

    [Fact]
    public async Task Handle_ShouldRetry_WhenConcurrencyExceptionOccurs()
    {
        //Arrange
        var command = new SpinCommand(PlayerId, CampaignId);
        var campaign = new Campaign(CampaignId, MaxSpinCount);
        var state = new PlayerSpinState(PlayerId, CampaignId);
        
        A.CallTo(() => _fakeRepository.GetCampaignAsync(CampaignId))
            .Returns(campaign);
        A.CallTo(() => _fakeRepository.GetPlayerSpinStateAsync(PlayerId, CampaignId))
            .ReturnsLazily(() => new PlayerSpinState(PlayerId, CampaignId));
        A.CallTo(() => _fakeRepository.SaveChangesAsync())
            .ThrowsAsync(new DbUpdateConcurrencyException()).Once()
            .Then
            .Returns(Task.CompletedTask);
        
        //Act
        var result = await _sut.Handle(command, CancellationToken.None);
        
        //Assert
        Assert.Equal(1, result.CurrentSpinCount);
        
        A.CallTo(() => _fakeRepository.SaveChangesAsync())
            .MustHaveHappened(2, Times.Exactly);
        
        A.CallTo(() => _fakeRepository.ClearChangeTrackers())
            .MustHaveHappened();
    }

    [Fact]
    public async Task Handle_ShouldThrow404Exception_WhenCampaignNotFound()
    {
        //Arrange
        var command = new SpinCommand(PlayerId, CampaignId);
        
        A.CallTo(() => _fakeRepository.GetCampaignAsync(CampaignId))
            .Returns((Campaign?)null);
        
        //Act and assert
        await Assert.ThrowsAsync<EntityNotFoundException>(async () => 
            await _sut.Handle(command, CancellationToken.None));
        
    }
}