using BestPracticesJWT.Core.Dtos;
using BestPracticesJWT.SharedCommons.Dtos;

namespace BestPracticesJWT.Core.Interfaces.Services;

public interface IUserService
{
    Task<ResponseDto<AppUserDto>> CreateUserAsync(CreateUserDto createUserDto);
    Task<ResponseDto<AppUserDto>> GetUserByNameAsync(string userName);
}
