using Zeta.CodebaseExpress.Shared.Common.Responses;

namespace Zeta.CodebaseExpress.Shared.ToDoGroups.Commands.CreateToDoGroup;

public class CreateToDoGroupResponse : Response
{
    public Guid ToDoGroupId { get; set; }
}
