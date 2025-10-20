using BestPracticesJWT.Core.Interfaces.Services;
using BestPracticesJWT.Service.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BestPracticesJWT.Service.Extensions;
public static class ServiceExtensions
{
    public static void AddServiceLayerExtensions(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ITokenService, TokenService>();
    }
}
