using MediatR;
using Microsoft.Extensions.Logging;
using Zeta.CodebaseExpress.Application.Common.Extensions;

namespace Zeta.CodebaseExpress.Application.Common.Behaviours;

public class UnhandledExceptionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly ILogger<TRequest> _logger;

    public UnhandledExceptionBehaviour(ILogger<TRequest> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        try
        {
            return await next();
        }
        catch (Exception exception)
        {
            var requestName = typeof(TRequest).Name;
            var formattedRequest = request.ToPrettyJson();

            _logger.LogError(exception, "Unhandled Exception when executing {RequestName}.\n{RequestObject}",
               requestName, formattedRequest);

            throw;
        }
    }
}
