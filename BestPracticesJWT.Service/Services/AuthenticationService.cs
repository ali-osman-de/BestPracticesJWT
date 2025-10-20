using BestPracticesJWT.Core.Configuration;
using BestPracticesJWT.Core.Dtos;
using BestPracticesJWT.Core.Entities;
using BestPracticesJWT.Core.Interfaces.GenericRepository;
using BestPracticesJWT.Core.Interfaces.Services;
using BestPracticesJWT.Core.Interfaces.UnitOfWork;
using BestPracticesJWT.SharedCommons.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace BestPracticesJWT.Service.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly List<Client> _client;
    private readonly ITokenService _tokenService;
    private readonly UserManager<AppUser> _userManager;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGenericRepository<UserRefreshToken> _userRefreshTokenService;

    public AuthenticationService(IOptions<List<Client>> client, 
                                 ITokenService tokenService, 
                                 UserManager<AppUser> userManager, 
                                 IUnitOfWork unitOfWork, 
                                 IGenericRepository<UserRefreshToken> userRefreshTokenService)
    {
        _client = client.Value;
        _tokenService = tokenService;
        _userManager = userManager;
        _unitOfWork = unitOfWork;
        _userRefreshTokenService = userRefreshTokenService;
    }

    public async Task<ResponseDto<TokenDto>> CreateTokenAsync(LoginDto loginDto)
    {
        if (loginDto == null) throw new ArgumentNullException(nameof(loginDto));

        var user = await _userManager.FindByEmailAsync(loginDto.Email);

        if (user == null) return ResponseDto<TokenDto>.Fail("Email or Password is wrong!", 404, true);

        if (!await _userManager.CheckPasswordAsync(user, loginDto.Password)) {
            ResponseDto<TokenDto>.Fail("Email or Password is wrong!", 404, true);
        }

        var token = _tokenService.CreateToken(user);

        var userRefreshToken = await _userRefreshTokenService.GetWhere(x => x.UserId == user.Id).SingleOrDefaultAsync();

        if (userRefreshToken==null)
        {
            await _userRefreshTokenService.AddSync(new UserRefreshToken { UserId= user.Id, Code=token.RefreshToken, Expiration=token.RefreshTokenExpiration });
        }
        else
        {
            userRefreshToken.Code = token.RefreshToken;
            userRefreshToken.Expiration = token.RefreshTokenExpiration;
        }

        await _unitOfWork.CommitAsync();

        return ResponseDto<TokenDto>.Success(token, 200);

    }

    public ResponseDto<ClientTokenDto> CreateTokenByClientAsync(ClientLoginDto clientLoginDto)
    {
        var client = _client.SingleOrDefault(x=> x.Id==clientLoginDto.ClientId && x.Secret==clientLoginDto.ClientSecret);

        if (client == null) {

            return ResponseDto<ClientTokenDto>.Fail("ClientId veya secret bulunamadı!", 404, true);
            
        }

        var token = _tokenService.CreateTokenByClient(client);
        return ResponseDto<ClientTokenDto>.Success(token, 200);
    }

    public async Task<ResponseDto<TokenDto>> CreateTokenByRefreshToken(string refreshToken)
    {
        var existsRefreshToken = await _userRefreshTokenService.GetWhere(x => x.Code == refreshToken).SingleOrDefaultAsync();

        if (existsRefreshToken == null) {

            return ResponseDto<TokenDto>.Fail("Refresh token not found", 404, true);

        }

        var user = await _userManager.FindByIdAsync(existsRefreshToken.UserId);
        if (user == null)
        {
            return ResponseDto<TokenDto>.Fail("User not found", 404, true);
        }

        var tokenDto = _tokenService.CreateToken(user);

        existsRefreshToken.Code = tokenDto.RefreshToken;
        existsRefreshToken.Expiration = tokenDto.RefreshTokenExpiration;

        await _unitOfWork.CommitAsync();

        return ResponseDto<TokenDto>.Success(tokenDto, 200);

    }

    public async Task<ResponseDto<NoDataDto>> RevokeRefreshToken(string refreshToken)
    {
        var exitsRefreshToken = await _userRefreshTokenService.GetWhere(x => x.Code == refreshToken).SingleOrDefaultAsync();

        if (exitsRefreshToken == null)
        {
            return ResponseDto<NoDataDto>.Fail("Refresh not found", 404, true);
        }

        _userRefreshTokenService.Remove(exitsRefreshToken);

        await _unitOfWork.CommitAsync();

        return ResponseDto<NoDataDto>.Success(200);

    }
}
