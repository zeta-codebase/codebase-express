using System.Net.Sockets;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Zeta.CodebaseExpress.Application.Common.Exceptions;
using Zeta.CodebaseExpress.Application.Services.Persistence;
using Zeta.CodebaseExpress.Domain.Entities;
using Zeta.CodebaseExpress.Domain.Events;
using Zeta.CodebaseExpress.Shared.ToDoItems.Commands.CreateToDoItem;

namespace Zeta.CodebaseExpress.Application.ToDoItems.Commands.CreateToDoItem;

public class CreateToDoItemCommand : CreateToDoItemRequest, IRequest<CreateToDoItemResponse>
{
}

public class CreateToDoItemCommandValidator : AbstractValidator<CreateToDoItemCommand>
{
    public CreateToDoItemCommandValidator()
    {
        Include(new CreateToDoItemRequestValidator());
    }
}

public class CreateToDoItemCommandHandler : IRequestHandler<CreateToDoItemCommand, CreateToDoItemResponse>
{
    private readonly IApplicationDbContext _context;

    public CreateToDoItemCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<CreateToDoItemResponse> Handle(CreateToDoItemCommand request, CancellationToken cancellationToken)
    {
        var toDoGroup = await _context.ToDoGroups
            .AsNoTracking()
            .Where(x => x.Id == request.ToDoGroupId)
            .SingleOrDefaultAsync(cancellationToken);

        if (toDoGroup is null)
        {
            throw new NotFoundException(Shared.ToDoGroups.Constants.DisplayTextFor.ToDoGroup, request.ToDoGroupId);
        }

        var toDoItem = new ToDoItem
        {
            ToDoGroupId = request.ToDoGroupId,

            Id = Guid.NewGuid(),
            Title = request.Title,
            Description = request.Description,
            PriorityLevel = request.PriorityLevel
        };

        toDoItem.DomainEvents.Add(new ToDoItemCreatedEvent(toDoItem));

        await _context.ToDoItems.AddAsync(toDoItem, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return new CreateToDoItemResponse
        {
            ToDoItemId = toDoItem.Id
        };
    }
}
