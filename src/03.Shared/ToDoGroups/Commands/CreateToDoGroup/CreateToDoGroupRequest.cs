using FluentValidation;
using Zeta.CodebaseExpress.Shared.ToDoGroups.Constants;

namespace Zeta.CodebaseExpress.Shared.ToDoGroups.Commands.CreateToDoGroup;

public class CreateToDoGroupRequest
{
    public string Name { get; set; } = default!;
}

public class CreateToDoGroupRequestValidator : AbstractValidator<CreateToDoGroupRequest>
{
    public CreateToDoGroupRequestValidator()
    {
        RuleFor(v => v.Name)
            .NotEmpty()
            .MaximumLength(MaximumLengthFor.Name);
    }
}
