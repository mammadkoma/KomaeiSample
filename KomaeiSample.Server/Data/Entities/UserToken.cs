using System;
using System.Collections.Generic;

namespace KomaeiSample.Server.Data.Entities;

public partial class UserToken
{
    public int Id { get; set; }

    public Guid UserId { get; set; }

    public string Token { get; set; } = null!;

    public DateTime ExpireDate { get; set; }

    public virtual User User { get; set; } = null!;
}
