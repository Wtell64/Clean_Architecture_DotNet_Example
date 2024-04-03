using AutoMapper;
using Sm.Crm.Domain.Entities;
using Sm.Crm.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Sm.Crm.Application.Dtos;

public class CreateOrUpdateCustomerDto
{
    public long? Id { get; set; }
    public string? IdentityNumber { get; set; }
    public CustomerTypeEnum? CustomerType { get; set; }

    [Required]
    public string? CompanyName { get; set; }

    public string? BirthDate { get; set; }
    public int? StatusTypeId { get; set; }
    public int? TitleId { get; set; }
    public int? TerritoryId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public GenderEnum? Gender { get; set; }

    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Customer, CreateOrUpdateCustomerDto>().ReverseMap();
        }
    }
}