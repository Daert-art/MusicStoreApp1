using MusicStoreApp1.Models;
using MusicStoreApp1.Commands;
using System.Windows.Input;
using MusicStoreApp1.Services;

namespace MusicStoreApp1.ViewModels
{
    // ViewModel для налаштування параметрів промоакцій.
    public class PromotionSettingsViewModel : BaseViewModel
    {
        private readonly VinylRecordsViewModel _mainViewModel;  // Посилання на основну ViewModel для взаємодії з колекцією платівок.

        // Властивості для налаштування жанру, року випуску, знижки та флаг для автопромоакцій.
        public string SelectedGenre { get; set; }
        public int MinReleaseYear { get; set; }
        public decimal DiscountPercentage { get; set; }
        public bool IsAutoPromotionEnabled { get; set; }

        // Команда для застосування налаштувань промоакції.
        public ICommand ApplyPromotionSettingsCommand { get; }

        // Конструктор, який приймає посилання на основну ViewModel і ініціалізує команду та початкові налаштування.
        public PromotionSettingsViewModel(VinylRecordsViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
            ApplyPromotionSettingsCommand = new RelayCommand(ApplyPromotionSettings); // Команда для застосування налаштувань

            // Ініціалізуємо поля початковими значеннями з PromotionManager.
            SelectedGenre = PromotionManager.SelectedGenre;
            MinReleaseYear = PromotionManager.MinReleaseYear;
            DiscountPercentage = PromotionManager.DiscountPercentage;
            IsAutoPromotionEnabled = PromotionManager.IsAutoPromotionEnabled;
        }

        // Метод для застосування налаштувань промоакції.
        private void ApplyPromotionSettings()
        {
            // Якщо увімкнено автоматичні промоакції, встановлюємо умови через PromotionManager.
            if (IsAutoPromotionEnabled)
            {
                PromotionManager.SetAutoPromotionConditions(SelectedGenre, MinReleaseYear, DiscountPercentage);
            }
            else
            {
                // Якщо вимкнено, вимикаємо всі промоакції.
                PromotionManager.DisableAutoPromotions();
            }

            // Оновлюємо промоакції у головній ViewModel, щоб вони застосувалися до колекції платівок.
            _mainViewModel.ApplyPromotionsToRecords();
        }
    }
}
