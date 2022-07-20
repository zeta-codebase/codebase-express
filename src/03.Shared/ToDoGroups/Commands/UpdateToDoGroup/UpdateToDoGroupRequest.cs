using FluentValidation;
using Zeta.CodebaseExpress.Shared.ToDoGroups.Constants;

namespace Zeta.CodebaseExpress.Shared.ToDoGroups.Commands.UpdateToDoGroup;

public class UpdateToDoGroupRequest
{
    public Guid ToDoGroupId { get; set; }
    public string Name { get; set; } = default!;
}

public class UpdateToDoGroupRequestValidator : AbstractValidator<UpdateToDoGroupRequest>
{
    public UpdateToDoGroupRequestValidator()
    {
        RuleFor(v => v.ToDoGroupId)
            .NotEmpty();

        RuleFor(v => v.Name)
            .NotEmpty()
            .MaximumLength(MaximumLengthFor.Name);
    }
}
