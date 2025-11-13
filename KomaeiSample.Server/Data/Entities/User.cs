using System;
using System.Collections.Generic;

namespace KomaeiSample.Server.Data.Entities;

public partial class User
{
    public Guid Id { get; set; }

    public string? FullName { get; set; }

    public string Mobile { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public int ConfirmCode { get; set; }

    public bool IsConfirmed { get; set; }

    public bool IsAdmin { get; set; }

    public string? LogoFileName { get; set; }

    public Guid? ReferrerUserId { get; set; }

    public decimal WalletBalance { get; set; }

    public DateTime AddDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public virtual ICollection<Discount> DiscountAddUsers { get; set; } = new List<Discount>();

    public virtual ICollection<Discount> DiscountUpdateUsers { get; set; } = new List<Discount>();

    public virtual ICollection<EnvelopeConfidential> EnvelopeConfidentialAddUsers { get; set; } = new List<EnvelopeConfidential>();

    public virtual ICollection<EnvelopeConfidential> EnvelopeConfidentialUpdateUsers { get; set; } = new List<EnvelopeConfidential>();

    public virtual ICollection<EnvelopeHandBag> EnvelopeHandBagAddUsers { get; set; } = new List<EnvelopeHandBag>();

    public virtual ICollection<EnvelopeHandBag> EnvelopeHandBagUpdateUsers { get; set; } = new List<EnvelopeHandBag>();

    public virtual ICollection<EnvelopeHospital> EnvelopeHospitalAddUsers { get; set; } = new List<EnvelopeHospital>();

    public virtual ICollection<EnvelopeHospital> EnvelopeHospitalUpdateUsers { get; set; } = new List<EnvelopeHospital>();

    public virtual ICollection<EnvelopeOffice> EnvelopeOfficeAddUsers { get; set; } = new List<EnvelopeOffice>();

    public virtual ICollection<EnvelopeOffice> EnvelopeOfficeUpdateUsers { get; set; } = new List<EnvelopeOffice>();

    public virtual ICollection<User> InverseReferrerUser { get; set; } = new List<User>();

    public virtual ICollection<Order> OrderAddUsers { get; set; } = new List<Order>();

    public virtual ICollection<Order> OrderUpdateUsers { get; set; } = new List<Order>();

    public virtual User? ReferrerUser { get; set; }

    public virtual ICollection<UserAddress> UserAddresses { get; set; } = new List<UserAddress>();

    public virtual ICollection<UserDiscount> UserDiscounts { get; set; } = new List<UserDiscount>();

    public virtual ICollection<UserReward> UserRewardReferredUsers { get; set; } = new List<UserReward>();

    public virtual ICollection<UserReward> UserRewardReferrerUsers { get; set; } = new List<UserReward>();

    public virtual ICollection<UserToken> UserTokens { get; set; } = new List<UserToken>();
}
