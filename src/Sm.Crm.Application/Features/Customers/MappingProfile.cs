using AutoMapper;
using Sm.Crm.Application.Common.Mapping;
using Sm.Crm.Application.Features.Customers.Commands.CreateCustomer;
using Sm.Crm.Application.Features.Customers.Commands.UpdateCustomer;
using Sm.Crm.Application.Features.Customers.Queries;
using Sm.Crm.Domain.Entities;

namespace Sm.Crm.Application.Features.Customers;

public class Mapping : Profile
{
    public Mapping()
    {
        CreateMap<Customer, CreateCustomerCommand>().ReverseMap();
        CreateMap<Customer, UpdateCustomerCommand>().ReverseMap();
        CreateMap<Customer, CustomerDto>()
            .ForMember(vm => vm.FirstName, m => m.MapFrom(u => u.UserFk != null ? u.UserFk.FirstName : "-"))
            .ForMember(vm => vm.LastName, m => m.MapFrom(u => u.UserFk != null ? u.UserFk.LastName : "-"))
            .ForMember(vm => vm.Email, m => m.MapFrom(u => u.UserFk != null ? u.UserFk.Email : "-"))
            .ForMember(vm => vm.Gender, m => m.MapFrom(u => u.UserFk != null ? u.UserFk.Gender : null))
            .ForMember(vm => vm.TitleName, m => m.MapFrom(u => u.TitleFk != null ? u.TitleFk.Name : null))
            .ForMember(vm => vm.StatusTypeName, m => m.MapFrom(u => u.StatusTypeFk != null ? u.StatusTypeFk.Name : null))
            .ReverseMap();

        CreateMap<string, DateOnly>().ConvertUsing(new DateTimeTypeConverter());
    }
}