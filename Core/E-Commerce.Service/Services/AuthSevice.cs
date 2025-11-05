global using E_Commerce.Domain.Entities.Auth;
global using E_Commerce.Service.Contracts;
global using E_Commerce.ServiceAbstraction;
global using E_Commerce.ServiceAbstraction.Common;
global using Microsoft.AspNetCore.Identity;
global using E_Commerce.Shared.DataTransferObjects.Auth;


namespace E_Commerce.Service;
public class AuthSevice(UserManager<ApplicationUser> userManager,
    ITokenService tokenService)
    : IAuthService
{
    public async Task<Result<UserResponse>> LoginAsync(LoginRequest request)
    {
        var user = await userManager.FindByEmailAsync(request.Email);
        if (user == null)
            return Error.Unauthorized(description: "Invalid Email or password");
        
        var result = await userManager.CheckPasswordAsync(user, request.Password);
        if (!result)
            return Error.Unauthorized(description: "Invalid Email or password");

        var roles = await userManager.GetRolesAsync(user);
        var token = tokenService.GetToken(user, roles);
        return new UserResponse(user.Email,user.DisplayName, token);
    }

    public async Task<Result<UserResponse>> RegisterAsync(RegisterRequest request)
    {
        var user = new ApplicationUser
        {
            Email = request.Email,
            DisplayName = request.DisplayName,
            UserName = request.UserName,
            PhoneNumber = request.PhoneNumber
        };
        var result = await userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
            return result.Errors.Select(x => Error.Validation(x.Code, x.Description)).ToList();

        var token = tokenService.GetToken(user, []);
        return new UserResponse(user.Email, user.DisplayName, token);
        

    }
}
