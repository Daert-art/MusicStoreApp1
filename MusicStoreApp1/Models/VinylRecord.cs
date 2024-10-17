using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStoreApp1.Models
{
    public class VinylRecord
    {
        public int Id { get; set; }
        public string AlbumName { get; set; }
        public string ArtistName { get; set; }
        public string PublisherName { get; set; }
        public int NumberOfTracks { get; set; }
        public string Genre { get; set; }
        public int ReleaseYear { get; set; }
        public decimal CostPrice { get; set; }
        public decimal SalePrice { get; set; }
        public bool IsOnPromotion { get; set; }
        public decimal DiscountPercentage { get; set; }
        public int QuantitySold { get; set; }
        public int QuantityInStock { get; set; } // Загальна кількість платівок в наявності
        public List<Reservation>? Reservations { get; set; } = new List<Reservation>();  // Список резервувань
        public int? TotalReservedQuantity => Reservations.Sum(r => r.QuantityReserved);
        public bool IsAutoPromotion { get; set; }
        public decimal FinalPrice
        {
            get
            {
                decimal finalPrice = IsOnPromotion ? SalePrice * (1 - DiscountPercentage / 100) : SalePrice;
                return Math.Round(finalPrice, 2);
            }
        }
        public string? ReservedBy { get; set; } // Ім'я клієнта, який резервує платівку
    }
}
