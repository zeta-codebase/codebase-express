using Zeta.CodebaseExpress.WebApi.Services.Documentation;

namespace Zeta.CodebaseExpress.WebApi.Services;

public static class DependencyInjection
{
    public static IServiceCollection AddWebApi(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDocumentationService(configuration);

        return services;
    }

    public static IApplicationBuilder UseWebApi(this IApplicationBuilder app, IConfiguration configuration)
    {
        app.UseDocumentationService(configuration);

        return app;
    }
}
