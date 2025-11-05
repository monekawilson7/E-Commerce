using E_Commerce.ServiceAbstraction;
using E_Commerce.Shared.DataTransferObjects.Auth;
using E_Commerce.Shared.DataTransferObjects.Users;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;

namespace E_Commerce.Presentation.ApI.Controllers;
[Authorize]
public class UsersController(IUserService userService)
    : APIBaseController
{
    [HttpGet] // Get Current User
    public async Task<ActionResult<UserResponse>> GetUser()
    {
        var email = User.FindFirstValue(ClaimTypes.Email);
        var result = await userService.GetByEmailAsync(email);
        return HandelResult(result);
    }

    [HttpGet("Address")]
    public async Task<ActionResult<AddressDTO>> GetAddress()
    {
        var email = User.FindFirstValue(ClaimTypes.Email);
        var result = await userService.GetAddressAsync(email);
        return HandelResult(result);
    }
    [HttpPut("Address")]
    public async Task<ActionResult<AddressDTO>> UpdateAddress (AddressDTO address)
    {
        var email = User.FindFirstValue(ClaimTypes.Email);
        var result = await userService.UpdateAddressAsync(email, address);
        return HandelResult(result);
    }
}
