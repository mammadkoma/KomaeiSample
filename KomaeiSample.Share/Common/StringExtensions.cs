namespace KomaeiSample.Share.Common;
public static class StringExtensions
{
    public static bool IsValidGuid(this string value)
    {
        if (string.IsNullOrEmpty(value))
            return false;

        Regex regex = new Regex(@"^[A-Fa-f0-9]{8}-[A-Fa-f0-9]{4}-[A-Fa-f0-9]{4}-[A-Fa-f0-9]{4}-[A-Fa-f0-9]{12}$");
        return regex.IsMatch(value);
    }

    public static bool IsNullOrEmpty(this string value)
    {
        return string.IsNullOrEmpty(value);
    }

    public static string ToReferralCode(this string mobile)
    {
        return "ID" + mobile.Substring(1, 10);
    }

    public static string ToMobile(this string referralCode)
    {
        return "0" + referralCode.Substring(2, 10);
    }

    public static decimal ToDecimal(this string value)
    {
        return decimal.Parse(value);
    }
}