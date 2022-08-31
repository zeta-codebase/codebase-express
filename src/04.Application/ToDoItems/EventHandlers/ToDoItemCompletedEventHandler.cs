using MediatR;
using Microsoft.Extensions.Logging;
using Zeta.CodebaseExpress.Application.Services.DomainEvent.Models;
using Zeta.CodebaseExpress.Domain.Events;

namespace Zeta.CodebaseExpress.Application.ToDoItems.EventHandlers;

public class ToDoItemCompletedEventHandler : INotificationHandler<DomainEventNotification<ToDoItemCompletedEvent>>
{
    private readonly ILogger<ToDoItemCompletedEventHandler> _logger;

    public ToDoItemCompletedEventHandler(ILogger<ToDoItemCompletedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(DomainEventNotification<ToDoItemCompletedEvent> notification, CancellationToken cancellationToken)
    {
        var domainEvent = notification.DomainEvent;

        _logger.LogInformation("CodebaseExpress Domain Event: {DomainEvent}", domainEvent.GetType().Name);

        return Task.CompletedTask;
    }
}
