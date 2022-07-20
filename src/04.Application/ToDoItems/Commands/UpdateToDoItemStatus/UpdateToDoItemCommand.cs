using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Zeta.CodebaseExpress.Application.Common.Exceptions;
using Zeta.CodebaseExpress.Application.Services.Persistence;
using Zeta.CodebaseExpress.Shared.ToDoItems.Commands.UpdateToDoItemStatus;
using Zeta.CodebaseExpress.Shared.ToDoItems.Constants;

namespace Zeta.CodebaseExpress.Application.ToDoItems.Commands.UpdateToDoItemStatus;

public class UpdateToDoItemStatusCommand : UpdateToDoItemStatusRequest, IRequest
{
}

public class UpdateToDoItemStatusCommandValidator : AbstractValidator<UpdateToDoItemStatusCommand>
{
    public UpdateToDoItemStatusCommandValidator()
    {
        Include(new UpdateToDoItemStatusRequestValidator());
    }
}

public class UpdateToDoItemStatusCommandHandler : IRequestHandler<UpdateToDoItemStatusCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateToDoItemStatusCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateToDoItemStatusCommand request, CancellationToken cancellationToken)
    {
        var toDoItem = await _context.ToDoItems
            .Where(x => x.Id == request.ToDoItemId)
            .SingleOrDefaultAsync(cancellationToken);

        if (toDoItem is null)
        {
            throw new NotFoundException(DisplayTextFor.ToDoItem, request.ToDoItemId);
        }

        toDoItem.IsDone = request.IsDone;

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
