using AutoMapper;
using Sm.Crm.Application.Common.Mapping;
using Sm.Crm.Application.Features.Customers.Commands.CreateCustomer;
using Sm.Crm.Application.Features.Customers.Commands.UpdateCustomer;
using Sm.Crm.Application.Features.Customers.Queries;
using Sm.Crm.Application.Features.UserEmails.Commands.UpdateEmails;
using Sm.Crm.Application.Features.UserEmails.Queries;
using Sm.Crm.Domain.Entities;

namespace Sm.Crm.Application.Features.UserEmails;

public class Mapping : Profile
{
    public Mapping()
    {
        CreateMap<UserEmail, CreateUserEmailCommand>().ReverseMap();
        CreateMap<UserEmail, UpdateUserEmailCommand>().ReverseMap();
        CreateMap<UserEmail, UserEmailDto>()
            .ForMember(vm => vm.UserFullName, m => m.MapFrom(u => u.UserFk != null ? u.UserFk.FirstName + " " + u.UserFk.LastName : "-"))
            .ReverseMap();

        CreateMap<string, DateOnly>().ConvertUsing(new DateTimeTypeConverter());
    }
}