using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MoneyManagementApp.Models
{
    public partial class MoneyManagementContext : DbContext
    {
        public MoneyManagementContext()
        {
        }

        public MoneyManagementContext(DbContextOptions<MoneyManagementContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cate> Cates { get; set; } = null!;
        public virtual DbSet<Maccount> Maccounts { get; set; } = null!;
        public virtual DbSet<Mess> Messes { get; set; } = null!;
        public virtual DbSet<Saver> Savers { get; set; } = null!;
        public virtual DbSet<Transction> Transctions { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", true, true)
               .Build();
            optionsBuilder.UseSqlServer(config.GetConnectionString("MoneyManagementDB"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cate>(entity =>
            {
                entity.ToTable("Cate");

                entity.Property(e => e.CateId).HasColumnName("cateId");

                entity.Property(e => e.CateName)
                    .HasMaxLength(50)
                    .HasColumnName("cateName");

                entity.Property(e => e.Color).HasColumnName("color");

                entity.Property(e => e.Icon).HasColumnName("icon");

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<Maccount>(entity =>
            {
                entity.HasKey(e => e.AccountId)
                    .HasName("PK__Account__F267251E5B87EEE0");

                entity.ToTable("MAccount");

                entity.Property(e => e.AccountId).HasColumnName("accountId");

                entity.Property(e => e.AccountName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("accountName");

                entity.Property(e => e.Color).HasColumnName("color");

                entity.Property(e => e.Icon).HasColumnName("icon");

                entity.Property(e => e.Money)
                    .HasColumnType("money")
                    .HasColumnName("money");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Maccounts)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Account__userId__3F466844");
            });

            modelBuilder.Entity<Mess>(entity =>
            {
                entity.ToTable("Mess");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Datetime)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Note).HasColumnName("note");

                entity.Property(e => e.UserId1).HasColumnName("userId1");

                entity.Property(e => e.UserId2).HasColumnName("userId2");

                entity.HasOne(d => d.UserId1Navigation)
                    .WithMany(p => p.MessUserId1Navigations)
                    .HasForeignKey(d => d.UserId1)
                    .HasConstraintName("FK__Mesage__userId1__48CFD27E");

                entity.HasOne(d => d.UserId2Navigation)
                    .WithMany(p => p.MessUserId2Navigations)
                    .HasForeignKey(d => d.UserId2)
                    .HasConstraintName("FK__Mesage__userId2__49C3F6B7");
            });

            modelBuilder.Entity<Saver>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__User__CB9A1CFF18B70A0A");

                entity.ToTable("Saver");

                entity.HasIndex(e => e.Email, "UQ__User__AB6E616427399406")
                    .IsUnique();

                entity.HasIndex(e => e.Username, "UQ__User__F3DBC57284F3D217")
                    .IsUnique();

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.Property(e => e.Avatar)
                    .HasColumnType("image")
                    .HasColumnName("avatar");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.Password)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("username");
            });

            modelBuilder.Entity<Transction>(entity =>
            {
                entity.ToTable("Transction");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AccountId).HasColumnName("accountId");

                entity.Property(e => e.CateId).HasColumnName("cateId");

                entity.Property(e => e.Datetime)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Money)
                    .HasColumnType("money")
                    .HasColumnName("money");

                entity.Property(e => e.Note).HasColumnName("note");

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Transctions)
                    .HasForeignKey(d => d.AccountId)
                    .HasConstraintName("FK__Transctio__accou__440B1D61");

                entity.HasOne(d => d.Cate)
                    .WithMany(p => p.Transctions)
                    .HasForeignKey(d => d.CateId)
                    .HasConstraintName("FK__Transctio__cateI__4316F928");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Transctions)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Transctio__userI__4222D4EF");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
