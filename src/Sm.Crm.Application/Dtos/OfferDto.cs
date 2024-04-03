using AutoMapper;
using Sm.Crm.Domain.Entities;

namespace Sm.Crm.Application.Dtos;

public class OfferDto
{
    public int EmployeeUserId { get; set; }
    public DateTime OfferDate { get; set; }
    public decimal BidAmount { get; set; }
    public int OfferStatusId { get; set; }

    public int RequestId { get; set; }

    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Offer, OfferDto>().ReverseMap();
        }
    }
}