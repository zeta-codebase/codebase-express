using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using Zeta.CodebaseExpress.Application.Common.Extensions;

namespace Zeta.CodebaseExpress.Application.Common.Behaviours;

public class LoggingBehaviour<TRequest> : IRequestPreProcessor<TRequest> where TRequest : notnull
{
    private readonly ILogger _logger;

    public LoggingBehaviour(ILogger<TRequest> logger)
    {
        _logger = logger;
    }

    public Task Process(TRequest request, CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        var formattedRequest = request.ToPrettyJson();

        _logger.LogInformation("Processing {RequestName}\n{RequestObject}",
           requestName, formattedRequest);

        return Task.CompletedTask;
    }
}
