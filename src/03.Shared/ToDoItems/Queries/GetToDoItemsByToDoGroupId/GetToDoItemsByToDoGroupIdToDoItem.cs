using Zeta.CodebaseExpress.Base.TodoItems.Enums;

namespace Zeta.CodebaseExpress.Shared.ToDoItems.Queries.GetToDoItemsByToDoGroupId;

public class GetToDoItemsByToDoGroupIdToDoItem
{
    public Guid Id { get; set; }
    public string Title { get; set; } = default!;
    public string? Description { get; set; }
    public PriorityLevel PriorityLevel { get; set; }
    public bool IsDone { get; set; }

    public DateTimeOffset Created { get; set; }
    public DateTimeOffset? Modified { get; set; }
}
