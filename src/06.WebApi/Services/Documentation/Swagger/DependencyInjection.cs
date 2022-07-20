using System.Reflection;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using Zeta.CodebaseExpress.Infrastructure.AppInfo;
using Zeta.CodebaseExpress.Shared.Common.Constants;
using Zeta.CodebaseExpress.Shared.Common.Extensions;

namespace Zeta.CodebaseExpress.WebApi.Services.Documentation.Swagger;

public static class DependencyInjection
{
    public static IServiceCollection AddSwaggerDocumentationService(this IServiceCollection services, IConfiguration configuration)
    {
        var appInfoOptions = configuration.GetSection(AppInfoOptions.SectionKey).Get<AppInfoOptions>();
        var documentationOptions = configuration.GetSection(DocumentationOptions.SectionKey).Get<DocumentationOptions>();
        var description = documentationOptions.Description;

        if (!string.IsNullOrWhiteSpace(documentationOptions.DescriptionMarkdownFile))
        {
            var markdownContent = File.ReadAllText(Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                nameof(Services),
                nameof(Documentation),
                documentationOptions.DescriptionMarkdownFile));

            description = markdownContent
                    .Replace("{EnvironmentName}", CommonValueFor.EnvironmentName)
                    .Replace("{OperatingSystem}", CommonValueFor.OperatingSystemDescription)
                    .Replace("{AspNetVersion}", CommonValueFor.EntryAssemblyFrameworkName)
                    .Replace("{SemanticVersion}", CommonValueFor.EntryAssemblyInformationalVersion)
                    .Replace("{AppAssemblyLastBuildDate}", CommonValueFor.EntryAssemblyLastBuild.ToLongDateTimeDisplayText());
        }

        var executingAssembly = Assembly.GetExecutingAssembly();
        var executingAssemblyLastBuild = File.GetLastWriteTime(executingAssembly.Location);

        services.AddSwaggerGen(setupAction =>
        {
            setupAction.SwaggerDoc(documentationOptions.VersionName, new OpenApiInfo
            {
                Title = appInfoOptions.FullName,
                Version = "Version 1.0",
                Description = description,
            });

            setupAction.CustomOperationIds(d => (d.ActionDescriptor as ControllerActionDescriptor)?.ActionName);
            setupAction.CustomSchemaIds(DefaultSchemaIdSelector);
        });

        return services;
    }

    public static IApplicationBuilder UseSwaggerDocumentationService(this IApplicationBuilder app, IConfiguration configuration)
    {
        var appInfoOptions = configuration.GetSection(AppInfoOptions.SectionKey).Get<AppInfoOptions>();
        var documentationOptions = configuration.GetSection(DocumentationOptions.SectionKey).Get<DocumentationOptions>();

        app.UseSwagger();

        app.UseSwaggerUI(options =>
        {
            var jsonEndpoint = documentationOptions.JsonEndpoint.Replace("[version]", documentationOptions.VersionName);

            options.SwaggerEndpoint(jsonEndpoint, "CodebaseExpress API v1.0");
            options.RoutePrefix = documentationOptions.SwaggerPrefix;
            options.DocumentTitle = appInfoOptions.FullName;
            options.DefaultModelExpandDepth(2);
            options.DefaultModelRendering(ModelRendering.Model);
            options.DefaultModelsExpandDepth(-1);
            options.DisplayOperationId();
            options.DisplayRequestDuration();
            options.DocExpansion(DocExpansion.None);
            options.EnableDeepLinking();
            options.EnableValidator();
            options.ShowExtensions();
        });

        return app;
    }

    private static string DefaultSchemaIdSelector(Type modelType)
    {
        if (!modelType.IsConstructedGenericType)
        {
            return modelType.Name;
        }

        var suffix = modelType.GetGenericArguments()
            .Select(DefaultSchemaIdSelector)
            .Aggregate((previous, current) => previous + current);

        return $"{modelType.Name.Split('`').First()}Of{suffix}";
    }
}
