using System;
using System.Collections.Generic;

namespace KomaeiSample.Server.Data.Entities;

public partial class EnvelopeOffice
{
    public int Id { get; set; }

    public int ModelId { get; set; }

    public int PaperId { get; set; }

    public int GrammageId { get; set; }

    public int CountId { get; set; }

    public byte HasInternalTeram { get; set; }

    public byte HasDoorGlue { get; set; }

    public decimal Price { get; set; }

    public bool Disable { get; set; }

    public Guid AddUserId { get; set; }

    public DateTime AddDate { get; set; }

    public Guid? UpdateUserId { get; set; }

    public DateTime? UpdateDate { get; set; }

    public virtual User AddUser { get; set; } = null!;

    public virtual Count Count { get; set; } = null!;

    public virtual Grammage Grammage { get; set; } = null!;

    public virtual Model Model { get; set; } = null!;

    public virtual Paper Paper { get; set; } = null!;

    public virtual User? UpdateUser { get; set; }
}
