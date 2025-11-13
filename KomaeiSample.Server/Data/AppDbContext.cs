using System;
using System.Collections.Generic;
using KomaeiSample.Server.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace KomaeiSample.Server.Data;

public partial class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AddressType> AddressTypes { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Cellophane> Cellophanes { get; set; }

    public virtual DbSet<Count> Counts { get; set; }

    public virtual DbSet<DeliveryMethod> DeliveryMethods { get; set; }

    public virtual DbSet<Discount> Discounts { get; set; }

    public virtual DbSet<EnvelopeConfidential> EnvelopeConfidentials { get; set; }

    public virtual DbSet<EnvelopeHandBag> EnvelopeHandBags { get; set; }

    public virtual DbSet<EnvelopeHospital> EnvelopeHospitals { get; set; }

    public virtual DbSet<EnvelopeOffice> EnvelopeOffices { get; set; }

    public virtual DbSet<Grammage> Grammages { get; set; }

    public virtual DbSet<HospitalTemplate> HospitalTemplates { get; set; }

    public virtual DbSet<Model> Models { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderStatus> OrderStatuses { get; set; }

    public virtual DbSet<Paper> Papers { get; set; }

    public virtual DbSet<Setting> Settings { get; set; }

    public virtual DbSet<Slider> Sliders { get; set; }

    public virtual DbSet<TehranArea> TehranAreas { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserAddress> UserAddresses { get; set; }

    public virtual DbSet<UserDiscount> UserDiscounts { get; set; }

    public virtual DbSet<UserReward> UserRewards { get; set; }

    public virtual DbSet<UserRewardType> UserRewardTypes { get; set; }

    public virtual DbSet<UserToken> UserTokens { get; set; }

    public virtual DbSet<Uv> Uvs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AddressType>(entity =>
        {
            entity.ToTable("AddressType");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Title).HasMaxLength(20);
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Category__3214EC0752E977DF");

            entity.ToTable("Category");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.FileExtension)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.FileName)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Title).HasMaxLength(50);
        });

        modelBuilder.Entity<Cellophane>(entity =>
        {
            entity.ToTable("Cellophane");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Title).HasMaxLength(20);
        });

        modelBuilder.Entity<Count>(entity =>
        {
            entity.ToTable("Count");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Category).WithMany(p => p.Counts)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Count_Category");
        });

        modelBuilder.Entity<DeliveryMethod>(entity =>
        {
            entity.ToTable("DeliveryMethod");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Title).HasMaxLength(50);
        });

        modelBuilder.Entity<Discount>(entity =>
        {
            entity.ToTable("Discount");

            entity.Property(e => e.AddDate)
                .HasPrecision(2)
                .HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Code).HasMaxLength(20);
            entity.Property(e => e.MaxPrice).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.UpdateDate).HasPrecision(2);

            entity.HasOne(d => d.AddUser).WithMany(p => p.DiscountAddUsers)
                .HasForeignKey(d => d.AddUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Discount_User");

            entity.HasOne(d => d.Category).WithMany(p => p.Discounts)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Discount_Category");

            entity.HasOne(d => d.UpdateUser).WithMany(p => p.DiscountUpdateUsers)
                .HasForeignKey(d => d.UpdateUserId)
                .HasConstraintName("FK_Discount_User1");
        });

        modelBuilder.Entity<EnvelopeConfidential>(entity =>
        {
            entity.ToTable("EnvelopeConfidential");

            entity.Property(e => e.AddDate)
                .HasPrecision(2)
                .HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.UpdateDate).HasPrecision(2);

            entity.HasOne(d => d.AddUser).WithMany(p => p.EnvelopeConfidentialAddUsers)
                .HasForeignKey(d => d.AddUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EnvelopeConfidential_User_Add");

            entity.HasOne(d => d.Count).WithMany(p => p.EnvelopeConfidentials)
                .HasForeignKey(d => d.CountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EnvelopeConfidential_Count");

            entity.HasOne(d => d.Grammage).WithMany(p => p.EnvelopeConfidentials)
                .HasForeignKey(d => d.GrammageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EnvelopeConfidential_Grammage");

            entity.HasOne(d => d.Model).WithMany(p => p.EnvelopeConfidentials)
                .HasForeignKey(d => d.ModelId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EnvelopeConfidential_Model");

            entity.HasOne(d => d.Paper).WithMany(p => p.EnvelopeConfidentials)
                .HasForeignKey(d => d.PaperId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EnvelopeConfidential_Paper");

            entity.HasOne(d => d.UpdateUser).WithMany(p => p.EnvelopeConfidentialUpdateUsers)
                .HasForeignKey(d => d.UpdateUserId)
                .HasConstraintName("FK_EnvelopeConfidential_User_Update");
        });

        modelBuilder.Entity<EnvelopeHandBag>(entity =>
        {
            entity.ToTable("EnvelopeHandBag");

            entity.Property(e => e.AddDate)
                .HasPrecision(2)
                .HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.UpdateDate).HasPrecision(2);

            entity.HasOne(d => d.AddUser).WithMany(p => p.EnvelopeHandBagAddUsers)
                .HasForeignKey(d => d.AddUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EnvelopeHandBag_User");

            entity.HasOne(d => d.Cellophane).WithMany(p => p.EnvelopeHandBags)
                .HasForeignKey(d => d.CellophaneId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EnvelopeHandBag_Cellophane");

            entity.HasOne(d => d.Count).WithMany(p => p.EnvelopeHandBags)
                .HasForeignKey(d => d.CountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EnvelopeHandBag_Count");

            entity.HasOne(d => d.Grammage).WithMany(p => p.EnvelopeHandBags)
                .HasForeignKey(d => d.GrammageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EnvelopeHandBag_Grammage");

            entity.HasOne(d => d.Model).WithMany(p => p.EnvelopeHandBags)
                .HasForeignKey(d => d.ModelId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EnvelopeHandBag_Model");

            entity.HasOne(d => d.Paper).WithMany(p => p.EnvelopeHandBags)
                .HasForeignKey(d => d.PaperId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EnvelopeHandBag_Paper");

            entity.HasOne(d => d.UpdateUser).WithMany(p => p.EnvelopeHandBagUpdateUsers)
                .HasForeignKey(d => d.UpdateUserId)
                .HasConstraintName("FK_EnvelopeHandBag_User1");

            entity.HasOne(d => d.Uv).WithMany(p => p.EnvelopeHandBags)
                .HasForeignKey(d => d.UvId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EnvelopeHandBag_Uv");
        });

        modelBuilder.Entity<EnvelopeHospital>(entity =>
        {
            entity.ToTable("EnvelopeHospital");

            entity.HasIndex(e => new { e.ModelId, e.HospitalTemplateId, e.PaperId, e.GrammageId, e.CountId, e.CellophaneId, e.UvId }, "IX_EnvelopeHospital").IsUnique();

            entity.Property(e => e.AddDate)
                .HasPrecision(2)
                .HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.UpdateDate).HasPrecision(2);

            entity.HasOne(d => d.AddUser).WithMany(p => p.EnvelopeHospitalAddUsers)
                .HasForeignKey(d => d.AddUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EnvelopeHospital_User");

            entity.HasOne(d => d.Cellophane).WithMany(p => p.EnvelopeHospitals)
                .HasForeignKey(d => d.CellophaneId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EnvelopeHospital_Cellophane");

            entity.HasOne(d => d.Count).WithMany(p => p.EnvelopeHospitals)
                .HasForeignKey(d => d.CountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EnvelopeHospital_Count");

            entity.HasOne(d => d.Grammage).WithMany(p => p.EnvelopeHospitals)
                .HasForeignKey(d => d.GrammageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EnvelopeHospital_Grammage");

            entity.HasOne(d => d.HospitalTemplate).WithMany(p => p.EnvelopeHospitals)
                .HasForeignKey(d => d.HospitalTemplateId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EnvelopeHospital_HospitalTemplate");

            entity.HasOne(d => d.Model).WithMany(p => p.EnvelopeHospitals)
                .HasForeignKey(d => d.ModelId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EnvelopeHospital_Model");

            entity.HasOne(d => d.Paper).WithMany(p => p.EnvelopeHospitals)
                .HasForeignKey(d => d.PaperId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EnvelopeHospital_Paper");

            entity.HasOne(d => d.UpdateUser).WithMany(p => p.EnvelopeHospitalUpdateUsers)
                .HasForeignKey(d => d.UpdateUserId)
                .HasConstraintName("FK_EnvelopeHospital_User1");

            entity.HasOne(d => d.Uv).WithMany(p => p.EnvelopeHospitals)
                .HasForeignKey(d => d.UvId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EnvelopeHospital_Uv");
        });

        modelBuilder.Entity<EnvelopeOffice>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Envelope");

            entity.ToTable("EnvelopeOffice");

            entity.HasIndex(e => new { e.ModelId, e.PaperId, e.GrammageId, e.CountId, e.HasInternalTeram, e.HasDoorGlue }, "IX_EnvelopeOffice").IsUnique();

            entity.Property(e => e.AddDate)
                .HasPrecision(2)
                .HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.UpdateDate).HasPrecision(2);

            entity.HasOne(d => d.AddUser).WithMany(p => p.EnvelopeOfficeAddUsers)
                .HasForeignKey(d => d.AddUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EnvelopeOffice_User_Add");

            entity.HasOne(d => d.Count).WithMany(p => p.EnvelopeOffices)
                .HasForeignKey(d => d.CountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EnvelopeOffice_Count");

            entity.HasOne(d => d.Grammage).WithMany(p => p.EnvelopeOffices)
                .HasForeignKey(d => d.GrammageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EnvelopeOffice_Grammage");

            entity.HasOne(d => d.Model).WithMany(p => p.EnvelopeOffices)
                .HasForeignKey(d => d.ModelId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EnvelopeOffice_Model");

            entity.HasOne(d => d.Paper).WithMany(p => p.EnvelopeOffices)
                .HasForeignKey(d => d.PaperId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EnvelopeOffice_Paper");

            entity.HasOne(d => d.UpdateUser).WithMany(p => p.EnvelopeOfficeUpdateUsers)
                .HasForeignKey(d => d.UpdateUserId)
                .HasConstraintName("FK_EnvelopeOffice_User_Update");
        });

        modelBuilder.Entity<Grammage>(entity =>
        {
            entity.ToTable("Grammage");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Title).HasMaxLength(50);

            entity.HasOne(d => d.Category).WithMany(p => p.Grammages)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Grammage_Category");
        });

        modelBuilder.Entity<HospitalTemplate>(entity =>
        {
            entity.ToTable("HospitalTemplate");

            entity.Property(e => e.FileName)
                .HasMaxLength(45)
                .IsUnicode(false);
            entity.Property(e => e.TemplateName).HasMaxLength(50);

            entity.HasOne(d => d.Model).WithMany(p => p.HospitalTemplates)
                .HasForeignKey(d => d.ModelId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HospitalTemplate_Model");
        });

        modelBuilder.Entity<Model>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_EnvelopeModel");

            entity.ToTable("Model");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.FileExtension)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.FileName)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Title).HasMaxLength(50);

            entity.HasOne(d => d.Category).WithMany(p => p.Models)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Model_Category");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_OrderOffice");

            entity.ToTable("Order");

            entity.Property(e => e.AddDate)
                .HasPrecision(2)
                .HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Address).HasMaxLength(400);
            entity.Property(e => e.Desc).HasMaxLength(100);
            entity.Property(e => e.FileExtension)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.FileName)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.PayDate).HasPrecision(2);
            entity.Property(e => e.PostalCode)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.PriceAfterDiscount).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.UpdateDate).HasPrecision(2);

            entity.HasOne(d => d.AddUser).WithMany(p => p.OrderAddUsers)
                .HasForeignKey(d => d.AddUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Order_User");

            entity.HasOne(d => d.AddressType).WithMany(p => p.Orders)
                .HasForeignKey(d => d.AddressTypeId)
                .HasConstraintName("FK_Order_AddressType");

            entity.HasOne(d => d.Category).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Order_Category");

            entity.HasOne(d => d.Cellophane).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CellophaneId)
                .HasConstraintName("FK_Order_Cellophane");

            entity.HasOne(d => d.DeliveryMethod).WithMany(p => p.Orders)
                .HasForeignKey(d => d.DeliveryMethodId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderOffice_DeliveryMethod");

            entity.HasOne(d => d.Discount).WithMany(p => p.Orders)
                .HasForeignKey(d => d.DiscountId)
                .HasConstraintName("FK_Order_Discount");

            entity.HasOne(d => d.OrderStatus).WithMany(p => p.Orders)
                .HasForeignKey(d => d.OrderStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Order_OrderStatus");

            entity.HasOne(d => d.TehranArea).WithMany(p => p.Orders)
                .HasForeignKey(d => d.TehranAreaId)
                .HasConstraintName("FK_Order_TehranArea");

            entity.HasOne(d => d.UpdateUser).WithMany(p => p.OrderUpdateUsers)
                .HasForeignKey(d => d.UpdateUserId)
                .HasConstraintName("FK_Order_User1");

            entity.HasOne(d => d.Uv).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UvId)
                .HasConstraintName("FK_Order_Uv");
        });

        modelBuilder.Entity<OrderStatus>(entity =>
        {
            entity.ToTable("OrderStatus");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Title).HasMaxLength(30);
        });

        modelBuilder.Entity<Paper>(entity =>
        {
            entity.ToTable("Paper");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Title).HasMaxLength(50);

            entity.HasOne(d => d.Category).WithMany(p => p.Papers)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Paper_Category");
        });

        modelBuilder.Entity<Setting>(entity =>
        {
            entity.ToTable("Setting");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Key)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.KeyFa).HasMaxLength(100);
        });

        modelBuilder.Entity<Slider>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Slider");

            entity.Property(e => e.FileName)
                .HasMaxLength(45)
                .IsUnicode(false);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Link)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TehranArea>(entity =>
        {
            entity.ToTable("TehranArea");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Title).HasMaxLength(20);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.AddDate)
                .HasPrecision(2)
                .HasDefaultValueSql("(getdate())");
            entity.Property(e => e.FullName).HasMaxLength(30);
            entity.Property(e => e.LogoFileName)
                .HasMaxLength(45)
                .IsUnicode(false);
            entity.Property(e => e.Mobile)
                .HasMaxLength(11)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.UpdateDate).HasPrecision(2);
            entity.Property(e => e.WalletBalance).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.ReferrerUser).WithMany(p => p.InverseReferrerUser)
                .HasForeignKey(d => d.ReferrerUserId)
                .HasConstraintName("FK_User_User");
        });

        modelBuilder.Entity<UserAddress>(entity =>
        {
            entity.ToTable("UserAddress");

            entity.Property(e => e.Address).HasMaxLength(500);
            entity.Property(e => e.Title).HasMaxLength(50);

            entity.HasOne(d => d.User).WithMany(p => p.UserAddresses)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserAddress_User");
        });

        modelBuilder.Entity<UserDiscount>(entity =>
        {
            entity.ToTable("UserDiscount");

            entity.HasOne(d => d.Discount).WithMany(p => p.UserDiscounts)
                .HasForeignKey(d => d.DiscountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserDiscount_Discount");

            entity.HasOne(d => d.User).WithMany(p => p.UserDiscounts)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserDiscount_User");
        });

        modelBuilder.Entity<UserReward>(entity =>
        {
            entity.ToTable("UserReward");

            entity.Property(e => e.AddDate)
                .HasPrecision(2)
                .HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Amount).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.Desc).HasMaxLength(200);

            entity.HasOne(d => d.ReferredUser).WithMany(p => p.UserRewardReferredUsers)
                .HasForeignKey(d => d.ReferredUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserReward_User2");

            entity.HasOne(d => d.ReferrerUser).WithMany(p => p.UserRewardReferrerUsers)
                .HasForeignKey(d => d.ReferrerUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserReward_User1");

            entity.HasOne(d => d.UserRewardType).WithMany(p => p.UserRewards)
                .HasForeignKey(d => d.UserRewardTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserReward_UserRewardType");
        });

        modelBuilder.Entity<UserRewardType>(entity =>
        {
            entity.ToTable("UserRewardType");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Title).HasMaxLength(50);
        });

        modelBuilder.Entity<UserToken>(entity =>
        {
            entity.ToTable("UserToken");

            entity.Property(e => e.ExpireDate).HasPrecision(2);
            entity.Property(e => e.Token)
                .HasMaxLength(300)
                .IsUnicode(false);

            entity.HasOne(d => d.User).WithMany(p => p.UserTokens)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserToken_User");
        });

        modelBuilder.Entity<Uv>(entity =>
        {
            entity.ToTable("Uv");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Title).HasMaxLength(20);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
