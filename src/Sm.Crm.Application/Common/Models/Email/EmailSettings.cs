namespace Sm.Crm.Application.Common.Models.Email;

public class EmailSettings
{
    public const string SectionName = "Email";

    public string? Host { get; set; }
    public int Port { get; set; }
    public bool UseSSL { get; set; }
    public bool UseStartTls { get; set; }
    public string? FromName { get; set; }
    public string? From { get; set; }
    public string? UserName { get; set; }
    public string? Password { get; set; }
}