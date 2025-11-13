namespace KomaeiSample.Client.Components;
public partial class ReferralCode
{
    UserRewardProfileDto _userRewardProfileDto = new UserRewardProfileDto();
    UserRewardDto[] _referredUsers = Array.Empty<UserRewardDto>();

    protected override async Task OnInitializedAsync()
    {
        _userRewardProfileDto = (await http.GetFromJsonAsync<UserRewardProfileDto>("UserReward/GetUserRewardsForProfile"))!;
        await Task.CompletedTask;
        if (_userRewardProfileDto.UserRewardDtos?.Length > 0)
        {
            _referredUsers = _userRewardProfileDto.UserRewardDtos.Where(x => x.UserRewardTypeId == UserRewardTypes.Register.ToInt()).ToArray();
        }
    }
}