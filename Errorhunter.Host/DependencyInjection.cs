namespace Errorhunter.Host;

public static class DependencyInjection
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<LibertyBanContext>();
        
        return services;
    }
}