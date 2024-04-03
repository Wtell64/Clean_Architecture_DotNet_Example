using AutoMapper;
using Sm.Crm.Domain.Entities;
using Sm.Crm.Domain.Enums;

namespace Sm.Crm.Application.Dtos;

public class CreateOrEditUserEmailDto
{
    public int? UserId { get; set; }
    public string? EmailAddress { get; set; }
    public EmailTypeEnum EmailType { get; set; }

    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<UserEmail, UserEmailDto>().ReverseMap();
        }
    }
}