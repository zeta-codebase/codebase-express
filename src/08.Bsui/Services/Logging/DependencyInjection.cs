using Zeta.CodebaseExpress.Bsui.Services.Logging.Serilog;

namespace Zeta.CodebaseExpress.Bsui.Services.Logging;

public static class DependencyInjection
{
    public static IHostBuilder UseLoggingService(this IHostBuilder hostBuilder)
    {
        hostBuilder.UseSerilogLoggingService();

        return hostBuilder;
    }
}
