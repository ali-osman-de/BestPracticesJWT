using BestPracticesJWT.SharedCommons.Extentions;

namespace MiniApp1.API.Extensions;

public static class ServiceExtensions
{
    public static void AddServiceExtensions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<BestPracticesJWT.SharedCommons.Configuration.CustomTokenOption>(configuration.GetSection("TokenOption"));

        var tokenOptions = configuration.GetSection("TokenOption").Get<BestPracticesJWT.SharedCommons.Configuration.CustomTokenOption>();

        services.AddCustomTokenAuth(tokenOptions);
    }
}
