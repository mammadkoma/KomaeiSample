using System;
using System.Collections.Generic;

namespace KomaeiSample.Server.Data.Entities;

public partial class UserReward
{
    public int Id { get; set; }

    public Guid ReferrerUserId { get; set; }

    public Guid ReferredUserId { get; set; }

    public int UserRewardTypeId { get; set; }

    public decimal Amount { get; set; }

    public string? Desc { get; set; }

    public DateTime AddDate { get; set; }

    public virtual User ReferredUser { get; set; } = null!;

    public virtual User ReferrerUser { get; set; } = null!;

    public virtual UserRewardType UserRewardType { get; set; } = null!;
}
