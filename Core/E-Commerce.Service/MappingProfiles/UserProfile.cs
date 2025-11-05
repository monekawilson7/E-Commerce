using AutoMapper;
using E_Commerce.Shared.DataTransferObjects.Users;

namespace E_Commerce.Service.MappingProfiles;
internal class UserProfile
    : Profile
{
    public UserProfile()
    {
        CreateMap<Address, AddressDTO>().ReverseMap();
    }
}
