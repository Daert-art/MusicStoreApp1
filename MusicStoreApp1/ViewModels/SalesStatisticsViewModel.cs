using MusicStoreApp1.Commands;
using MusicStoreApp1.Data;
using MusicStoreApp1.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace MusicStoreApp1.ViewModels
{
    // ViewModel для відображення та управління статистикою продажів платівок.
    public class SalesStatisticsViewModel : BaseViewModel
    {
        // Колекція для зберігання статистики продажів.
        public ObservableCollection<VinylRecord> SalesStatistics { get; set; }

        // Команда для оновлення статистики продажів.
        public ICommand RefreshStatisticsCommand { get; }

        // Конструктор, який ініціалізує колекцію та команду для оновлення статистики.
        public SalesStatisticsViewModel()
        {
            SalesStatistics = new ObservableCollection<VinylRecord>(); // Ініціалізуємо колекцію
            RefreshStatisticsCommand = new RelayCommand(RefreshStatistics); // Прив'язуємо команду для оновлення
            LoadSalesStatistics(); // Завантажуємо статистику при ініціалізації
        }

        // Метод для завантаження статистики продажів з бази даних.
        private void LoadSalesStatistics()
        {
            using (var context = new AppDbContext())
            {
                // Отримуємо всі записи платівок, які були продані (QuantitySold > 0) і сортуємо їх за кількістю проданих одиниць.
                var statistics = context.VinylRecords
                    .Where(v => v.QuantitySold > 0) // Фільтр по кількості проданих
                    .OrderByDescending(v => v.QuantitySold) // Сортуємо від більшого до меншого
                    .ToList();

                // Очищаємо колекцію і додаємо отримані записи.
                SalesStatistics.Clear();
                foreach (var record in statistics)
                {
                    SalesStatistics.Add(record);
                }

                // Оповіщаємо UI про зміну даних.
                OnPropertyChanged(nameof(SalesStatistics));
            }
        }

        // Метод для оновлення статистики при натисканні на кнопку.
        private void RefreshStatistics()
        {
            LoadSalesStatistics(); // Перезавантажуємо статистику з бази даних
        }
    }
}
