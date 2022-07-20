using Zeta.CodebaseExpress.Domain.Interfaces;

namespace Zeta.CodebaseExpress.Domain.Abstracts;

public abstract class ModifiableEntity : Entity, IModifiable
{
    public DateTimeOffset? Modified { get; set; }
}
