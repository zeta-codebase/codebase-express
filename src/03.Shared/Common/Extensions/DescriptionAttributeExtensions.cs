using System.ComponentModel;

namespace Zeta.CodebaseExpress.Shared.Common.Extensions;

public static class DescriptionAttributeExtensions
{
    public static string GetDescription(this Enum enumValue)
    {
        var enumValueName = enumValue.ToString();

        var enumFieldInfo = enumValue.GetType().GetField(enumValueName);

        if (enumFieldInfo is null)
        {
            return enumValueName;
        }

        var descriptionAttributes = enumFieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

        if (descriptionAttributes is null)
        {
            return enumValueName;
        }

        if (!descriptionAttributes.Any())
        {
            return enumValueName;
        }

        return ((DescriptionAttribute)descriptionAttributes.First()).Description;
    }
}
