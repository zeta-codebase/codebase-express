namespace Zeta.CodebaseExpress.Domain.Interfaces;

public interface IModifiable
{
    DateTimeOffset? Modified { get; set; }
}
