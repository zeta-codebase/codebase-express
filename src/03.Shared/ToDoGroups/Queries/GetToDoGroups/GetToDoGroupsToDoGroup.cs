namespace Zeta.CodebaseExpress.Shared.ToDoGroups.Queries.GetToDoGroups;

public class GetToDoGroupsToDoGroup
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;

    public DateTimeOffset Created { get; set; }
    public DateTimeOffset? Modified { get; set; }
}
