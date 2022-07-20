using Microsoft.AspNetCore.Components;
using MudBlazor;
using Zeta.CodebaseExpress.Bsui.Common.Components;
using Zeta.CodebaseExpress.Bsui.Common.Constants;
using Zeta.CodebaseExpress.Bsui.Common.Extensions;
using Zeta.CodebaseExpress.Bsui.Features.ToDoGroups.Components;
using Zeta.CodebaseExpress.Bsui.Features.ToDoGroups.Constants;
using Zeta.CodebaseExpress.Shared.Common.Constants;
using Zeta.CodebaseExpress.Shared.Common.Responses;
using Zeta.CodebaseExpress.Shared.ToDoGroups.Commands.UpdateToDoGroup;
using Zeta.CodebaseExpress.Shared.ToDoGroups.Constants;
using Zeta.CodebaseExpress.Shared.ToDoGroups.Queries.GetToDoGroup;

namespace Zeta.CodebaseExpress.Bsui.Features.ToDoGroups;

public partial class Details
{
    [Parameter]
    public Guid ToDoGroupId { get; set; }

    private bool _isLoading;
    private ErrorResponse? _error;
    private string _browserTabTitle = DisplayTextFor.ToDoGroup;
    private List<BreadcrumbItem> _breadcrumbItems = new();
    private GetToDoGroupResponse _toDoGroup = default!;

    protected override async Task OnParametersSetAsync()
    {
        await Reload();
    }

    private async Task Reload()
    {
        SetupBreadcrumb();

        await GetToDoGroup();

        if (_toDoGroup is null)
        {
            _breadcrumbItems.Add(CommonBreadcrumbFor.Active(CommonDisplayTextFor.Error));

            return;
        }

        _browserTabTitle = _toDoGroup.Name;
        _breadcrumbItems.Add(CommonBreadcrumbFor.Active(_toDoGroup.Name));
    }

    private void SetupBreadcrumb()
    {
        _breadcrumbItems = new()
        {
            CommonBreadcrumbFor.Home,
            BreadcrumbFor.Index
        };
    }

    private async Task GetToDoGroup()
    {
        _isLoading = true;

        var response = await _toDoGroupService.GetToDoGroupAsync(ToDoGroupId);

        _isLoading = false;

        if (response.Error is not null)
        {
            _error = response.Error;

            return;
        }

        _toDoGroup = response.Result!;
    }

    private async Task ShowDialogEdit()
    {
        var request = new UpdateToDoGroupRequest
        {
            ToDoGroupId = _toDoGroup.Id,
            Name = _toDoGroup.Name,
        };

        var parameters = new DialogParameters
        {
            { nameof(DialogEdit.Request), request }
        };

        var dialog = _dialogService.Show<DialogEdit>($"{CommonDisplayTextFor.Edit} {DisplayTextFor.ToDoGroup}", parameters);
        var result = await dialog.Result;

        if (!result.Cancelled)
        {
            _snackbar.AddSuccess(SuccessMessageFor.Action(DisplayTextFor.ToDoGroup, _toDoGroup.Name, CommonDisplayTextFor.Updated));

            await Reload();
        }
    }

    private async Task ShowDialogDelete()
    {
        var dialog = _dialogService.Show<DialogDelete>($"{CommonDisplayTextFor.Delete} {_toDoGroup.Name}");
        var result = await dialog.Result;

        if (!result.Cancelled)
        {
            var response = await _toDoGroupService.DeleteToDoGroupAsync(_toDoGroup.Id);

            if (response.Error is not null)
            {
                _snackbar.AddErrors(response.Error.Details);

                return;
            }

            _snackbar.AddSuccess(SuccessMessageFor.Action(DisplayTextFor.ToDoGroup, _toDoGroup.Name, CommonDisplayTextFor.Deleted));
            _navigationManager.NavigateTo(RouteFor.Index);
        }
    }
}
