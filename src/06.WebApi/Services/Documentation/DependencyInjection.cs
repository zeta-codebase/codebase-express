using Zeta.CodebaseExpress.WebApi.Services.Documentation.Swagger;

namespace Zeta.CodebaseExpress.WebApi.Services.Documentation;

public static class DependencyInjection
{
    public static IServiceCollection AddDocumentationService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSwaggerDocumentationService(configuration);

        return services;
    }

    public static IApplicationBuilder UseDocumentationService(this IApplicationBuilder app, IConfiguration configuration)
    {
        app.UseSwaggerDocumentationService(configuration);

        return app;
    }
}
