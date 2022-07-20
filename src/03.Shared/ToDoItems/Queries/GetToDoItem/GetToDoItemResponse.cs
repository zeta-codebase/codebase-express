using Zeta.CodebaseExpress.Base.TodoItems.Enums;
using Zeta.CodebaseExpress.Shared.Common.Responses;

namespace Zeta.CodebaseExpress.Shared.ToDoItems.Queries.GetToDoItem;

public class GetToDoItemResponse : Response
{
    public Guid Id { get; set; }
    public string Title { get; set; } = default!;
    public string? Description { get; set; }
    public PriorityLevel PriorityLevel { get; set; }
    public bool IsDone { get; set; }

    public DateTimeOffset Created { get; set; }
    public DateTimeOffset? Modified { get; set; }
}
