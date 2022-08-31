using Zeta.CodebaseExpress.Domain.Entities;

namespace Zeta.CodebaseExpress.Domain.Events;

public class ToDoItemCreatedEvent : DomainEvent
{
    public ToDoItem ToDoItem { get; set; }

    public ToDoItemCreatedEvent(ToDoItem toDoItem)
    {
        ToDoItem = toDoItem;
    }
}
