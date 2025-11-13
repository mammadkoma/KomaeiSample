namespace KomaeiSample.Share.Common;
public enum HourDay
{
    Hour,
    Day,
}

public enum CategoriesEnum
{
    /// <summary>
    /// پاکت نامه و اداری
    /// </summary>
    [Description("پاکت نامه و اداری")]
    Office = 1,

    /// <summary>
    /// پاکت آزمایشگاهی و بیمارستانی
    /// </summary>
    [Description("پاکت آزمایشگاهی و بیمارستانی")]
    Hospital = 2,

    /// <summary>
    /// پاکت تبلیغاتی (ساک دستی)
    /// </summary>
    [Description("پاکت تبلیغاتی (ساک دستی)")]
    HandBag = 3,

    /// <summary>
    /// پاکت محرمانه و پستی
    /// </summary>
    [Description("پاکت محرمانه و پستی")]
    Confidential = 4,

    /// <summary>
    /// پاکت اختصاصی
    /// </summary>
    [Description("پاکت اختصاصی")]
    Exclusive = 5,
}

public enum DeliveryMethods
{
    Send = 1,
    InPerson = 2,
}

public enum AddressTypes
{
    Tehran = 1,
    OtherCities = 2,
}

public enum TehranAreas
{
    North = 1,
    South = 2,
    West = 3,
    East = 4,
    Center = 5,
}

public enum OrderStatusesEnum
{
    Card = 1, // سبد خرید
    Pay = 2, // پرداخت شده
    SendToPrint = 3, // ارسال برای چاپ
    SendToCustomer = 4 // آماده تحویل
}

public enum SettingEnum
{
    /// <summary>
    /// جمله بالای سایت
    /// </summary>
    [Description("جمله بالای سایت")]
    TopSentence = 1,

    /// <summary>
    /// صفحه ورود
    /// </summary>
    [Description("صفحه ورود")]
    Login = 2,

    /// <summary>
    /// صفحه خانه - پیشنهاد شگفت‌انگیز
    /// </summary>
    [Description("صفحه خانه - پیشنهاد شگفت‌انگیز")]
    Home1 = 3,

    /// <summary>
    /// صفحه خانه - جدیدترین‌ها
    /// </summary>
    [Description("صفحه خانه - جدیدترین‌ها")]
    Home2 = 4,

    /// <summary>
    /// مبلغ جایزه بابت عضویت و خرید زیر مجموعه به معرف
    /// </summary>
    [Description("مبلغ جایزه بابت عضویت و خرید زیر مجموعه به معرف")]
    UserRewardAmount = 5,
}

public enum UpdateMultiplePriceType
{
    [Description("مبلغی")]
    Price = 1,

    [Description("‌درصدی")]
    Percent = 2,
}

public enum UserRewardTypes
{
    /// <summary>
    /// ثبت نام زیرمجموعه - سود سهامداری
    /// </summary>
    [Description("سود سهامداری")]
    Register = 1,

    /// <summary>
    /// خرید توسط کاربر معرفی شده
    /// </summary>
    [Description("خرید توسط کاربر معرفی شده")]
    Pay = 2,

    /// <summary>
    /// واریز توسط مدیریت سیستم
    /// </summary>
    [Description("واریز توسط مدیریت سیستم")]
    Admin = 3,
}