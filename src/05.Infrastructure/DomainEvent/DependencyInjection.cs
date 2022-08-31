using Microsoft.Extensions.DependencyInjection;
using Zeta.CodebaseExpress.Application.Services.DomainEvent;

namespace Zeta.CodebaseExpress.Infrastructure.DomainEvent;

public static class DependencyInjection
{
    public static IServiceCollection AddDomainEventService(this IServiceCollection services)
    {
        services.AddScoped<IDomainEventService, DomainEventService>();

        return services;
    }
}
