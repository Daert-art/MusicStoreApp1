using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStoreApp1.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }  // Ім'я клієнта
        public int QuantityReserved { get; set; }  // Кількість зарезервованих платівок
        public int VinylRecordId { get; set; }  // Посилання на платівку
    }

}
