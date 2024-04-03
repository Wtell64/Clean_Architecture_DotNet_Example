using Sm.Crm.Domain.Enums;

namespace Sm.Crm.Application.Features.Customers.Queries;

public class CustomerDto
{
    public long? Id { get; set; }
    public string? IdentityNumber { get; set; }
    public CustomerTypeEnum? CustomerType { get; set; }
    public string? CompanyName { get; set; }
    public string? BirthDate { get; set; }

    public int? StatusTypeId { get; set; }
    public string? StatusTypeName { get; set; }

    public int? TitleId { get; set; }
    public string? TitleName { get; set; }

    public int? TerritoryId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? FullName => FirstName + " " + LastName;
    public string? Email { get; set; }
    public GenderEnum? Gender { get; set; }
}