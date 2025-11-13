namespace KomaeiSample.Share.Common;
public static class DateTimeExtensions
{
    public static int GetDaysDifference(this DateTime startDate, DateTime endDate)
    {
        TimeSpan timeSpan = endDate - startDate;
        return (int)Math.Floor(timeSpan.TotalDays);
    }

    public static string ToShamsiDate(this DateTime? dateTime)
    {
        if (dateTime is null) return "";
        var persianCalendar = new PersianCalendar();
        var year = persianCalendar.GetYear(dateTime!.Value).ToString("00");
        var month = persianCalendar.GetMonth(dateTime.Value).ToString("00");
        var day = persianCalendar.GetDayOfMonth(dateTime.Value).ToString("00");
        return $"{year}/{month}/{day}";
    }

    public static string ToShamsiDate(this DateTime dateTime)
    {
        var persianCalendar = new PersianCalendar();
        var year = persianCalendar.GetYear(dateTime).ToString("00");
        var month = persianCalendar.GetMonth(dateTime).ToString("00");
        var day = persianCalendar.GetDayOfMonth(dateTime).ToString("00");
        return $"{year}/{month}/{day}";
    }

    public static string ToShamsiDateTime(this DateTime dateTime)
    {
        var persianCalendar = new PersianCalendar();
        var year = persianCalendar.GetYear(dateTime).ToString("00");
        var month = persianCalendar.GetMonth(dateTime).ToString("00");
        var day = persianCalendar.GetDayOfMonth(dateTime).ToString("00");
        return $"{dateTime.ToLongTimeString()} - {year}/{month}/{day}";
    }

    public static string ToShamsiDateTime(this DateTime? dateTime)
    {
        if (dateTime is null) return "";
        var persianCalendar = new PersianCalendar();
        var year = persianCalendar.GetYear(dateTime.Value).ToString("00");
        var month = persianCalendar.GetMonth(dateTime.Value).ToString("00");
        var day = persianCalendar.GetDayOfMonth(dateTime.Value).ToString("00");
        return $"{dateTime.Value.ToLongTimeString()} - {year}/{month}/{day}";
    }

    public static string ToShamsiDayName(this DateTime dateTime)
    {
        return dateTime.DayOfWeek.ToString() switch
        {
            "Saturday" => "شنبه",
            "Sunday" => "یکشنبه",
            "Monday" => "‌دوشنبه",
            "Tuesday" => "سه‌شنبه",
            "Wednesday" => "چهارشنبه",
            "Thursday" => "پنجشنبه",
            "Friday" => "جمعه",
            _ => "bug!",
        };
    }

    public static string ToShamsiDay(this DateTime dateTime)
    {
        return new PersianCalendar().GetDayOfMonth(dateTime).ToString();
    }

    public static string ToShamsiMonthName(this DateTime dateTime)
    {
        var persianCalendar = new PersianCalendar();
        return persianCalendar.GetMonth(dateTime) switch
        {
            1 => "فروردین",
            2 => "اردیبهشت",
            3 => "‌خرداد",
            4 => "تیر",
            5 => "مرداد",
            6 => "شهریور",
            7 => "مهر",
            8 => "آبان",
            9 => "آذر",
            10 => "دی",
            11 => "بهمن",
            12 => "اسفند",
            _ => "bug!",
        };
    }

    public static bool IsLeapYear(this DateTime dateTime)
    {
        var persianCalendar = new PersianCalendar();
        return persianCalendar.IsLeapYear(persianCalendar.GetYear(dateTime));
    }
}