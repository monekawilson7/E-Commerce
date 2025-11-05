namespace E_Commerce.Domain.Entities.Auth;
public class Address
{
    public ApplicationUser user { get; set; }
    public string userId { get; set; }
    public int Id { get; set; }
    public string FristName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string City { get; set; } = default!;
    public string Country { get; set; } = default!;
    public string Street { get; set; } = default!;
}
