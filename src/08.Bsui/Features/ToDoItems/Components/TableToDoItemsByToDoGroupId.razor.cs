using Microsoft.AspNetCore.Components;
using MudBlazor;
using Zeta.CodebaseExpress.Bsui.Common.Components;
using Zeta.CodebaseExpress.Bsui.Common.Constants;
using Zeta.CodebaseExpress.Bsui.Common.Extensions;
using Zeta.CodebaseExpress.Shared.Common.Constants;
using Zeta.CodebaseExpress.Shared.Common.Responses;
using Zeta.CodebaseExpress.Shared.ToDoItems.Commands.UpdateToDoItem;
using Zeta.CodebaseExpress.Shared.ToDoItems.Commands.UpdateToDoItemStatus;
using Zeta.CodebaseExpress.Shared.ToDoItems.Constants;
using Zeta.CodebaseExpress.Shared.ToDoItems.Queries.GetToDoItemsByToDoGroupId;

namespace Zeta.CodebaseExpress.Bsui.Features.ToDoItems.Components;

public partial class TableToDoItemsByToDoGroupId
{
    [Parameter]
    public Guid ToDoGroupId { get; set; } = default!;

    private ErrorResponse? _error;
    private MudTable<GetToDoItemsByToDoGroupIdToDoItem> _tableToDoItems = new();
    private string? _searchKeyword;

    private async Task<TableData<GetToDoItemsByToDoGroupIdToDoItem>> ReloadTableToDoItems(TableState state)
    {
        _error = null;

        StateHasChanged();

        var tableData = new TableData<GetToDoItemsByToDoGroupIdToDoItem>();
        var request = state.ToPaginatedListRequest(_searchKeyword);
        var response = await _toDoItemService.GetToDoItemsByToDoGroupIdAsync(ToDoGroupId, request);

        _error = response.Error;

        StateHasChanged();

        if (response.Result is null)
        {
            return tableData;
        }

        return response.Result.ToTableData();
    }

    private async Task OnSearch(string keyword)
    {
        _searchKeyword = keyword.Trim();

        await _tableToDoItems.ReloadServerData();
    }

    private async Task ShowDialogCreate()
    {
        var parameters = new DialogParameters
        {
            { nameof(DialogCreate.ToDoGroupId), ToDoGroupId }
        };

        var dialog = _dialogService.Show<DialogCreate>($"{CommonDisplayTextFor.Create} {DisplayTextFor.ToDoItem}", parameters);
        var result = await dialog.Result;

        if (!result.Cancelled)
        {
            _snackbar.AddSuccess(SuccessMessageFor.Action(DisplayTextFor.ToDoItem, CommonDisplayTextFor.Created));

            await _tableToDoItems.ReloadServerData();
        }
    }

    private async Task ToggleIsDone(GetToDoItemsByToDoGroupIdToDoItem toDoItem)
    {
        var request = new UpdateToDoItemStatusRequest
        {
            ToDoItemId = toDoItem.Id,
            IsDone = !toDoItem.IsDone
        };

        var response = await _toDoItemService.UpdateToDoItemStatusAsync(request);

        if (response.Error is not null)
        {
            _error = response.Error;

            return;
        }

        _snackbar.AddSuccess(SuccessMessageFor.Action(DisplayTextFor.ToDoItem, DisplayTextFor.Status, CommonDisplayTextFor.Updated));

        await _tableToDoItems.ReloadServerData();
    }

    private void ShowDialogDetails(Guid toDoItemId)
    {
        var parameters = new DialogParameters
        {
            { nameof(DialogDetails.ToDoItemId), toDoItemId }
        };

        _dialogService.Show<DialogDetails>($"{CommonDisplayTextFor.View} {DisplayTextFor.ToDoItem}", parameters);
    }

    private async Task ShowDialogEdit(GetToDoItemsByToDoGroupIdToDoItem toDoItem)
    {
        var request = new UpdateToDoItemRequest
        {
            ToDoItemId = toDoItem.Id,
            Title = toDoItem.Title,
            Description = toDoItem.Description,
            PriorityLevel = toDoItem.PriorityLevel,
        };

        var parameters = new DialogParameters
        {
            { nameof(DialogEdit.Request), request }
        };

        var dialog = _dialogService.Show<DialogEdit>($"{CommonDisplayTextFor.Edit} {DisplayTextFor.ToDoItem}", parameters);
        var result = await dialog.Result;

        if (!result.Cancelled)
        {
            _snackbar.AddSuccess(SuccessMessageFor.Action(DisplayTextFor.ToDoItem, toDoItem.Title, CommonDisplayTextFor.Updated));

            await _tableToDoItems.ReloadServerData();
        }
    }

    private async Task ShowDialogDelete(GetToDoItemsByToDoGroupIdToDoItem toDoItem)
    {
        var dialog = _dialogService.Show<DialogDelete>($"{CommonDisplayTextFor.Delete} {toDoItem.Title}");
        var result = await dialog.Result;

        if (!result.Cancelled)
        {
            var response = await _toDoItemService.DeleteToDoItemAsync(toDoItem.Id);

            if (response.Error is not null)
            {
                _snackbar.AddErrors(response.Error.Details);

                return;
            }

            _snackbar.AddSuccess(SuccessMessageFor.Action(DisplayTextFor.ToDoItem, toDoItem.Title, CommonDisplayTextFor.Deleted));

            await _tableToDoItems.ReloadServerData();
        }
    }
}
