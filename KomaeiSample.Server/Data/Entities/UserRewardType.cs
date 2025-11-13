using System;
using System.Collections.Generic;

namespace KomaeiSample.Server.Data.Entities;

public partial class UserRewardType
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public virtual ICollection<UserReward> UserRewards { get; set; } = new List<UserReward>();
}
