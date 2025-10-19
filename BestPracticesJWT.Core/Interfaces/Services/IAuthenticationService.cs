using BestPracticesJWT.Core.Dtos;
using BestPracticesJWT.SharedCommons.Dtos;

namespace BestPracticesJWT.Core.Interfaces.Services;

public interface IAuthenticationService
{ 
    Task<ResponseDto<TokenDto>> CreateTokenAsync(LoginDto loginDto);
    Task<ResponseDto<TokenDto>> CreateTokenByRefreshToken(string refreshToken);
    Task<ResponseDto<NoDataDto>> RevokeRefreshToken(string refreshToken);
    Task<ResponseDto<ClientTokenDto>> CreateTokenByClientAsync(ClientLoginDto clientLoginDto);
}
