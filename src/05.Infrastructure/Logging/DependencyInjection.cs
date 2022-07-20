using Microsoft.Extensions.Hosting;
using Zeta.CodebaseExpress.Infrastructure.Logging.Serilog;

namespace Zeta.CodebaseExpress.Infrastructure.Logging;

public static class DependencyInjection
{
    public static IHostBuilder UseLoggingService(this IHostBuilder hostBuilder)
    {
        hostBuilder.UseSerilogLoggingService();

        return hostBuilder;
    }
}
