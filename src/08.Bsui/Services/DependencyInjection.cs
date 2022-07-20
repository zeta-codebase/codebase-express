using Zeta.CodebaseExpress.Bsui.Services.AppInfo;
using Zeta.CodebaseExpress.Bsui.Services.UI;

namespace Zeta.CodebaseExpress.Bsui.Services;

public static class DependencyInjection
{
    public static IServiceCollection AddBsui(this IServiceCollection services, IConfiguration configuration)
    {
        #region AppInfo
        services.AddAppInfoService(configuration);
        #endregion AppInfo

        #region User Interface
        services.AddUIService();
        #endregion User Interface

        return services;
    }
}
