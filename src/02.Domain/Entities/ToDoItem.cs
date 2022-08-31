using Zeta.CodebaseExpress.Base.TodoItems.Enums;
using Zeta.CodebaseExpress.Domain.Abstracts;
using Zeta.CodebaseExpress.Domain.Events;

namespace Zeta.CodebaseExpress.Domain.Entities;

public class ToDoItem : ModifiableEntity, IHasDomainEvent
{
    public IList<DomainEvent> DomainEvents { get; set; } = new List<DomainEvent>();

    public Guid ToDoGroupId { get; set; }
    public ToDoGroup ToDoGroup { get; set; } = default!;

    public string Title { get; set; } = default!;
    public string? Description { get; set; } = default!;
    public PriorityLevel PriorityLevel { get; set; }
    public bool IsDone { get; set; }
}
