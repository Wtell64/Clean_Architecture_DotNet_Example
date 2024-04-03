using AutoMapper;

namespace Sm.Crm.Application.Common.Mapping;

public class DateTimeTypeConverter : ITypeConverter<string, DateOnly>
{
    public DateOnly Convert(string source, DateOnly destination, ResolutionContext context)
    {
        var d = System.Convert.ToDateTime(source);
        return DateOnly.FromDateTime(d);
    }
}