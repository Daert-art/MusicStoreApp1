using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MusicStoreApp1.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStoreApp1.Data
{
    // Контекст бази даних для додатку "Music Store".
    // Наслідується від DbContext, що забезпечує роботу з базою даних через ORM (Entity Framework Core).

    public class AppDbContext : DbContext
    {
        // Властивість для доступу до таблиці платівок (VinylRecord).
        public DbSet<VinylRecord> VinylRecords { get; set; }

        // Властивість для доступу до таблиці резервувань (Reservation).
        public DbSet<Reservation> Reservations { get; set; }

        // Властивість для доступу до таблиці клієнтів (Customer).
        public DbSet<Customer> Customers { get; set; }

        // Метод для налаштування підключення до бази даних.
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string dbFolder = Path.Combine(localAppData, "MusicStoreApp1");

            // Создаем папку, если она не существует
            if (!Directory.Exists(dbFolder))
            {
                Directory.CreateDirectory(dbFolder);
            }

            string dbPath = Path.Combine(dbFolder, "music.db");
            optionsBuilder.UseSqlite($"Data Source={dbPath}")
                          .LogTo(Console.WriteLine, LogLevel.Information);
        }


        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    // Використовуємо SQLite для зберігання даних. Шлях до бази даних вказано явно.
        //    // Логування SQL-запитів для дебагу виводиться в консоль.
        //    optionsBuilder.UseSqlite("Data Source=C:\\Users\\Laptopchik\\source\\repos\\MusicStoreApp1\\MusicStoreApp1\\music.db")
        //           .LogTo(Console.WriteLine, LogLevel.Information); // Логування SQL-запитів
        //}

        // Метод для налаштування моделей та початкових даних.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Додаємо початкові дані для таблиці VinylRecord.
            // Це необхідно для заповнення бази даних мінімальними даними при створенні (seeding).
            modelBuilder.Entity<VinylRecord>().HasData(
                new VinylRecord
                {
                    Id = 1,
                    AlbumName = "Thriller",
                    ArtistName = "Michael Jackson",
                    PublisherName = "Epic",
                    NumberOfTracks = 9,
                    Genre = "Pop",
                    ReleaseYear = 1982,
                    CostPrice = 10.00m,
                    SalePrice = 15.00m,
                    IsOnPromotion = false
                }
            );
        }

        // Закоментований код для альтернативного способу наповнення таблиці платівок.
        // Метод SeedVinylRecords може бути використаний для додавання даних вручну в базу після її створення.
        // Перевіряється, чи є вже записи в таблиці, і, якщо їх немає, додаються нові.
        public static void SeedVinylRecords(AppDbContext context)
        {
            // Перевіряємо, чи є вже платівки в базі
            if (!context.VinylRecords.Any())
            {
                var vinylRecords = new List<VinylRecord>
                {
                    new VinylRecord { AlbumName = "The Dark Side of the Moon", ArtistName = "Pink Floyd", PublisherName = "Harvest", NumberOfTracks = 10, Genre = "Rock", ReleaseYear = 1973, CostPrice = 15.99m, SalePrice = 19.99m, IsOnPromotion = false, DiscountPercentage = 0, QuantityInStock = 10, QuantitySold = 0 },
                    new VinylRecord { AlbumName = "Abbey Road", ArtistName = "The Beatles", PublisherName = "Apple Records", NumberOfTracks = 17, Genre = "Rock", ReleaseYear = 1969, CostPrice = 18.99m, SalePrice = 24.99m, IsOnPromotion = false, DiscountPercentage = 0, QuantityInStock = 8, QuantitySold = 0 },
                    new VinylRecord { AlbumName = "Thriller", ArtistName = "Michael Jackson", PublisherName = "Epic", NumberOfTracks = 9, Genre = "Pop", ReleaseYear = 1982, CostPrice = 14.99m, SalePrice = 20.99m, IsOnPromotion = false, DiscountPercentage = 0, QuantityInStock = 15, QuantitySold = 0 },
                    new VinylRecord { AlbumName = "Back in Black", ArtistName = "AC/DC", PublisherName = "Atlantic", NumberOfTracks = 10, Genre = "Rock", ReleaseYear = 1980, CostPrice = 16.99m, SalePrice = 22.99m, IsOnPromotion = false, DiscountPercentage = 0, QuantityInStock = 12, QuantitySold = 0 },
                    new VinylRecord { AlbumName = "Rumours", ArtistName = "Fleetwood Mac", PublisherName = "Warner Bros.", NumberOfTracks = 11, Genre = "Rock", ReleaseYear = 1977, CostPrice = 13.99m, SalePrice = 18.99m, IsOnPromotion = false, DiscountPercentage = 0, QuantityInStock = 20, QuantitySold = 0 },
                    new VinylRecord { AlbumName = "Hotel California", ArtistName = "Eagles", PublisherName = "Asylum", NumberOfTracks = 9, Genre = "Rock", ReleaseYear = 1976, CostPrice = 17.99m, SalePrice = 23.99m, IsOnPromotion = false, DiscountPercentage = 0, QuantityInStock = 9, QuantitySold = 0 },
                    new VinylRecord { AlbumName = "Led Zeppelin IV", ArtistName = "Led Zeppelin", PublisherName = "Atlantic", NumberOfTracks = 8, Genre = "Rock", ReleaseYear = 1971, CostPrice = 16.99m, SalePrice = 22.99m, IsOnPromotion = false, DiscountPercentage = 0, QuantityInStock = 11, QuantitySold = 0 },
                    new VinylRecord { AlbumName = "Born in the USA", ArtistName = "Bruce Springsteen", PublisherName = "Columbia", NumberOfTracks = 12, Genre = "Rock", ReleaseYear = 1984, CostPrice = 14.99m, SalePrice = 21.99m, IsOnPromotion = false, DiscountPercentage = 0, QuantityInStock = 10, QuantitySold = 0 },
                    new VinylRecord { AlbumName = "1989", ArtistName = "Taylor Swift", PublisherName = "Big Machine", NumberOfTracks = 13, Genre = "Pop", ReleaseYear = 2014, CostPrice = 15.99m, SalePrice = 22.99m, IsOnPromotion = false, DiscountPercentage = 0, QuantityInStock = 14, QuantitySold = 0 },
                    new VinylRecord { AlbumName = "A Night at the Opera", ArtistName = "Queen", PublisherName = "EMI", NumberOfTracks = 12, Genre = "Rock", ReleaseYear = 1975, CostPrice = 16.99m, SalePrice = 24.99m, IsOnPromotion = false, DiscountPercentage = 0, QuantityInStock = 10, QuantitySold = 0 },
                    new VinylRecord { AlbumName = "Purple Rain", ArtistName = "Prince", PublisherName = "Warner Bros.", NumberOfTracks = 9, Genre = "Pop", ReleaseYear = 1984, CostPrice = 13.99m, SalePrice = 18.99m, IsOnPromotion = false, DiscountPercentage = 0, QuantityInStock = 11, QuantitySold = 0 },
                    new VinylRecord { AlbumName = "Nevermind", ArtistName = "Nirvana", PublisherName = "DGC", NumberOfTracks = 12, Genre = "Grunge", ReleaseYear = 1991, CostPrice = 17.99m, SalePrice = 24.99m, IsOnPromotion = false, DiscountPercentage = 0, QuantityInStock = 10, QuantitySold = 0 },
                    new VinylRecord { AlbumName = "Appetite for Destruction", ArtistName = "Guns N' Roses", PublisherName = "Geffen", NumberOfTracks = 12, Genre = "Rock", ReleaseYear = 1987, CostPrice = 16.99m, SalePrice = 22.99m, IsOnPromotion = false, DiscountPercentage = 0, QuantityInStock = 9, QuantitySold = 0 },
                    new VinylRecord { AlbumName = "The Wall", ArtistName = "Pink Floyd", PublisherName = "Columbia", NumberOfTracks = 26, Genre = "Rock", ReleaseYear = 1979, CostPrice = 19.99m, SalePrice = 27.99m, IsOnPromotion = false, DiscountPercentage = 0, QuantityInStock = 7, QuantitySold = 0 },
                    new VinylRecord { AlbumName = "Sgt. Pepper's Lonely Hearts Club Band", ArtistName = "The Beatles", PublisherName = "Parlophone", NumberOfTracks = 13, Genre = "Rock", ReleaseYear = 1967, CostPrice = 17.99m, SalePrice = 24.99m, IsOnPromotion = false, DiscountPercentage = 0, QuantityInStock = 10, QuantitySold = 0 },
                    new VinylRecord { AlbumName = "The Joshua Tree", ArtistName = "U2", PublisherName = "Island", NumberOfTracks = 11, Genre = "Rock", ReleaseYear = 1987, CostPrice = 15.99m, SalePrice = 22.99m, IsOnPromotion = false, DiscountPercentage = 0, QuantityInStock = 8, QuantitySold = 0 },
                    new VinylRecord { AlbumName = "The Rise and Fall of Ziggy Stardust", ArtistName = "David Bowie", PublisherName = "RCA", NumberOfTracks = 11, Genre = "Rock", ReleaseYear = 1972, CostPrice = 18.99m, SalePrice = 25.99m, IsOnPromotion = false, DiscountPercentage = 0, QuantityInStock = 6, QuantitySold = 0 },
                    new VinylRecord { AlbumName = "In the Court of the Crimson King", ArtistName = "King Crimson", PublisherName = "Atlantic", NumberOfTracks = 5, Genre = "Progressive Rock", ReleaseYear = 1969, CostPrice = 20.99m, SalePrice = 29.99m, IsOnPromotion = false, DiscountPercentage = 0, QuantityInStock = 5, QuantitySold = 0 },
                    new VinylRecord { AlbumName = "The Beatles (White Album)", ArtistName = "The Beatles", PublisherName = "Apple Records", NumberOfTracks = 30, Genre = "Rock", ReleaseYear = 1968, CostPrice = 22.99m, SalePrice = 31.99m, IsOnPromotion = false, DiscountPercentage = 0, QuantityInStock = 4, QuantitySold = 0 },
                    new VinylRecord { AlbumName = "OK Computer", ArtistName = "Radiohead", PublisherName = "Capitol", NumberOfTracks = 12, Genre = "Alternative Rock", ReleaseYear = 1997, CostPrice = 17.99m, SalePrice = 23.99m, IsOnPromotion = false, DiscountPercentage = 0, QuantityInStock = 9, QuantitySold = 0 }
                };


                context.VinylRecords.AddRange(vinylRecords);
                context.SaveChanges();
            }
        }
    }
}
