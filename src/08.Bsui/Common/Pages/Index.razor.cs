using MudBlazor;
using Zeta.CodebaseExpress.Bsui.Common.Constants;
using Zeta.CodebaseExpress.Shared.Common.Constants;
using Zeta.CodebaseExpress.Shared.Common.Extensions;

namespace Zeta.CodebaseExpress.Bsui.Common.Pages;

public partial class Index
{
    private List<BreadcrumbItem> _breadcrumbItems = new();
    private string _greetings = default!;

    protected override void OnInitialized()
    {
        SetupBreadcrumb();

        _greetings = $"Good {DateTimeOffset.Now.ToFriendlyTimeDisplayText()}";
    }

    private void SetupBreadcrumb()
    {
        _breadcrumbItems = new()
        {
            CommonBreadcrumbFor.Active(CommonDisplayTextFor.Home)
        };
    }
}
