namespace KomaeiSample.Share.Common;
public static class ByteExtensions
{
    public static string ToHasOrNotTitle(this byte value)
    {
        return value == 1 ? "دارد" : "ندارد";
    }
}