using AutoMapper;
using Sm.Crm.Application.Common.Mapping;
using Sm.Crm.Application.Features.RequestStatuses.Commands.CreateRequestStatus;
using Sm.Crm.Application.Features.RequestStatuses.Commands.UpdateRequestStatus;
using Sm.Crm.Application.Features.RequestStatuses.Queries;
using Sm.Crm.Application.Features.Titles.Commands.CreateTitle;
using Sm.Crm.Application.Features.Titles.Commands.UpdateTitle;
using Sm.Crm.Domain.Entities.LST;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sm.Crm.Application.Features.RequestStatuses;
public class Mapping : Profile
{
    public Mapping()
    {
        CreateMap<RequestStatus, CreateRequestStatusCommand>().ReverseMap();
        CreateMap<RequestStatus, UpdateRequestStatusCommand>().ReverseMap();
        CreateMap<RequestStatus, RequestStatusDto>();
        CreateMap<string, DateOnly>().ConvertUsing(new DateTimeTypeConverter());
    }
}
