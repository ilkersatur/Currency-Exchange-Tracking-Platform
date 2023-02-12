using DataAPI.DAL;
using DataAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace DataAPI.DAL
{

    public class CurrencyDBContext : DbContext
    {
        public CurrencyDBContext()
        {

        }
        public CurrencyDBContext(DbContextOptions<CurrencyDBContext> options) : base(options)
        {
            try
            {
                var databaseCreator = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
                if (databaseCreator != null)

                {
                    if (!databaseCreator.CanConnect()) databaseCreator.Create();
                    if (!databaseCreator.HasTables()) databaseCreator.CreateTables();
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }


        }

        public virtual DbSet<CentralBankOfTurkey> Centralbankofturkeys { get; set; } = null!;
        public virtual DbSet<DailyRecords> DailyRecordss { get; set; } = null!;

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
        }

    }
}