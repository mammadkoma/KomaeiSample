namespace KomaeiSample.Server.Controllers;
public class UserRewardController(UserRewardService userRewardService) : AppController
{
    [HttpGet]
    public async Task<ActionResult> GetUserRewardsForProfile()
    {
        return Ok(await userRewardService.GetUserRewardsForProfile());
    }
}