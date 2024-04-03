using MediatR;
using Serilog;
using Sm.Crm.Application.Common.Interfaces;
using System.Diagnostics;

namespace Sm.Crm.Application.Common.Behaviours;

public class PerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class
{
    private readonly ILogger _logger;
    private readonly Stopwatch _timer;
    private readonly IUser _user;
    private readonly IAccountService _accountService;

    public PerformanceBehaviour(IAppLogger logger, IUser user, IAccountService accountService)
    {
        _timer = new();
        _logger = logger.CreatePerformanceLogger();
        _user = user;
        _accountService = accountService;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        _timer.Start();

        var response = await next();

        _timer.Stop();

        var elapsedMilliseconds = _timer.ElapsedMilliseconds;
        var requestName = typeof(TRequest).Name;
        var userId = _user.Id ?? "Unknown";
        var userName = (await _accountService.GetUserNameAsync(_user.Id ?? "")) ?? "Unknown";

        if (elapsedMilliseconds > 100)
        {
            _logger.Error($"[Performance] Request: {requestName}, User: {userId}-{userName}, Time: {elapsedMilliseconds}");
        }

        return response;
    }
}