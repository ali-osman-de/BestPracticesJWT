using BestPracticesJWT.Core.Configuration;
using BestPracticesJWT.Core.Entities;
using BestPracticesJWT.SharedCommons.Dtos;

namespace BestPracticesJWT.Core.Interfaces.Services;

public interface ITokenService
{
    TokenDto CreateToken(AppUser appUser);
    ClientTokenDto CreateTokenByClient(Client client);
}
