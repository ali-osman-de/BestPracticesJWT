using BestPracticesJWT.SharedCommons.Configuration;
using BestPracticesJWT.SharedCommons.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;

namespace BestPracticesJWT.SharedCommons.Extentions;

public static class ServiceExtensions
{
    public static void AddCustomTokenAuth(this IServiceCollection services, CustomTokenOption tokenOptions)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

        }).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opts =>
        {
            opts.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
            {
                ValidIssuer = tokenOptions.Issuer,
                ValidAudience = tokenOptions.Audience[0],
                IssuerSigningKey = SignService.GetSymmetricSecurityKey(tokenOptions.SecurityKey),

                ValidateIssuerSigningKey = true,
                ValidateAudience = false,
                ValidateIssuer = true,
                ValidateLifetime = true,

                ClockSkew = TimeSpan.Zero
            };
        });
    }

}
