using Serilog;

namespace Sm.Crm.Application.Common.Interfaces;

public interface IAppLogger
{
    ILogger CreateMongoLogger();
    ILogger CreatePerformanceLogger();
}
