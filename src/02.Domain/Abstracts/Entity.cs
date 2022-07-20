using Zeta.CodebaseExpress.Domain.Interfaces;

namespace Zeta.CodebaseExpress.Domain.Abstracts;

public abstract class Entity : ICreatable
{
    public Guid Id { get; set; }
    public DateTimeOffset Created { get; set; }
}
