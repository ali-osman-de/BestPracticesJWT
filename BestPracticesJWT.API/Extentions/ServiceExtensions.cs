using BestPracticesJWT.Core.Configuration;
using BestPracticesJWT.SharedCommons.Configuration;

namespace BestPracticesJWT.API.Extentions;

public static class ServiceExtensions
{
    public static void AddServiceExtensions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<CustomTokenOption>(configuration.GetSection("TokenOption"));
        services.Configure<List<Client>>(configuration.GetSection("Clients"));
    }
}
