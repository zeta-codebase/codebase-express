using MediatR;
using Microsoft.Extensions.Logging;
using Zeta.CodebaseExpress.Application.Services.DomainEvent.Models;
using Zeta.CodebaseExpress.Domain.Events;

namespace Zeta.CodebaseExpress.Application.ToDoItems.EventHandlers;

public class ToDoItemCreatedEventHandler : INotificationHandler<DomainEventNotification<ToDoItemCreatedEvent>>
{
    private readonly ILogger<ToDoItemCreatedEventHandler> _logger;

    public ToDoItemCreatedEventHandler(ILogger<ToDoItemCreatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(DomainEventNotification<ToDoItemCreatedEvent> notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("CodebaseExpress Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}
