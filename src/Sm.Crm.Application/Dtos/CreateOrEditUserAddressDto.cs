using AutoMapper;
using Sm.Crm.Domain.Entities;
using Sm.Crm.Domain.Enums;

namespace Sm.Crm.Application.Dtos;

public class CreateOrEditUserAddressDto
{
    public int? Id { get; set; }
    public Guid? UserId { get; set; }
    public string? Address { get; set; }
    public string? Country { get; set; }
    public string? City { get; set; }
    public AddressTypeEnum? AddressType { get; set; }

    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<UserAddress, CreateOrEditUserAddressDto>().ReverseMap();
        }
    }
}