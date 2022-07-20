namespace Zeta.CodebaseExpress.WebApi.Services.Documentation;

public class DocumentationOptions
{
    public const string SectionKey = nameof(Documentation);

    public string SwaggerPrefix { get; set; } = default!;
    public string JsonEndpoint { get; set; } = default!;

    public string Description { get; set; } = default!;
    public string DescriptionMarkdownFile { get; set; } = default!;
    public string VersionName { get; set; } = default!;
}

