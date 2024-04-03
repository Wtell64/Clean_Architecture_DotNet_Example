using AutoMapper;
using Sm.Crm.Domain.Entities;

namespace Sm.Crm.Application.Dtos;

public class DocumentDto
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
            CreateMap<Document, DocumentDto>().ReverseMap();
        }
    }
}