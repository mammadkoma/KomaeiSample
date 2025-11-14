namespace KomaeiSample.Share.Common;

public class Constants
{
    // Validation Error Message
    public const string RequireMsg = "الزامی است";
    public const string MinLengthMsg = "باید دارای طول بیشتر از {1} باشد";
    public const string MaxLengthMsg = "باید دارای طول کمتر از {1} باشد";
    public const string LengthMsg = "باید دارای طول {1} باشد";
    public const string RangeMsg = "باید عدد بین {1} و {2} باشد";
    public const string RegularExpressionMsg = "معتبر نیست";
    public const string PasswordMsg = "باید شامل حرف انگلیسی و عدد باشد";
    public const string CompareMsg = "{1} و {0} با هم مطابقت ندارند";

    // Role
    public const string UserRole = "u";
    public const string AdminRole = "a";
    public const string SuperAdminRole = "sa";

    // Token Claims
    public const string UserTokenIdClaim = "i";
    public const string UserIdClaim = "ui";
    public const string RoleClaim = "r";
    //public const string MobileClaim = "m";

    // Jwt
    public const string JwtSecretKey = "asdfghjkl";
    public const int JwtExpireDays = 14;
    public const int JwtRefreshDays = 3;

    // DeliveryMethods
    public const string DeliveryMethodSend = "ارسال به محل";
    public const string DeliveryMethodInPerson = "حضوری";

    // AddressTypes
    public const string AddressTypeTehran = "تهران";
    public const string AddressTypeOtherCities = "سایر شهرستانها";

    // TehranAreas
    public const string TehranAreaNorth = "شمال";
    public const string TehranAreaSouth = "جنوب";
    public const string TehranAreaWest = "غرب";
    public const string TehranAreaEast = "شرق";
    public const string TehranAreaCenter = "مرکز";
}