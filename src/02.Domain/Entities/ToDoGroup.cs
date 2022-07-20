using Zeta.CodebaseExpress.Domain.Abstracts;

namespace Zeta.CodebaseExpress.Domain.Entities;

public class ToDoGroup : ModifiableEntity
{
    public string Name { get; set; } = default!;

    public IList<ToDoItem> ToDoItems { get; set; } = new List<ToDoItem>();
}
