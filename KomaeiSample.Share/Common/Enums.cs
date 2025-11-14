namespace KomaeiSample.Share.Common;

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