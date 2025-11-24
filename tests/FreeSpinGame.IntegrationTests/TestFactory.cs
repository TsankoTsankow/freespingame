using FreeSpinGame.Domain.Entities;
using FreeSpinGame.Infrastructure.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FreeSpinGame.IntegrationTests;

public class TestFactory : WebApplicationFactory<Program>
{
    private const string CampaignId = "1";
    private const int MaxSpins = 3;
    
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<AppDbContext>));
            if(descriptor != null) services.Remove(descriptor);
            
            var connection = new SqliteConnection("DataSource=:memory:");
            
            connection.Open();
            
            services.AddDbContext<AppDbContext>(options => options.UseSqlite(connection));
            
            using var sp  = services.BuildServiceProvider();
            using var scope = sp.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            dbContext.Database.EnsureCreated();
            
            SeedData(dbContext);
            
        });
    }

    private static void SeedData(AppDbContext context)
    {
        context.Campaigns.Add(new Campaign(CampaignId, MaxSpins));
        
        context.SaveChanges();
    }
}