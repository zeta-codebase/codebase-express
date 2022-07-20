namespace Zeta.CodebaseExpress.Bsui.Features.ToDoGroups.Constants;

public static class RouteFor
{
    public const string Index = nameof(ToDoGroups);

    public static string Details(Guid id)
    {
        return $"{nameof(ToDoGroups)}/{nameof(Details)}/{id}";
    }
}
