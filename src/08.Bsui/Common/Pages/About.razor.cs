using MudBlazor;
using Zeta.CodebaseExpress.Bsui.Common.Constants;
using Zeta.CodebaseExpress.Shared.Common.Constants;

namespace Zeta.CodebaseExpress.Bsui.Common.Pages;

public partial class About
{
    private List<BreadcrumbItem> _breadcrumbItems = new();

    protected override void OnInitialized()
    {
        SetupBreadcrumb();
    }

    private void SetupBreadcrumb()
    {
        _breadcrumbItems = new()
        {
            CommonBreadcrumbFor.Home,
            CommonBreadcrumbFor.Active(CommonDisplayTextFor.About)
        };
    }
}
