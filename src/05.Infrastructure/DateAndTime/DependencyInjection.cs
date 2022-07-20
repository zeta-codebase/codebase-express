using Microsoft.Extensions.DependencyInjection;
using Zeta.CodebaseExpress.Application.Services.DateAndTime;

namespace Zeta.CodebaseExpress.Infrastructure.DateAndTime;

public static class DependencyInjection
{
    public static IServiceCollection AddDateAndTimeService(this IServiceCollection services)
    {
        services.AddTransient<IDateAndTimeService, DateAndTimeService>();

        return services;
    }
}
