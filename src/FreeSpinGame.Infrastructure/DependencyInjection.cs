using FreeSpinGame.Application.Common.Interfaces;
using FreeSpinGame.Domain.Interfaces;
using FreeSpinGame.Infrastructure.Persistence;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FreeSpinGame.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        var connection = new SqliteConnection("DataSource=:memory:");
        connection.Open();
        
        services.AddDbContext<AppDbContext>(options => 
            options.UseSqlite(connection));
        
        services.AddScoped<IAppDbContext>(provider => provider.GetRequiredService<AppDbContext>());
        services.AddScoped<ISpinRepository, SpinRepository>();
        
        return services;
    }
}