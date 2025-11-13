namespace KomaeiSample.Share.Common;
public static class EnumExtensions
{
    public static int ToInt(this Enum value)
    {
        return Convert.ToInt32(value);
    }

    public static string GetDescription(this Enum value)
    {
        var type = value.GetType();
        var name = Enum.GetName(type, value);

        if (name == null)
            return "";
        var field = type.GetField(name);

        if (field == null)
            return "";
        var attr = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;

        if (attr == null)
            return "";
        return attr.Description;
    }
}