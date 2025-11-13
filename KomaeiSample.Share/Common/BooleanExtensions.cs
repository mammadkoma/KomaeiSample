namespace KomaeiSample.Share.Common;
public static class BoolanExtensions
{
    public static string ToYesNo(this bool? value)
    {
        return value == true ? "Yes" : "No";
    }
}