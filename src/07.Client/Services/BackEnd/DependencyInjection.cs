using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Zeta.CodebaseExpress.Client.Services.BackEnd;

public static class DependencyInjection
{
    public static IServiceCollection AddBackEndService(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<BackEndOptions>(configuration.GetSection(BackEndOptions.SectionKey));

        services.AddTransient<ToDoGroupService>();
        services.AddTransient<ToDoItemService>();

        return services;
    }
}
