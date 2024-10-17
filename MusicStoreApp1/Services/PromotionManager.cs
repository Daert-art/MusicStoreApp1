using MusicStoreApp1.Models;
using System.Collections.ObjectModel;

namespace MusicStoreApp1.Services
{
    // Клас PromotionManager керує автоматичним застосуванням знижок до платівок.
    // Реалізує збереження та застосування умов промоакцій на основі жанру, року випуску та проценту знижки.

    public static class PromotionManager
    {
        // Властивості для збереження вибраних умов для промоакції.
        public static string SelectedGenre { get; private set; } // Вибраний жанр для промо
        public static int MinReleaseYear { get; private set; } // Мінімальний рік випуску для застосування промо
        public static decimal DiscountPercentage { get; private set; } // Процент знижки для промо
        public static bool IsAutoPromotionEnabled { get; private set; } // Флаг для увімкнення/вимкнення автопромо

        // Метод для налаштування умов автоматичної промоакції.
        public static void SetAutoPromotionConditions(string genre, int releaseYear, decimal discountPercentage)
        {
            SelectedGenre = genre; // Встановлюємо вибраний жанр
            MinReleaseYear = releaseYear; // Встановлюємо мінімальний рік
            DiscountPercentage = discountPercentage; // Встановлюємо процент знижки
            IsAutoPromotionEnabled = true; // Увімкнення автоматичних промоакцій
        }

        // Метод для вимкнення автоматичних промоакцій.
        public static void DisableAutoPromotions()
        {
            IsAutoPromotionEnabled = false; // Вимикає автопромо
        }

        // Метод для застосування промоакцій до колекції платівок на основі вказаних умов.
        public static void ApplyPromotions(ObservableCollection<VinylRecord> vinylRecords, string selectedGenre, int minReleaseYear, decimal discountPercentage, bool isAutoPromotionEnabled)
        {
            // Перевіряємо, чи ввімкнено автоматичні промоакції.
            if (isAutoPromotionEnabled)
            {
                // Перебираємо всі платівки та застосовуємо знижку за умовами.
                foreach (var record in vinylRecords)
                {
                    // Якщо жанр платівки відповідає вибраному жанру і рік випуску більше або дорівнює мінімальному року, то знижка застосовується.
                    if (record.Genre.Equals(selectedGenre, System.StringComparison.OrdinalIgnoreCase) && record.ReleaseYear >= minReleaseYear)
                    {
                        record.IsOnPromotion = true; // Платівка на промоакції
                        record.DiscountPercentage = discountPercentage; // Встановлюємо процент знижки
                    }
                    else
                    {
                        // Якщо умови не виконані, скидаємо знижку та промо.
                        record.IsOnPromotion = false;
                        record.DiscountPercentage = 0;
                    }
                }
            }
        }
    }
}
