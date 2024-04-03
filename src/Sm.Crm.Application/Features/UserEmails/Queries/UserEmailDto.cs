using Sm.Crm.Domain.Enums;

namespace Sm.Crm.Application.Features.UserEmails.Queries;

public class UserEmailDto
{
    public long? Id { get; set; }
    public string? EmailAddress { get; set; }
    public EmailTypeEnum EmailType { get; set; }
    public string? UserFullName { get; set; }
}