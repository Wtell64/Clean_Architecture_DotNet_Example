using AutoMapper;
using Sm.Crm.Application.Common.Mapping;
using Sm.Crm.Application.Features.DocumentTypes.Commands.CreateDocumentType;
using Sm.Crm.Application.Features.DocumentTypes.Commands.UpdateDocumentType;
using Sm.Crm.Application.Features.DocumentTypes.Queries;
using Sm.Crm.Application.Features.Titles.Commands.CreateTitle;
using Sm.Crm.Application.Features.Titles.Commands.UpdateTitle;
using Sm.Crm.Domain.Entities.LST;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sm.Crm.Application.Features.DocumentTypes;
public class Mapping : Profile
{
    public Mapping() 
    {
        CreateMap<DocumentType, CreateDocumentTypeCommand>().ReverseMap();
        CreateMap<DocumentType, UpdateDocumentTypeCommand>().ReverseMap();
        CreateMap<DocumentType, DocumentTypeDto>();
        CreateMap<string, DateOnly>().ConvertUsing(new DateTimeTypeConverter());
    }
}
