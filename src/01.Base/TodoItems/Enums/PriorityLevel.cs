using System.ComponentModel;

namespace Zeta.CodebaseExpress.Base.TodoItems.Enums;

public enum PriorityLevel
{
    [Description("0 - None")]
    None = 0,

    [Description("1 - Low")]
    Low = 1,

    [Description("2 - Medium")]
    Medium = 2,

    [Description("3 - High")]
    High = 3
}
