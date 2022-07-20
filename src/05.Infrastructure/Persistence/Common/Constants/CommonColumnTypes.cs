namespace Zeta.CodebaseExpress.Infrastructure.Persistence.Common.Constants;

public static class CommonColumnTypes
{
    public const string NvarcharMax = "nvarchar(max)";
    public const string VarbinaryMax = "varbinary(max)";

    public static string Nvarchar(int length)
    {
        return $"nvarchar({length})";
    }
}
