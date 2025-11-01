using BestPracticesJWT.Core.Configuration;
using BestPracticesJWT.Core.Entities;
using BestPracticesJWT.Core.Interfaces.GenericRepository;
using BestPracticesJWT.Core.Interfaces.Services;
using BestPracticesJWT.Core.Interfaces.UnitOfWork;
using BestPracticesJWT.Data;
using BestPracticesJWT.Data.Repositories;
using BestPracticesJWT.Data.UOW;
using BestPracticesJWT.Service.Services;
using BestPracticesJWT.SharedCommons.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BestPracticesJWT.API.Extentions;

public static class ServiceExtensions
{
    public static void AddServiceExtensions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<CustomTokenOption>(configuration.GetSection("TokenOption"));

        var tokenOptions = configuration.GetSection("TokenOption").Get<CustomTokenOption>();
  
        services.Configure<List<Client>>(configuration.GetSection("Clients"));

        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped(typeof(IServiceGeneric<,>), typeof(ServiceGeneric<,>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnect"),sqlServerOptionsAction =>
            {
                sqlServerOptionsAction.MigrationsAssembly("BestPracticesJWT.Data");
            });
        });

        services.AddIdentity<AppUser, IdentityRole>(options =>
        {
            options.User.RequireUniqueEmail = true;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireDigit = false;

        }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();


    
    }
}
