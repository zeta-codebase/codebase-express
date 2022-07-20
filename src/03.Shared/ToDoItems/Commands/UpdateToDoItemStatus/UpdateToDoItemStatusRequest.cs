using FluentValidation;

namespace Zeta.CodebaseExpress.Shared.ToDoItems.Commands.UpdateToDoItemStatus;

public class UpdateToDoItemStatusRequest
{
    public Guid ToDoItemId { get; set; }
    public bool IsDone { get; set; }
}

public class UpdateToDoItemStatusRequestValidator : AbstractValidator<UpdateToDoItemStatusRequest>
{
    public UpdateToDoItemStatusRequestValidator()
    {
        RuleFor(v => v.ToDoItemId)
            .NotEmpty();
    }
}
