using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStoreApp1.Models
{
    public class SalesStatistics
    {
        public int TotalVinylRecordsSold { get; set; }
        public decimal TotalRevenue { get; set; }

        // Властивість лише для читання, що обчислюється автоматично
        public decimal AveragePricePerSale
        {
            get
            {
                return TotalVinylRecordsSold > 0 ? Math.Round(TotalRevenue / TotalVinylRecordsSold, 2) : 0;
            }
        }
    }


}
