using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Debugging;
using Zeta.CodebaseExpress.Infrastructure.AppInfo;
using Zeta.CodebaseExpress.Shared.Common.Constants;
using Zeta.CodebaseExpress.Shared.Services.Logging.Constants;

namespace Zeta.CodebaseExpress.Infrastructure.Logging.Serilog;

public static class DependencyInjection
{
    public static IHostBuilder UseSerilogLoggingService(this IHostBuilder hostBuilder)
    {
        hostBuilder.UseSerilog((hostBuilderContext, loggerConfiguration) => loggerConfiguration.ConfigureSerilog(hostBuilderContext.Configuration));
        SelfLog.Enable(message => Console.WriteLine(message));

        return hostBuilder;
    }

    public static LoggerConfiguration ConfigureSerilog(this LoggerConfiguration loggerConfiguration, IConfiguration configuration)
    {
        var appInfoOptions = configuration.GetSection(AppInfoOptions.SectionKey).Get<AppInfoOptions>();

        return loggerConfiguration
            .ReadFrom.Configuration(configuration, sectionName: $"{nameof(Logging)}")
            .Enrich.WithProperty(PropertyNameFor.Version, CommonValueFor.EntryAssemblyVersion!)
            .Enrich.WithProperty(PropertyNameFor.ApplicationName, appInfoOptions.FullName)
            .Enrich.WithProperty(PropertyNameFor.AssemblyName, CommonValueFor.EntryAssemblySimpleName!)
            .Enrich.WithProperty(PropertyNameFor.EnvironmentName, CommonValueFor.EnvironmentName);
    }
}
