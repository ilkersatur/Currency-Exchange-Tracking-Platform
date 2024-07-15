using BussinessAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BussinessAPI.DAL
{
    public class CachingDataDBContext : DbContext
    {
        public CachingDataDBContext()
        {

        }
        public CachingDataDBContext(DbContextOptions<CachingDataDBContext> options) : base(options) {

        }
        public virtual DbSet<Centralbankofturkey> Centralbankofturkeys { get; set; } = null!;
        public virtual DbSet<Currency> Dailyrecords { get; set; } = null!;


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySQL("Server=localhost;Database=currencydb;Uid=root;Pwd=1230");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Centralbankofturkey>(entity =>
            {
                entity.HasKey(e => e.Currencyid)
                    .HasName("PRIMARY");

                entity.ToTable("centralbankofturkey");

                entity.Property(e => e.Currencyname).HasMaxLength(40);
            });

            modelBuilder.Entity<Currency>(entity =>
            {
                entity.ToTable("dailyrecords");

                entity.HasIndex(e => e.Currencyid, "Currencyid");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Exchangedate).HasColumnType("datetime");

                entity.Property(e => e.Forexselling).HasPrecision(13, 4);

                entity.HasOne(d => d.Currencies)
                    .WithMany(p => p.Dailyrecords)
                    .HasForeignKey(d => d.Currencyid)
                    .HasConstraintName("usdtry_ibfk_1");
            });

        }

    }
}