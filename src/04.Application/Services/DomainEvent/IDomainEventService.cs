namespace Zeta.CodebaseExpress.Application.Services.DomainEvent;

public interface IDomainEventService
{
    Task Publish(Domain.Events.DomainEvent domainEvent);
}
