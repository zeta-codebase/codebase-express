using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Zeta.CodebaseExpress.Shared;

public static class DependencyInjection
{
    public static IServiceCollection AddShared(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;
    }
}
