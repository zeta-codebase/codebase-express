namespace Zeta.CodebaseExpress.Shared.Common.Extensions;

public static class BooleanExtensions
{
    public static string ToYesNoDisplayText(this bool value)
    {
        return value ? "Yes" : "No";
    }

    public static string ToDoneUndoneDisplayText(this bool value)
    {
        return value ? "Done" : "Undone";
    }
}
