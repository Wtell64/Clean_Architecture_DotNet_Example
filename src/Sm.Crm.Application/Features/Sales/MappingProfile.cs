using AutoMapper;
using Sm.Crm.Application.Common.Mapping;
using Sm.Crm.Application.Features.Sales.Commands.CreateSale;
using Sm.Crm.Application.Features.Sales.Commands.UpdateSale;
using Sm.Crm.Application.Features.Sales.Queries;
using Sm.Crm.Domain.Entities;

namespace Sm.Crm.Application.Features.Sales;

public class Mapping : Profile
{
    public Mapping()
    {
        CreateMap<Sale, SaleDto>().ReverseMap();
        CreateMap<Sale, CreateSaleCommand>().ReverseMap();
        CreateMap<Sale, UpdateSaleCommand>().ReverseMap();
    }
}