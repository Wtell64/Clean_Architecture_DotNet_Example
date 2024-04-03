using AutoMapper;
using Sm.Crm.Domain.Entities;

namespace Sm.Crm.Application.Dtos;

public class CreateOrEditDocumentDto
{
    public int? Id { get; set; }
    public string DocumentFileName { get; set; }
    public Guid UserId { get; set; }
    public int RequestId { get; set; }
    public int DocumentTypeId { get; set; }

    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Document, CreateOrEditDocumentDto>().ReverseMap();
        }
    }
}