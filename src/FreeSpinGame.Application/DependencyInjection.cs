using Microsoft.Extensions.DependencyInjection;

namespace FreeSpinGame.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddMediatr(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
        });
        
        return services;
    }
}