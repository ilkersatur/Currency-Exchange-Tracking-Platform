﻿// <auto-generated />
using System;
using DataAPI.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DataAPI.Migrations
{
    [DbContext(typeof(DailyDBContext))]
    partial class currencydbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseCollation("utf8mb4_0900_ai_ci")
                .HasAnnotation("ProductVersion", "6.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.HasCharSet(modelBuilder, "utf8mb4");

            modelBuilder.Entity("DataAPI.Models.Centralbankofturkey", b =>
                {
                    b.Property<int>("Currencyid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Currencyname")
                        .HasMaxLength(40)
                        .HasColumnType("varchar(40)");

                    b.HasKey("Currencyid")
                        .HasName("PRIMARY");

                    b.ToTable("centralbankofturkey", (string)null);

                    b.HasData(
                        new
                        {
                            Currencyid = 1,
                            Currencyname = "USD"
                        },
                        new
                        {
                            Currencyid = 2,
                            Currencyname = "EUR"
                        },
                        new
                        {
                            Currencyid = 3,
                            Currencyname = "GBP"
                        });
                });

            modelBuilder.Entity("DataAPI.Models.DailyRecords", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<int?>("Currencyid")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Exchangedate")
                        .HasColumnType("datetime");

                    b.Property<decimal?>("Forexselling")
                        .HasPrecision(13, 4)
                        .HasColumnType("decimal(13,4)");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "Currencyid" }, "Currencyid");

                    b.ToTable("dailyrecords", (string)null);
                });

            modelBuilder.Entity("DataAPI.Models.DailyRecords", b =>
                {
                    b.HasOne("DataAPI.Models.Centralbankofturkey", "Currency")
                        .WithMany("DailyRecordss")
                        .HasForeignKey("Currencyid")
                        .HasConstraintName("usdtry_ibfk_1");

                    b.Navigation("Currency");
                });

            modelBuilder.Entity("DataAPI.Models.Centralbankofturkey", b =>
                {
                    b.Navigation("DailyRecordss");
                });
#pragma warning restore 612, 618
        }
    }
}