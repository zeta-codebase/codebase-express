using Zeta.CodebaseExpress.Shared.Common.Responses;

namespace Zeta.CodebaseExpress.Application.Common.Extensions;

public static class ListExtensions
{
    public static ListResponse<T> ToListResponse<T>(this List<T> source)
    {
        return new ListResponse<T> { Items = source };
    }
}
