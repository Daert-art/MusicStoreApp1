﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MusicStoreApp1.Data;

#nullable disable

namespace MusicStoreApp1.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.10");

            modelBuilder.Entity("MusicStoreApp1.Models.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("TotalPurchases")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("MusicStoreApp1.Models.Reservation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CustomerName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("QuantityReserved")
                        .HasColumnType("INTEGER");

                    b.Property<int>("VinylRecordId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("VinylRecordId");

                    b.ToTable("Reservations");
                });

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

                    b.Property<bool>("IsAutoPromotion")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsOnPromotion")
                        .HasColumnType("INTEGER");

                    b.Property<int>("NumberOfTracks")
                        .HasColumnType("INTEGER");

                    b.Property<string>("PublisherName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("QuantityInStock")
                        .HasColumnType("INTEGER");

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
                            IsAutoPromotion = false,
                            IsOnPromotion = false,
                            NumberOfTracks = 9,
                            PublisherName = "Epic",
                            QuantityInStock = 0,
                            QuantitySold = 0,
                            ReleaseYear = 1982,
                            SalePrice = 15.00m
                        });
                });

            modelBuilder.Entity("MusicStoreApp1.Models.Reservation", b =>
                {
                    b.HasOne("MusicStoreApp1.Models.VinylRecord", null)
                        .WithMany("Reservations")
                        .HasForeignKey("VinylRecordId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MusicStoreApp1.Models.VinylRecord", b =>
                {
                    b.Navigation("Reservations");
                });
#pragma warning restore 612, 618
        }
    }
}
