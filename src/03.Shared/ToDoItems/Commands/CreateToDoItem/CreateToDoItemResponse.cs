using Zeta.CodebaseExpress.Shared.Common.Responses;

namespace Zeta.CodebaseExpress.Shared.ToDoItems.Commands.CreateToDoItem;

public class CreateToDoItemResponse : Response
{
    public Guid ToDoItemId { get; set; }
}
