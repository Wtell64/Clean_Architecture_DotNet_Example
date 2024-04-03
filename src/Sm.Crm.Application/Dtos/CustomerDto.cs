using AutoMapper;
using Sm.Crm.Domain.Entities;
using Sm.Crm.Domain.Enums;

namespace Sm.Crm.Application.Dtos;

public class CustomerDto
{
    public long? Id { get; set; }
    public string? IdentityNumber { get; set; }
    public CustomerTypeEnum? CustomerType { get; set; }
    public string? CompanyName { get; set; }
    public string? BirthDate { get; set; }
    public int? StatusTypeId { get; set; }
    public string? StatusTypeName { get; set; }
    public int? TitleId { get; set; }
    public string? TitleName { get; set; }
    public int? TerritoryId { get; set; }
    public string? TerritoryName { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? FullName => FirstName + " " + LastName;
    public string? Email { get; set; }
    public GenderEnum? Gender { get; set; }

    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Customer, CustomerDto>()
                .ForMember(vm => vm.FirstName, m => m.MapFrom(u => u.UserFk != null ? u.UserFk.FirstName : "-"))
                .ForMember(vm => vm.LastName, m => m.MapFrom(u => u.UserFk != null ? u.UserFk.LastName : "-"))
                .ForMember(vm => vm.Email, m => m.MapFrom(u => u.UserFk != null ? u.UserFk.Email : "-"))
                .ForMember(vm => vm.Gender, m => m.MapFrom(u => u.UserFk != null ? u.UserFk.Gender : null))
                .ForMember(vm => vm.TitleName, m => m.MapFrom(u => u.TitleFk != null ? u.TitleFk.Name : null))
                .ForMember(vm => vm.StatusTypeName, m => m.MapFrom(u => u.StatusTypeFk != null ? u.StatusTypeFk.Name : null))
                .ForMember(vm => vm.TerritoryName, m => m.MapFrom(u => u.TerritoryFk != null ? u.TerritoryFk.Name : null))
                .ReverseMap();
        }
    }
}