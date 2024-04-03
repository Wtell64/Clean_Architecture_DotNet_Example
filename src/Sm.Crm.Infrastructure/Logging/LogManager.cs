using Microsoft.Extensions.Configuration;
using Serilog;
using Sm.Crm.Application.Common.Interfaces;

namespace Sm.Crm.Infrastructure.Logging;

public class LogManager : IAppLogger
{
    private readonly IConfiguration _configuration;

    public LogManager(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public ILogger CreatePerformanceLogger()
    {
        return new LoggerConfiguration()
            .Enrich.FromLogContext()
            .MinimumLevel.Debug()
            .MinimumLevel.Override("Microsoft.EntityFrameworkCore.Database.Command", Serilog.Events.LogEventLevel.Warning)
            .WriteTo.File("logs/crm-performance-log.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();
    }

    public ILogger CreateMongoLogger()
    {
        if (_configuration["App:IsMongoActive"] == "true")
        {
            return new LoggerConfiguration()
                .Enrich.FromLogContext()
                .MinimumLevel.Warning()
                .MinimumLevel.Override("Microsoft.EntityFrameworkCore.Database.Command", Serilog.Events.LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.AspNetCore", Serilog.Events.LogEventLevel.Warning)
                .WriteTo.MongoDBBson(
                    _configuration["MongoDbSettings:ConnectionString"] + "/" + _configuration["MongoDbSettings:DatabaseName"],
                    _configuration["MongoDbSettings:LogCollection"])
                .CreateLogger();
        }

        return Log.Logger;
    }
}
