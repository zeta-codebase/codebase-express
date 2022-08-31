using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Zeta.CodebaseExpress.Infrastructure.AppInfo;
using Zeta.CodebaseExpress.Infrastructure.DateAndTime;
using Zeta.CodebaseExpress.Infrastructure.DomainEvent;
using Zeta.CodebaseExpress.Infrastructure.Persistence;

namespace Zeta.CodebaseExpress.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAppInfoService(configuration);
        services.AddDateAndTimeService();
        services.AddDomainEventService();
        services.AddPersistenceService(configuration);

        return services;
    }
}
