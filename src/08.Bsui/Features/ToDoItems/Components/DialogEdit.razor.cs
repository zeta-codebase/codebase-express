using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using Zeta.CodebaseExpress.Bsui.Common.Extensions;
using Zeta.CodebaseExpress.Shared.Common.Responses;
using Zeta.CodebaseExpress.Shared.ToDoItems.Commands.UpdateToDoItem;

namespace Zeta.CodebaseExpress.Bsui.Features.ToDoItems.Components;

public partial class DialogEdit
{
    [CascadingParameter]
    private MudDialogInstance MudDialog { get; set; } = default!;

    [Parameter]
    public UpdateToDoItemRequest Request { get; set; } = default!;

    private bool _isLoading;
    private ErrorResponse? _error;

    private void Cancel()
    {
        MudDialog.Cancel();
    }

    private async Task OnValidSubmit()
    {
        _error = null;

        _isLoading = true;

        var response = await _toDoItemService.UpdateToDoItemAsync(Request);

        _isLoading = false;

        if (response.Error is not null)
        {
            _error = response.Error;

            return;
        }

        MudDialog.Close(DialogResult.Ok(true));
    }

    private void OnInvalidSubmit(EditContext editContext)
    {
        _snackbar.AddErrors(editContext.GetValidationMessages());
    }
}
