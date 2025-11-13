namespace KomaeiSample.Share.Vm;
public class RegisterVm
{
    [MaxLength(30, ErrorMessage = Constants.MaxLengthMsg)]
    public string FullName { get; set; } = string.Empty;

    [Required(ErrorMessage = Constants.RequireMsg)]
    [RegularExpression(@"^09\d{9}$", ErrorMessage = "شماره موبایل نامعتبر است")]
    public string Mobile { get; set; } = "09";

    [Display(Name = "پسورد")]
    [Required(ErrorMessage = Constants.RequireMsg)]
    [MinLength(6, ErrorMessage = Constants.MinLengthMsg)]
    [MaxLength(20, ErrorMessage = Constants.MaxLengthMsg)]
    [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d).{6,}$", ErrorMessage = Constants.PasswordMsg)]
    public string Password { get; set; } = "";

    [Display(Name = "تکرار پسورد")]
    [Required(ErrorMessage = Constants.RequireMsg)]
    [MinLength(6, ErrorMessage = Constants.MinLengthMsg)]
    [MaxLength(20, ErrorMessage = Constants.MaxLengthMsg)]
    [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d).{6,}$", ErrorMessage = Constants.PasswordMsg)]
    [Compare("Password", ErrorMessage = Constants.CompareMsg)]
    public string ConfirmPassword { get; set; } = "";

    [Display(Name = "کد معرف")]
    [RegularExpression(@"(^$)|(^ID9\d{9}$)", ErrorMessage = Constants.RegularExpressionMsg)]
    public string ReferralCode { get; set; } = "";
}

public class ConfirmRegisterVm
{
    [Required(ErrorMessage = Constants.RequireMsg)]
    public Guid Id { get; set; }

    [Required(ErrorMessage = Constants.RequireMsg)]
    [Length(5, 5, ErrorMessage = Constants.LengthMsg)]
    public string ConfirmCode { get; set; } = "";
}

public class LoginVm
{
    [Required(ErrorMessage = Constants.RequireMsg)]
    [RegularExpression(@"^09\d{9}$", ErrorMessage = "شماره موبایل نامعتبر است")]
    public string Mobile { get; set; } = "09";

    [Display(Name = "پسورد")]
    [Required(ErrorMessage = Constants.RequireMsg)]
    [MinLength(6, ErrorMessage = Constants.MinLengthMsg)]
    [MaxLength(20, ErrorMessage = Constants.MaxLengthMsg)]
    [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d).{6,}$", ErrorMessage = Constants.PasswordMsg)]
    public string Password { get; set; } = "";
}

public class ChangeFullNameVm
{
    [Required(ErrorMessage = Constants.RequireMsg)]
    [MinLength(2, ErrorMessage = Constants.MinLengthMsg)]
    [MaxLength(30, ErrorMessage = Constants.MaxLengthMsg)]
    public string FullName { get; set; } = string.Empty;
}

public class ChangePasswordVm
{
    [Required(ErrorMessage = Constants.RequireMsg)]
    [MinLength(6, ErrorMessage = Constants.MinLengthMsg)]
    [MaxLength(20, ErrorMessage = Constants.MaxLengthMsg)]
    [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d).{6,}$", ErrorMessage = Constants.PasswordMsg)]
    public string? CurrentPassword { get; set; }

    [Display(Name = "پسورد")]
    [Required(ErrorMessage = Constants.RequireMsg)]
    [MinLength(6, ErrorMessage = Constants.MinLengthMsg)]
    [MaxLength(20, ErrorMessage = Constants.MaxLengthMsg)]
    [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d).{6,}$", ErrorMessage = Constants.PasswordMsg)]
    public string NewPassword { get; set; } = "";

    [Display(Name = "تکرار پسورد")]
    [Required(ErrorMessage = Constants.RequireMsg)]
    [MinLength(6, ErrorMessage = Constants.MinLengthMsg)]
    [MaxLength(20, ErrorMessage = Constants.MaxLengthMsg)]
    [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d).{6,}$", ErrorMessage = Constants.PasswordMsg)]
    [Compare(nameof(NewPassword), ErrorMessage = Constants.CompareMsg)]
    public string ConfirmNewPassword { get; set; } = "";
}

public class MobileVm
{
    [Required(ErrorMessage = Constants.RequireMsg)]
    [RegularExpression(@"^09\d{9}$", ErrorMessage = "شماره موبایل نامعتبر است")]
    public string Mobile { get; set; } = "09";
}

public class ChangePasswordInForgetVm
{
    [Required(ErrorMessage = Constants.RequireMsg)]
    public Guid Id { get; set; }

    [Required(ErrorMessage = Constants.RequireMsg)]
    [Length(5, 5, ErrorMessage = Constants.LengthMsg)]
    public string ConfirmCode { get; set; } = "";

    [Display(Name = "پسورد")]
    [Required(ErrorMessage = Constants.RequireMsg)]
    [MinLength(6, ErrorMessage = Constants.MinLengthMsg)]
    [MaxLength(20, ErrorMessage = Constants.MaxLengthMsg)]
    [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d).{6,}$", ErrorMessage = Constants.PasswordMsg)]
    public string NewPassword { get; set; } = "";

    [Display(Name = "تکرار پسورد")]
    [Required(ErrorMessage = Constants.RequireMsg)]
    [MinLength(6, ErrorMessage = Constants.MinLengthMsg)]
    [MaxLength(20, ErrorMessage = Constants.MaxLengthMsg)]
    [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d).{6,}$", ErrorMessage = Constants.PasswordMsg)]
    [Compare(nameof(NewPassword), ErrorMessage = Constants.CompareMsg)]
    public string ConfirmNewPassword { get; set; } = "";
}

public class UserLogoVm
{
    [Required(ErrorMessage = Constants.RequireMsg)]
    public IBrowserFile? File { get; set; }
}