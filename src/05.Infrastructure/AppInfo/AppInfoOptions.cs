﻿namespace Zeta.CodebaseExpress.Infrastructure.AppInfo;

public class AppInfoOptions
{
    public const string SectionKey = nameof(AppInfo);

    public string FullName { get; set; } = default!;
}
