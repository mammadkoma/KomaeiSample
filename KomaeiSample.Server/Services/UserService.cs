namespace KomaeiSample.Server.Services;
public class UserService(AppDbContext appDbContext, PasswordService passwordService, TokenService tokenService, UserTokenService userTokenService, SmsService smsService, IHttpContextAccessor httpContext, FileService fileService)
{
    public async Task<Guid> Register(RegisterVm vm)
    {
        // رکورد داشته - میخواد کانفرم کنه
        var currentUser = await appDbContext.Users.FirstOrDefaultAsync(x => x.Mobile == vm.Mobile);
        var confirmCode = new Random().Next(10000, 99999);
        if (currentUser != null)
        {
            if (currentUser.IsConfirmed)
                throw new AppException("این موبایل قبلا ثبت نام شده است");
            else
            {
                currentUser.FullName = vm.FullName;
                currentUser.PasswordHash = passwordService.HashPasswordV3(vm.Password);
                currentUser.ConfirmCode = confirmCode;
                currentUser.UpdateDate = DateTime.Now;
                currentUser.ReferrerUserId = null;
                if (vm.ReferralCode.IsNullOrEmpty() == false)
                {
                    var referrer = await appDbContext.Users.FirstOrDefaultAsync(x => x.Mobile == vm.ReferralCode.ToMobile());
                    if (referrer != null)
                    {
                        var userRewardAmount = await appDbContext.Settings.AsNoTracking().Where(x => x.Id == 5).Select(x => x.Value).FirstAsync();
                        currentUser.ReferrerUserId = referrer.Id;
                        var currentUserReferral = await appDbContext.UserRewards.FirstOrDefaultAsync(x => x.ReferrerUserId == referrer.Id && x.ReferredUserId == currentUser.Id && x.UserRewardTypeId == UserRewardTypes.Register.ToInt());
                        if (currentUserReferral != null)
                            currentUserReferral.AddDate = DateTime.Now;
                        else
                            appDbContext.UserRewards.Add(new UserReward
                            {
                                ReferrerUserId = referrer.Id,
                                ReferredUserId = currentUser.Id,
                                UserRewardTypeId = UserRewardTypes.Register.ToInt(),
                                Amount = userRewardAmount.ToDecimal(),
                            });
                    }
                }
                await appDbContext.SaveChangesAsync();
                await smsService.SendOtp(vm.Mobile, confirmCode.ToString());
                return currentUser.Id; //
            }
        }

        // رکورد نداشته
        var id = Guid.NewGuid();
        var newUser = new User
        {
            Id = id,
            FullName = vm.FullName,
            Mobile = vm.Mobile,
            PasswordHash = passwordService.HashPasswordV3(vm.Password),
            ConfirmCode = confirmCode,
        };

        if (vm.ReferralCode.IsNullOrEmpty() == false)
        {
            var referrer = await appDbContext.Users.FirstOrDefaultAsync(x => x.Mobile == vm.ReferralCode.ToMobile());
            if (referrer != null)
            {
                var userRewardAmount = await appDbContext.Settings.AsNoTracking().Where(x => x.Id == 5).Select(x => x.Value).FirstAsync();
                newUser.ReferrerUserId = referrer.Id;
                appDbContext.UserRewards.Add(new UserReward
                {
                    ReferrerUserId = referrer.Id,
                    ReferredUserId = newUser.Id,
                    UserRewardTypeId = UserRewardTypes.Register.ToInt(),
                    Amount = userRewardAmount.ToDecimal(),
                });
            }
        }
        appDbContext.Users.Add(newUser);
        await appDbContext.SaveChangesAsync();
        await smsService.SendOtp(vm.Mobile, confirmCode.ToString());
        return id; //
    }

    public async Task<int> ConfirmRegisterCode(ConfirmRegisterVm vm)
    {
        if (int.TryParse(vm.ConfirmCode, out int code) == false)
            throw new AppException("کد تایید معتبر نیست");
        var currentUser = await appDbContext.Users.FirstOrDefaultAsync(x => x.Id == vm.Id);
        if (currentUser == null)
            throw new AppException("کاربر یافت نشد");
        if (currentUser.ConfirmCode != int.Parse(vm.ConfirmCode))
            throw new AppException("کد تایید نادرست است");
        currentUser.IsConfirmed = true;
        if (currentUser.ReferrerUserId != null)
        {
            var userRewardAmount = await appDbContext.Settings.AsNoTracking().Where(x => x.Id == 5).Select(x => x.Value).FirstAsync();
            var referrer = await appDbContext.Users.FirstAsync(x => x.Id == currentUser.ReferrerUserId);
            referrer.WalletBalance += userRewardAmount.ToDecimal();
        }
        return await appDbContext.SaveChangesAsync();
    }

    public async Task<LoginDto> Login(LoginVm vm)
    {
        var user = await appDbContext.Users.AsNoTracking()
            .FirstOrDefaultAsync(x => x.Mobile == vm.Mobile);
        if (user == null || passwordService.VerifyHashedPasswordV3(user.PasswordHash, vm.Password!) == false)
            throw new AppException("موبایل یا پسورد صحیح نیست");

        var userTokenId = await userTokenService.Add(user.Id, "-");

        var token = tokenService.GenerateToken(user, userTokenId);

        await userTokenService.UpdateToken(userTokenId, token);

        return new LoginDto
        {
            Token = token,
            FullName = user.FullName!,
        };
    }

    public async Task<int> Logout(int userTokenId)
    {
        return await userTokenService.Delete(userTokenId);
    }

    public async Task<int> ChangeFullName(ChangeFullNameVm vm)
    {
        return await appDbContext.Users.Where(u => u.Id == httpContext.GetUserId())
        .ExecuteUpdateAsync(u => u
        .SetProperty(x => x.FullName, vm.FullName)
        .SetProperty(x => x.UpdateDate, DateTime.Now));
    }

    public async Task<int> ChangePassword(ChangePasswordVm vm)
    {
        var user = await appDbContext.Users.FirstOrDefaultAsync(u => u.Id == httpContext.GetUserId());
        if (user == null || !passwordService.VerifyHashedPasswordV3(user.PasswordHash, vm.CurrentPassword!))
            throw new AppException("پسورد فعلی را اشتباه وارد کرده اید");

        user.PasswordHash = passwordService.HashPasswordV3(vm.NewPassword!);
        user.UpdateDate = DateTime.Now;
        await appDbContext.SaveChangesAsync();
        return await Logout(httpContext.GetUserTokenId());
    }

    public async Task<Guid> CreateNewConfirmCode(MobileVm vm)
    {
        var user = await appDbContext.Users.FirstOrDefaultAsync(x => x.Mobile == vm.Mobile);
        if (user == null)
            throw new AppException("این موبایل قبلا ثبت نام نشده است");

        if (user.IsConfirmed == false)
            throw new AppException("ثبت نام شما تکمیل نشده ، به صفحه ثبت نام بروید");

        var confirmCode = new Random().Next(10000, 99999);
        await smsService.SendOtp(vm.Mobile, confirmCode.ToString());
        user.ConfirmCode = confirmCode;
        user.UpdateDate = DateTime.Now;
        await appDbContext.SaveChangesAsync();
        return user.Id;
    }

    public async Task<int> ChangePasswordInForget(ChangePasswordInForgetVm vm)
    {
        if (int.TryParse(vm.ConfirmCode, out int code) == false)
            throw new AppException("کد تایید معتبر نیست");
        var user = await appDbContext.Users.FirstOrDefaultAsync(x => x.Id == vm.Id);
        if (user == null)
            throw new AppException("کاربر یافت نشد");
        if (user.ConfirmCode != int.Parse(vm.ConfirmCode))
            throw new AppException("کد تایید نادرست است");
        user.PasswordHash = passwordService.HashPasswordV3(vm.NewPassword);
        return await appDbContext.SaveChangesAsync();
    }

    public async Task<int> UpdateLogo(IFormFile? file)
    {
        if (file == null)
            return 1;
        var userId = httpContext.GetUserId();
        var user = await appDbContext.Users.FirstAsync(x => x.Id == userId);
        var fileName = Guid.NewGuid().ToString();
        if (user.LogoFileName!.IsNullOrEmpty() == false)
            fileService.Delete(user.LogoFileName!.Split(".")[0], "." + user.LogoFileName.Split(".")[1], "UserLogo");
        await fileService.WriteAsync(fileName, file, "UserLogo");
        user.LogoFileName = fileName + Path.GetExtension(file.FileName);
        user.UpdateDate = DateTime.Now;
        return await appDbContext.SaveChangesAsync();
    }

    public async Task<string> GetLogoFileName()
    {
        return (await appDbContext.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == httpContext.GetUserId()))!.LogoFileName!;
    }

    public async Task<UserDto[]> GetAllConfirmed()
    {
        return (await appDbContext.Users.AsNoTracking().Where(x => x.IsConfirmed).ProjectToType<UserDto>().OrderByDescending(x => x.AddDate).ToArrayAsync())!;
    }

    public async Task<string> GetCurrentUserMobile()
    {
        return (await appDbContext.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == httpContext.GetUserId()))!.Mobile;
    }
}