using BestPracticesJWT.Core.Dtos;
using BestPracticesJWT.Core.Entities;
using BestPracticesJWT.Core.Interfaces.Services;
using BestPracticesJWT.Service.Mappers;
using BestPracticesJWT.SharedCommons.Dtos;
using Microsoft.AspNetCore.Identity;

namespace BestPracticesJWT.Service.Services;

public class UserService : IUserService
{
    private readonly UserManager<AppUser> _userManager;

    public UserService(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<ResponseDto<AppUserDto>> CreateUserAsync(CreateUserDto createUserDto)
    {
        var user = new AppUser
        {
            Email = createUserDto.Email,
            UserName = createUserDto.UserName,
        };


        var result = await _userManager.CreateAsync(user, createUserDto.Password);

        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(x => x.Description).ToList();
            return ResponseDto<AppUserDto>.Fail(new ErrorDto(errors,true), 400);

        }
        return ResponseDto<AppUserDto>.Success(ObjectMapper.Mapper.Map<AppUserDto>(user), 200);
    }

    public async Task<ResponseDto<AppUserDto>> GetUserByNameAsync(string userName)
    {
        var user = await _userManager.FindByNameAsync(userName);
    
        if (user == null) return ResponseDto<AppUserDto>.Fail("User not found", 404, true);

        return ResponseDto<AppUserDto>.Success(ObjectMapper.Mapper.Map<AppUserDto>(user), 200);

    }
}
