using AutoMapper;
using Sm.Crm.Application.Common.Mapping;
using Sm.Crm.Application.Dtos;
using Sm.Crm.Application.Features.UserAddresses.Commands.CreateUserAddresses;
using Sm.Crm.Application.Features.UserAddresses.Commands.UpdateUserAddresses;
using Sm.Crm.Domain.Entities;

namespace Sm.Crm.Application.Features.UserAddresses;
public class MappingProfile: Profile
{
    public MappingProfile()
    {
        CreateMap<UserAddress, CreateUserAddressesCommand>().ReverseMap();
        CreateMap<UserAddress, UpdateUserAdressesCommand>().ReverseMap();
        CreateMap<UserAddress, UserAddressDto>()
            .ForMember(vm => vm.UserFullName, m => m.MapFrom(u => u.UserFk != null ? u.UserFk.FirstName + u.UserFk.LastName : "-")).ReverseMap();
            

        CreateMap<string, DateOnly>().ConvertUsing(new DateTimeTypeConverter());
    }
}
