using AutoMapper;
using Sm.Crm.Domain.Entities;

namespace Sm.Crm.Application.Dtos;

public class CreateOrEditSaleDto
{
    public int Id { get; set; }
    public int RequestId { get; set; }
    public Guid EmployeeUserId { get; set; }
    public DateTime SaleDate { get; set; }
    public decimal SaleAmount { get; set; }
    public string Description { get; set; }

    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Sale, CreateOrEditSaleDto>().ReverseMap();
        }
    }
}