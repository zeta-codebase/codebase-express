using Zeta.CodebaseExpress.Base.TodoItems.Enums;

namespace Zeta.CodebaseExpress.Shared.ToDoGroups.Queries.GetToDoGroup;

public class GetToDoGroupToDoItem
{
    public Guid Id { get; set; } = default!;
    public string Title { get; set; } = default!;
    public string? Description { get; set; } = default!;
    public PriorityLevel PriorityLevel { get; set; }
    public bool IsDone { get; set; }

    public DateTimeOffset Created { get; set; }
    public DateTimeOffset? Modified { get; set; }
}
