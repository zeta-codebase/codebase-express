using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Zeta.CodebaseExpress.Shared.Common.Responses;
using Zeta.CodebaseExpress.Shared.ToDoItems.Queries.GetToDoItem;

namespace Zeta.CodebaseExpress.Bsui.Features.ToDoItems.Components;

public partial class DialogDetails
{
    [CascadingParameter]
    private MudDialogInstance MudDialog { get; set; } = default!;

    [Parameter]
    public Guid ToDoItemId { get; set; }

    private bool _isLoading;
    private ErrorResponse? _error;
    private GetToDoItemResponse _toDoItem = default!;

    protected override void OnInitialized()
    {
        MudDialog.Options.CloseButton = true;
        MudDialog.SetOptions(MudDialog.Options);
    }

    protected override async Task OnParametersSetAsync()
    {
        _isLoading = true;

        var response = await _toDoItemService.GetToDoItemAsync(ToDoItemId);

        _isLoading = false;

        if (response.Error is not null)
        {
            _error = response.Error;

            return;
        }

        _toDoItem = response.Result!;
    }

    private void Close()
    {
        MudDialog.Cancel();
    }
}
