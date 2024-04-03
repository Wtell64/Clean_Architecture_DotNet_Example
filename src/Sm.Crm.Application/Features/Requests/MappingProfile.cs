using AutoMapper;
using Sm.Crm.Application.Common.Mapping;
using Sm.Crm.Application.Features.Requests.Commands.CreateRequest;
using Sm.Crm.Application.Features.Requests.Commands.UpdateRequest;
using Sm.Crm.Application.Features.Requests.Queries;
using Sm.Crm.Domain.Entities;

namespace Sm.Crm.Application.Features.Requests;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Request, CreateRequestCommand>().ReverseMap();
        CreateMap<Request, UpdateRequestCommand>().ReverseMap();
        CreateMap<Request, RequestDto>()
            .ForMember(
               destinationMember: r => r.CustomerFirstName,
               memberOptions: c => c.MapFrom(c => c.CustomerFk != null && c.CustomerFk.UserFk != null && c.CustomerFk.UserFk.FirstName != null ? c.CustomerFk.UserFk.FirstName : "-"))
            .ForMember(
               destinationMember: r => r.CustomerLastName,
               memberOptions: c => c.MapFrom(c => c.CustomerFk != null && c.CustomerFk.UserFk != null && c.CustomerFk.UserFk.LastName != null ? c.CustomerFk.UserFk.LastName : "-"))
            .ForMember(
               destinationMember: r => r.CompanyName,
               memberOptions: c => c.MapFrom(c => c.CustomerFk != null && c.CustomerFk.CompanyName != null ? c.CustomerFk.CompanyName : "-"))
            .ForMember(
               destinationMember: r => r.EmployeeFirstName,
               memberOptions: c => c.MapFrom(c => c.EmployeeFk != null && c.EmployeeFk.UserFk != null && c.EmployeeFk.UserFk.FirstName != null ? c.EmployeeFk.UserFk.FirstName : "-"))
            .ForMember(
               destinationMember: r => r.EmployeeLastName,
               memberOptions: c => c.MapFrom(c => c.EmployeeFk != null && c.EmployeeFk.UserFk != null && c.EmployeeFk.UserFk.LastName != null ? c.EmployeeFk.UserFk.LastName : "-"))
            .ForMember(
               destinationMember: r => r.DepartmentName,
               memberOptions: c => c.MapFrom(c => c.EmployeeFk != null && c.EmployeeFk.DepartmentFk != null && c.EmployeeFk.DepartmentFk.Name != null ? c.EmployeeFk.DepartmentFk.Name : "-"))
            .ForMember(
               destinationMember: r => r.RequestStatusName,
               memberOptions: c => c.MapFrom(c => c.RequestStatusFk != null && c.RequestStatusFk.Name != null ? c.RequestStatusFk.Name : "-"))
            .ReverseMap();
        CreateMap<string, DateOnly>().ConvertUsing(new DateTimeTypeConverter());
    }
}