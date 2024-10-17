using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStoreApp1.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public decimal TotalPurchases { get; set; }  // Загальна сума покупок
        public decimal DiscountPercentage => TotalPurchases >= 1000 ? 10 : 5;  // Проста система знижок
    }
}
