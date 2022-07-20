using MediatR;
using Microsoft.EntityFrameworkCore;
using Zeta.CodebaseExpress.Application.Common.Exceptions;
using Zeta.CodebaseExpress.Application.Services.Persistence;
using Zeta.CodebaseExpress.Shared.ToDoItems.Constants;

namespace Zeta.CodebaseExpress.Application.ToDoItems.Commands.DeleteToDoItem;

public class DeleteToDoItemCommand : IRequest
{
    public Guid ToDoItemId { get; set; }
}

public class DeleteToDoItemCommandHandler : IRequestHandler<DeleteToDoItemCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteToDoItemCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteToDoItemCommand request, CancellationToken cancellationToken)
    {
        var toDoItem = await _context.ToDoItems
            .Where(x => x.Id == request.ToDoItemId)
            .SingleOrDefaultAsync(cancellationToken);

        if (toDoItem is null)
        {
            throw new NotFoundException(DisplayTextFor.ToDoItem, request.ToDoItemId);
        }

        _context.ToDoItems.Remove(toDoItem);

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
