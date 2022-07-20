using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zeta.CodebaseExpress.Application.Common.Exceptions;
using Zeta.CodebaseExpress.Application.ToDoGroups.Commands.CreateToDoGroup;
using Zeta.CodebaseExpress.Application.ToDoGroups.Commands.DeleteToDoGroup;
using Zeta.CodebaseExpress.Application.ToDoGroups.Commands.UpdateToDoGroup;
using Zeta.CodebaseExpress.Application.ToDoGroups.Queries.GetToDoGroup;
using Zeta.CodebaseExpress.Application.ToDoGroups.Queries.GetToDoGroups;
using Zeta.CodebaseExpress.Shared.Common.Responses;
using Zeta.CodebaseExpress.Shared.ToDoGroups.Commands.CreateToDoGroup;
using Zeta.CodebaseExpress.Shared.ToDoGroups.Queries.GetToDoGroup;
using Zeta.CodebaseExpress.Shared.ToDoGroups.Queries.GetToDoGroups;

namespace Zeta.CodebaseExpress.WebApi.Areas.V1.Controllers;

public class ToDoGroupsController : ApiControllerBase
{
    [HttpPost]
    public async Task<ActionResult<CreateToDoGroupResponse>> CreateToDoGroup([FromForm] CreateToDoGroupCommand command)
    {
        return CreatedAtAction(nameof(CreateToDoGroup), await Mediator.Send(command));
    }

    [HttpPut("{toDoGroupId:guid}")]
    public async Task<ActionResult> UpdateToDoGroup([FromRoute] Guid toDoGroupId, [FromForm] UpdateToDoGroupCommand command)
    {
        if (toDoGroupId != command.ToDoGroupId)
        {
            throw new MismatchException(nameof(UpdateToDoGroupCommand.ToDoGroupId), toDoGroupId, command.ToDoGroupId);
        }

        await Mediator.Send(command);

        return NoContent();
    }

    [HttpDelete("{toDoGroupId:guid}")]
    public async Task<ActionResult> DeleteToDoGroup([FromRoute] Guid toDoGroupId)
    {
        await Mediator.Send(new DeleteToDoGroupCommand { ToDoGroupId = toDoGroupId });

        return NoContent();
    }

    [HttpGet]
    public async Task<ActionResult<PaginatedListResponse<GetToDoGroupsToDoGroup>>> GetToDoGroups([FromQuery] GetToDoGroupsQuery query)
    {
        return await Mediator.Send(query);
    }

    [HttpGet("{toDoGroupId:guid}")]
    public async Task<ActionResult<GetToDoGroupResponse>> GetToDoGroup([FromRoute] Guid toDoGroupId)
    {
        return await Mediator.Send(new GetToDoGroupQuery { ToDoGroupId = toDoGroupId });
    }
}
