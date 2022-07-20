using FluentValidation;
using Zeta.CodebaseExpress.Base.TodoItems.Enums;
using Zeta.CodebaseExpress.Shared.ToDoItems.Constants;

namespace Zeta.CodebaseExpress.Shared.ToDoItems.Commands.CreateToDoItem;

public class CreateToDoItemRequest
{
    public Guid ToDoGroupId { get; set; } = default!;

    public string Title { get; set; } = default!;
    public string? Description { get; set; } = default!;
    public PriorityLevel PriorityLevel { get; set; } = default!;
}

public class CreateToDoItemRequestValidator : AbstractValidator<CreateToDoItemRequest>
{
    public CreateToDoItemRequestValidator()
    {
        RuleFor(v => v.ToDoGroupId)
            .NotEmpty();

        RuleFor(v => v.Title)
            .NotEmpty()
            .MaximumLength(MaximumLengthFor.Title);

        RuleFor(v => v.Description)
            .MaximumLength(MaximumLengthFor.Description);

        RuleFor(v => v.PriorityLevel)
            .IsInEnum();
    }
}
