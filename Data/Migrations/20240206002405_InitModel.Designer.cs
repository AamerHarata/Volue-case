﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Volue_case.Data;

#nullable disable

namespace Volue_case.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240206002405_InitModel")]
    partial class InitModel
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Volue_case.Models.Bid", b =>
                {
                    b.Property<string>("ExternalId")
                        .HasColumnType("text");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("DateOfLastChange")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("Day")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Market")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("ExternalId");

                    b.ToTable("Bids");
                });

            modelBuilder.Entity("Volue_case.Models.Entities.Customer", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("Volue_case.Models.Entities.Position", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<decimal>("Quantity")
                        .HasColumnType("numeric");

                    b.Property<string>("SeriesExternalId")
                        .HasColumnType("text");

                    b.Property<string>("SeriesId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("SeriesExternalId");

                    b.HasIndex("SeriesId");

                    b.ToTable("Positions");
                });

            modelBuilder.Entity("Volue_case.Models.Entities.Series", b =>
                {
                    b.Property<string>("ExternalId")
                        .HasColumnType("text");

                    b.Property<string>("AssetId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("BidExternalId")
                        .HasColumnType("text");

                    b.Property<string>("BidId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Currency")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("CustomerId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Direction")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("EndInterval")
                        .HasColumnType("timestamp with time zone");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.Property<string>("PriceArea")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Resolution")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("StartInterval")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("ExternalId");

                    b.HasIndex("BidExternalId");

                    b.HasIndex("BidId");

                    b.HasIndex("CustomerId");

                    b.ToTable("Series");
                });

            modelBuilder.Entity("Volue_case.Models.UpdateHistory", b =>
                {
                    b.Property<string>("BidId")
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("BidExternalId")
                        .HasColumnType("text");

                    b.Property<int>("FromStatus")
                        .HasColumnType("integer");

                    b.Property<int>("ToStatus")
                        .HasColumnType("integer");

                    b.HasKey("BidId", "UpdateTime");

                    b.HasIndex("BidExternalId");

                    b.ToTable("History");
                });

            modelBuilder.Entity("Volue_case.Models.Entities.Position", b =>
                {
                    b.HasOne("Volue_case.Models.Entities.Series", null)
                        .WithMany("Positions")
                        .HasForeignKey("SeriesExternalId");

                    b.HasOne("Volue_case.Models.Entities.Series", "Series")
                        .WithMany()
                        .HasForeignKey("SeriesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Series");
                });

            modelBuilder.Entity("Volue_case.Models.Entities.Series", b =>
                {
                    b.HasOne("Volue_case.Models.Bid", null)
                        .WithMany("Series")
                        .HasForeignKey("BidExternalId");

                    b.HasOne("Volue_case.Models.Bid", "Bid")
                        .WithMany()
                        .HasForeignKey("BidId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Volue_case.Models.Entities.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bid");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("Volue_case.Models.UpdateHistory", b =>
                {
                    b.HasOne("Volue_case.Models.Bid", null)
                        .WithMany("UpdateHistory")
                        .HasForeignKey("BidExternalId");

                    b.HasOne("Volue_case.Models.Bid", "Bid")
                        .WithMany()
                        .HasForeignKey("BidId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bid");
                });

            modelBuilder.Entity("Volue_case.Models.Bid", b =>
                {
                    b.Navigation("Series");

                    b.Navigation("UpdateHistory");
                });

            modelBuilder.Entity("Volue_case.Models.Entities.Series", b =>
                {
                    b.Navigation("Positions");
                });
#pragma warning restore 612, 618
        }
    }
}
