using AutoMapper;
using Sm.Crm.Domain.Entities;
using Sm.Crm.Domain.Enums;

namespace Sm.Crm.Application.Dtos;

public class UserAddressDto
{
    public int? Id { get; set; }
    public Guid? UserId { get; set; }   
    public string? Address { get; set; }
    public string? Country { get; set; }
    public string? City { get; set; }
    public AddressTypeEnum? AddressType { get; set; }
    public string AddressTypeName { get; set; }
    public string UserFullName { get; set; }

    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<UserAddress, UserAddressDto>()
                .ForMember(vm => vm.UserFullName, m => m.MapFrom(u => u.UserFk != null ? u.UserFk.FirstName + " " + u.UserFk.LastName : "-"))
                .ForMember(at => at.AddressTypeName, n => n.MapFrom(u => u.AddressType != null ? u.AddressType.Value.ToString() : "-"))
                .ReverseMap();
        }
    }
}