using E_Commerce.ServiceAbstraction;
using E_Commerce.Shared.DataTransferObjects.Auth;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace E_Commerce.Presentation.ApI.Controllers;
public class AuthController(IAuthService authSevice)
    :APIBaseController
{
    [HttpPost("Register")]
    public async Task<ActionResult<UserResponse>> Register(RegisterRequest request) { 
    var result = await authSevice.RegisterAsync(request);
        return HandelResult(result);
    }

    [HttpPost("Login")]
    public async Task<ActionResult<UserResponse>> Login(LoginRequest request)
    {
        var result = await authSevice.LoginAsync(request);
        return HandelResult(result);
    }

    // check if email exist => Client side Validation
    [HttpGet("CheckEmail")]
    public async Task<ActionResult<bool>> CheckEmail (string email)
    {
        return Ok(await authSevice.CheckEmailAsync(email));
    }
}
