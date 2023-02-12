using System;
using System.Collections.Generic;
using DataAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DataAPI.DAL
{
    public partial class DailyDBContext : DbContext
    {
        public DailyDBContext()
        {
        }

        public DailyDBContext(DbContextOptions<DailyDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CentralBankOfTurkey> Centralbankofturkeys { get; set; } = null!;
        public virtual DbSet<DailyRecords> Usdtries { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=localhost;database=currencydb;uid=root;pwd=1230", ServerVersion.Parse("8.0.32-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<CentralBankOfTurkey>().HasData(
                new CentralBankOfTurkey { Currencyid = 1, Currencyname = "USD" },
                new CentralBankOfTurkey { Currencyid = 2, Currencyname = "EUR" },
                new CentralBankOfTurkey { Currencyid = 3, Currencyname = "GBP" });
            modelBuilder.UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<CentralBankOfTurkey>(entity =>
            {
                entity.HasKey(e => e.Currencyid)
                    .HasName("PRIMARY");

                entity.ToTable("centralbankofturkey");

                entity.Property(e => e.Currencyname).HasMaxLength(40);
            });



            modelBuilder.Entity<DailyRecords>(entity =>
            {
                entity.ToTable("dailyrecords");

                entity.HasIndex(e => e.Currencyid, "Currencyid");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Exchangedate).HasColumnType("datetime");

                entity.Property(e => e.Forexselling).HasPrecision(13, 4);

                entity.HasOne(d => d.Currency)
                    .WithMany(p => p.DailyRecordss)
                    .HasForeignKey(d => d.Currencyid)
                    .HasConstraintName("usdtry_ibfk_1");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
