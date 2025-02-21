﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MusicStoreApp1.Data;

#nullable disable

namespace MusicStoreApp1.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20241013062346_AddReservedByToVinylRecord")]
    partial class AddReservedByToVinylRecord
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.10");

            modelBuilder.Entity("MusicStoreApp1.Models.VinylRecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("AlbumName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ArtistName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("CostPrice")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("DiscountPercentage")
                        .HasColumnType("TEXT");

                    b.Property<string>("Genre")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsOnPromotion")
                        .HasColumnType("INTEGER");

                    b.Property<int>("NumberOfTracks")
                        .HasColumnType("INTEGER");

                    b.Property<string>("PublisherName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("QuantitySold")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ReleaseYear")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ReservedBy")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("SalePrice")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("VinylRecords");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AlbumName = "Thriller",
                            ArtistName = "Michael Jackson",
                            CostPrice = 10.00m,
                            DiscountPercentage = 0m,
                            Genre = "Pop",
                            IsOnPromotion = false,
                            NumberOfTracks = 9,
                            PublisherName = "Epic",
                            QuantitySold = 0,
                            ReleaseYear = 1982,
                            SalePrice = 15.00m
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
