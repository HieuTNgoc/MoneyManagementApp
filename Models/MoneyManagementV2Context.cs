using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MoneyManagementApp.Models
{
    public partial class MoneyManagementV2Context : DbContext
    {
        public MoneyManagementV2Context()
        {
        }

        public MoneyManagementV2Context(DbContextOptions<MoneyManagementV2Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Cate> Cates { get; set; } = null!;
        public virtual DbSet<Maccount> Maccounts { get; set; } = null!;
        public virtual DbSet<Mesage> Mesages { get; set; } = null!;
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

                entity.Property(e => e.Color)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("color");

                entity.Property(e => e.Icon)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("icon");

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<Maccount>(entity =>
            {
                entity.HasKey(e => e.AccountId)
                    .HasName("PK__MAccount__F267251E3679AFD5");

                entity.ToTable("MAccount");

                entity.Property(e => e.AccountId).HasColumnName("accountId");

                entity.Property(e => e.AccountName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("accountName");

                entity.Property(e => e.Color)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("color");

                entity.Property(e => e.Icon)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("icon");

                entity.Property(e => e.Money)
                    .HasColumnType("money")
                    .HasColumnName("money");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Maccounts)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__MAccount__userId__3E52440B");
            });

            modelBuilder.Entity<Mesage>(entity =>
            {
                entity.ToTable("Mesage");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Datetime)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Note).HasColumnName("note");

                entity.Property(e => e.UserId1).HasColumnName("userId1");

                entity.Property(e => e.UserId2).HasColumnName("userId2");

                entity.HasOne(d => d.UserId1Navigation)
                    .WithMany(p => p.MesageUserId1Navigations)
                    .HasForeignKey(d => d.UserId1)
                    .HasConstraintName("FK__Mesage__userId1__47DBAE45");

                entity.HasOne(d => d.UserId2Navigation)
                    .WithMany(p => p.MesageUserId2Navigations)
                    .HasForeignKey(d => d.UserId2)
                    .HasConstraintName("FK__Mesage__userId2__48CFD27E");
            });

            modelBuilder.Entity<Saver>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__Saver__CB9A1CFFE00C4663");

                entity.ToTable("Saver");

                entity.HasIndex(e => e.Email, "UQ__Saver__AB6E616431A8229C")
                    .IsUnique();

                entity.HasIndex(e => e.Username, "UQ__Saver__F3DBC5728ABBAB72")
                    .IsUnique();

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.Property(e => e.Avatar).HasColumnName("avatar");

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
                    .HasConstraintName("FK__Transctio__accou__4316F928");

                entity.HasOne(d => d.Cate)
                    .WithMany(p => p.Transctions)
                    .HasForeignKey(d => d.CateId)
                    .HasConstraintName("FK__Transctio__cateI__4222D4EF");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Transctions)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Transctio__userI__412EB0B6");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
