using Zeta.CodebaseExpress.Domain.Entities;

namespace Zeta.CodebaseExpress.Domain.Events;

public class ToDoItemCompletedEvent : DomainEvent
{
    public ToDoItem ToDoItem { get; set; }

    public ToDoItemCompletedEvent(ToDoItem toDoItem)
    {
        ToDoItem = toDoItem;
    }
}
