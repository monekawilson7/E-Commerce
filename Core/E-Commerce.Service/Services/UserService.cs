using AutoMapper;
using E_Commerce.Shared.DataTransferObjects.Users;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Service.Services;
internal class UserService (UserManager<ApplicationUser> userManager, 
    ITokenService tokenService, 
    IMapper mapper)
    : IUserService
{
    public async Task<Result<AddressDTO>> GetAddressAsync(string email)
    {
        var user = await userManager.Users.Include(u => u.Address)
            .FirstOrDefaultAsync(x => x.Email == email);
        if (user == null)
            return Error.NotFound("User Not Found.", $"User With Email {email} was not found");
        if (user.Address == null)
            return Error.NotFound("Address Not Found.", $"User With Email {email} doesn't have address");

        return mapper.Map<AddressDTO>(user.Address);
    }

    public async Task<Result<UserResponse>> GetByEmailAsync(string email)
    {
       var user = await userManager.FindByEmailAsync(email);
        if (user == null) 
            return Error.NotFound("User Not Found.", $"User With Email {email} was not found");
        var roles = await userManager.GetRolesAsync(user);

        return new UserResponse(user.Email, user.DisplayName, tokenService.GetToken(user, roles));
    }

    public async Task<Result<AddressDTO>> UpdateAddressAsync(string email, AddressDTO address)
    {
        var user = await userManager.Users.Include(u => u.Address)
    .FirstOrDefaultAsync(x => x.Email == email);
        if (user == null)
            return Error.NotFound("User Not Found.", $"User With Email {email} was not found");
        if (user.Address is not null)
        {
            user.Address.FristName = address.FristName;
            user.Address.LastName = address.LastName;
            user.Address.City = address.City;
            user.Address.Country = address.Country;
            user.Address.Street = address.Street;
        }
        else
        { 
            user.Address = mapper.Map<Address>(address);
        }

        await userManager.UpdateAsync(user);
        return mapper.Map<AddressDTO>(user.Address);
    }
}
