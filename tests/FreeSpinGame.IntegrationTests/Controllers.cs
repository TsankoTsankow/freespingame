using System.Net;
using System.Net.Http.Json;
using FreeSpinGame.Application.Features.Campaigns.Queries.GetStatus;

namespace FreeSpinGame.IntegrationTests;

public class Controllers :IClassFixture<TestFactory>
{
    private readonly HttpClient _client;
    
    public Controllers(TestFactory factory)
    {
        _client = factory.CreateClient();
    }
    [Fact]
    public async Task Spin_ShouldNotExceedMaxSpins_UnderHighLoad()
    {
        //Arrange
        string campaignId = "1";
        string playerId = "2";
        int maxSpins = 3;
        const int parallelRequests = 30;
        
        //Act
        var tasks = Enumerable.Range(0, parallelRequests)
            .Select(_ => _client.PostAsync(
                $"capmaigns/{campaignId}/players/{playerId}/spin", null))
            .ToList();
        
        var responses = await Task.WhenAll(tasks);
        
        var playerStatusResponseMessage = await _client.GetAsync($"capmaigns/{campaignId}/players/{playerId}");
        var playerStatus = await playerStatusResponseMessage.Content.ReadFromJsonAsync<PlayerSpinStatusViewModel>();
        
        //Assert
        int succeeded = responses.Count(r => r.IsSuccessStatusCode);
        int failed = responses.Count(r => r.StatusCode == HttpStatusCode.Forbidden || r.StatusCode == HttpStatusCode.InternalServerError);
        
        Assert.Equal(maxSpins, succeeded);
        Assert.Equal(parallelRequests - maxSpins, failed);
        Assert.Equal(maxSpins, playerStatus.SpinCount);
    }
}