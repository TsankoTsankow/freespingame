using FreeSpinGame.Application.Common.Interfaces;
using FreeSpinGame.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FreeSpinGame.Infrastructure.Persistence;

public class AppDbContext : DbContext, IAppDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) :  base(options) {}
    
    public DbSet<Campaign> Campaigns { get; set; }
    public DbSet<PlayerSpinState> PlayerSpinStates { get; set; }
    public DbSet<SpinLog> SpinLogs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Campaign>().HasKey(c => c.Id);
        
        var stateBuilder = modelBuilder.Entity<PlayerSpinState>();
        stateBuilder.HasKey(c => new { c.PlayerId, c.CampaignId });
        stateBuilder.Property(p => p.ConcurrencyKey).IsConcurrencyToken();
        
        modelBuilder.Entity<SpinLog>().HasKey(sl => sl.Id);
    }
}