using Microsoft.AspNetCore.Identity;

namespace E_Commerce.Domain.Entities.Auth;
public class ApplicationUser : IdentityUser
{
    public string? FristName { get; set; }
    public string? LastName { get; set; }
    public string? DisplayName { get; set; }
    public Address Address { get; set; }
}
