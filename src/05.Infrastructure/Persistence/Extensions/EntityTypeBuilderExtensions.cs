using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zeta.CodebaseExpress.Domain.Events;

namespace Zeta.CodebaseExpress.Infrastructure.Persistence.Common.Extensions;

public static class EntityTypeBuilderExtensions
{
    public static void IgnoreDomainEventsProperties<TEntity>(this EntityTypeBuilder<TEntity> builder)
        where TEntity : class, IHasDomainEvent
    {
        builder.Ignore(e => e.DomainEvents);
    }
}
