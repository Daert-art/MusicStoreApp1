using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;
using MusicStoreApp1.Commands;
using MusicStoreApp1.Data;
using MusicStoreApp1.Models;
using MusicStoreApp1.Services;
using MusicStoreApp1.Views;

namespace MusicStoreApp1.ViewModels
{
    // ViewModel для роботи з платівками, яка надає функціонал для додавання, редагування, видалення, продажу та резервування платівок.
    public class VinylRecordsViewModel : BaseViewModel
    {
        // Колекція для зберігання платівок.
        public ObservableCollection<VinylRecord> VinylRecords { get; set; }

        // Пошуковий запит для фільтрації платівок.
        public string SearchQuery { get; set; }

        // Статистика продажів платівок.
        public SalesStatistics SalesStatistics { get; set; }

        // Вибрана платівка.
        private VinylRecord _selectedRecord;
        public VinylRecord SelectedRecord
        {
            get { return _selectedRecord; }
            set
            {
                _selectedRecord = value;
                // Оновлюємо стан вибраної платівки і викликаємо команди для оновлення UI.
                OnPropertyChanged(nameof(SelectedRecord));
                OnPropertyChanged(nameof(FinalPrice));
                UpdateCommandStates(); // Оновлюємо доступність команд
            }
        }

        // Команди для різних операцій з платівками.
        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand SellCommand { get; }
        public ICommand ReserveCommand { get; }
        public ICommand ShowReservationsCommand { get; }
        public ICommand SearchCommand { get; }
        public ICommand ShowSalesStatisticsCommand { get; }
        public ICommand ShowCustomersCommand { get; }
        public ICommand UpdateSalesStatisticsCommand { get; }
        public ICommand ApplyPromotionsCommand { get; }
        public ICommand ShowPromotionSettingsCommand { get; }

        // Властивість для відображення фінальної ціни платівки.
        public decimal FinalPrice
        {
            get
            {
                return SelectedRecord != null ? SelectedRecord.FinalPrice : 0;
            }
        }

        // Конструктор, який ініціалізує команди і завантажує дані з бази.
        public VinylRecordsViewModel()
        {
            LoadRecordsFromDatabase(); // Завантажуємо платівки з бази

            // Ініціалізація команд.
            AddCommand = new RelayCommand(AddRecord);
            EditCommand = new RelayCommand(EditRecord, CanEditOrDelete);
            DeleteCommand = new RelayCommand(DeleteRecord, CanEditOrDelete);
            SearchCommand = new RelayCommand(SearchRecords);
            SellCommand = new RelayCommand(SellRecord, CanSell);
            ReserveCommand = new RelayCommand(ReserveRecord, CanReserve);
            ShowReservationsCommand = new RelayCommand(ShowReservations, CanShowReservations);
            ShowSalesStatisticsCommand = new RelayCommand(ShowSalesStatistics);
            ShowCustomersCommand = new RelayCommand(ShowCustomers);
            UpdateSalesStatisticsCommand = new RelayCommand(UpdateSalesStatistics);
            ApplyPromotionsCommand = new RelayCommand(ApplyPromotionsToRecords);
            ShowPromotionSettingsCommand = new RelayCommand(ShowPromotionSettings);

            UpdateSalesStatistics(); // Оновлюємо статистику продажів при ініціалізації.
        }

        // Метод для завантаження платівок з бази даних.
        public void LoadRecordsFromDatabase()
        {
            using (var context = new AppDbContext())
            {
                // Завантажуємо всі платівки разом з резервуваннями.
                VinylRecords = new ObservableCollection<VinylRecord>(context.VinylRecords.Include(r => r.Reservations).ToList());
                OnPropertyChanged(nameof(VinylRecords)); // Оповіщаємо UI про зміну даних
            }
        }

        // Метод для збереження платівки в базу даних.
        public void SaveRecordToDatabase(VinylRecord record)
        {
            using (var context = new AppDbContext())
            {
                // Округляємо ціни до двох знаків після коми
                record.CostPrice = Math.Round(record.CostPrice, 2);
                record.SalePrice = Math.Round(record.SalePrice, 2);
                
                // Шукаємо запис у базі даних за ID платівки.
                var existingRecord = context.VinylRecords.Include(r => r.Reservations).SingleOrDefault(r => r.Id == record.Id);

                if (existingRecord != null)
                {
                    // Оновлюємо дані існуючої платівки.
                    existingRecord.AlbumName = record.AlbumName;
                    existingRecord.ArtistName = record.ArtistName;
                    existingRecord.Genre = record.Genre;
                    existingRecord.ReleaseYear = record.ReleaseYear;
                    existingRecord.CostPrice = record.CostPrice;
                    existingRecord.SalePrice = record.SalePrice;
                    existingRecord.IsOnPromotion = record.IsOnPromotion;
                    existingRecord.DiscountPercentage = record.DiscountPercentage;
                    existingRecord.QuantityInStock = record.QuantityInStock;
                    existingRecord.QuantitySold = record.QuantitySold;

                    // Оновлюємо резервування.
                    existingRecord.Reservations.Clear();
                    foreach (var reservation in record.Reservations)
                    {
                        existingRecord.Reservations.Add(reservation);
                    }

                    context.VinylRecords.Update(existingRecord);
                }
                else
                {
                    // Додаємо нову платівку, якщо її не було в базі.
                    context.VinylRecords.Add(record);
                }

                context.SaveChanges(); // Зберігаємо зміни
            }
        }

        // Метод для додавання нової платівки.
        private void AddRecord()
        {
            var newRecord = new VinylRecord
            {
                AlbumName = "New Album",
                ArtistName = "New Artist",
                PublisherName = "New Publisher",
                NumberOfTracks = 10,
                Genre = "Rock",
                ReleaseYear = 2023,
                CostPrice = 20.00m,
                SalePrice = 30.00m,
                IsOnPromotion = false,
                DiscountPercentage = 0
            };

            VinylRecords.Add(newRecord);
            SaveRecordToDatabase(newRecord); // Зберігаємо нову платівку в базу даних.
        }

        // Метод для редагування вибраної платівки.
        private void EditRecord()
        {
            if (SelectedRecord != null)
            {
                var editWindow = new EditRecordWindow(SelectedRecord); // Відкриваємо вікно редагування.
                if (editWindow.ShowDialog() == true)
                {
                    SaveRecordToDatabase(SelectedRecord); // Зберігаємо зміни після редагування.
                    LoadRecordsFromDatabase(); // Оновлюємо список платівок.
                }
            }
        }

        // Метод для видалення вибраної платівки.
        private void DeleteRecord()
        {
            if (SelectedRecord != null)
            {
                var result = MessageBox.Show($"Ви дійсно хочете видалити {SelectedRecord.AlbumName}?", "Підтвердження", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    using (var context = new AppDbContext())
                    {
                        context.VinylRecords.Remove(SelectedRecord); // Видаляємо платівку з бази.
                        context.SaveChanges();
                    }
                    VinylRecords.Remove(SelectedRecord); // Видаляємо платівку з колекції.
                }
            }
        }

        // Метод для пошуку платівок за запитом.
        private void SearchRecords()
        {
            if (!string.IsNullOrEmpty(SearchQuery))
            {
                // Фільтруємо платівки за назвою, іменем артиста або жанром.
                var filteredRecords = VinylRecords.Where(record =>
                    record.AlbumName.Contains(SearchQuery, System.StringComparison.OrdinalIgnoreCase) ||
                    record.ArtistName.Contains(SearchQuery, System.StringComparison.OrdinalIgnoreCase) ||
                    record.Genre.Contains(SearchQuery, System.StringComparison.OrdinalIgnoreCase)).ToList();

                VinylRecords = new ObservableCollection<VinylRecord>(filteredRecords);
            }
            else
            {
                LoadRecordsFromDatabase(); // Якщо запит порожній, завантажуємо всі платівки з бази.
            }

            OnPropertyChanged(nameof(VinylRecords)); // Оповіщаємо UI про зміну.
        }

        // Метод для продажу платівки.
        private void SellRecord()
        {
            if (SelectedRecord != null && SelectedRecord.QuantityInStock > 0)
            {
                var sellWindow = new SellRecordWindow(SelectedRecord); // Відкриваємо вікно для продажу.
                if (sellWindow.ShowDialog() == true)
                {
                    // Оновлюємо кількість проданих і в наявності після продажу.
                    SelectedRecord.QuantitySold += sellWindow.Quantity;
                    SelectedRecord.QuantityInStock -= sellWindow.Quantity;
                    SaveRecordToDatabase(SelectedRecord);
                    LoadRecordsFromDatabase(); // Оновлюємо список платівок.
                }
            }
            else
            {
                MessageBox.Show("Ця платівка більше не доступна в наявності.");
            }
        }

        // Метод для перевірки, чи можна продати платівку.
        private bool CanSell()
        {
            return SelectedRecord != null;
        }

        // Метод для резервування платівки.
        private void ReserveRecord()
        {
            if (SelectedRecord != null && SelectedRecord.QuantityInStock > 0)
            {
                var reserveWindow = new ReserveRecordWindow(SelectedRecord); // Вікно для резервування.
                if (reserveWindow.ShowDialog() == true)
                {
                    var reservation = new Reservation
                    {
                        CustomerName = reserveWindow.CustomerName,
                        QuantityReserved = reserveWindow.QuantityReserved,
                        VinylRecordId = SelectedRecord.Id
                    };

                    // Оновлюємо кількість в наявності і додаємо резервування.
                    SelectedRecord.QuantityInStock -= reserveWindow.QuantityReserved;
                    SelectedRecord.Reservations.Add(reservation);

                    SaveRecordToDatabase(SelectedRecord);
                    LoadRecordsFromDatabase(); // Оновлюємо список платівок.
                }
            }
            else
            {
                MessageBox.Show("Платівка більше не доступна для резервування.");
            }
        }

        // Метод для перевірки, чи можна резервувати платівку.
        private bool CanReserve()
        {
            return SelectedRecord != null && SelectedRecord.QuantityInStock > 0;
        }

        // Метод для відображення резервувань вибраної платівки.
        private void ShowReservations()
        {
            if (SelectedRecord != null)
            {
                var reservationsWindow = new ReservationsWindow(SelectedRecord); // Відкриваємо вікно з резервуваннями.
                reservationsWindow.ShowDialog();
                LoadRecordsFromDatabase(); // Оновлюємо список після перегляду резервувань.
            }
        }

        // Метод для перевірки, чи можна переглянути резервування.
        private bool CanShowReservations()
        {
            return SelectedRecord != null && SelectedRecord.Reservations.Any();
        }

        // Метод для відображення вікна зі статистикою продажів.
        private void ShowSalesStatistics()
        {
            var statisticsWindow = new SalesStatisticsWindow
            {
                DataContext = this // Встановлюємо поточну ViewModel як DataContext для вікна.
            };
            statisticsWindow.ShowDialog();
        }

        // Метод для відображення вікна зі списком клієнтів.
        private void ShowCustomers()
        {
            var customersWindow = new CustomersWindow();
            customersWindow.Show();
        }

        // Метод для оновлення статистики продажів.
        public void UpdateSalesStatistics()
        {
            using (var context = new AppDbContext())
            {
                var soldRecords = context.VinylRecords.Where(v => v.QuantitySold > 0).ToList();
                var totalSold = soldRecords.Sum(v => v.QuantitySold); // Підрахунок загальної кількості проданих платівок.
                var totalRevenue = soldRecords.Sum(v => v.SalePrice * v.QuantitySold); // Підрахунок загального доходу.

                SalesStatistics = new SalesStatistics
                {
                    TotalVinylRecordsSold = totalSold,
                    TotalRevenue = totalRevenue
                };

                OnPropertyChanged(nameof(SalesStatistics)); // Оповіщаємо UI про оновлення статистики.
            }
        }

        // Метод для застосування промо-акцій до платівок.
        public void ApplyPromotionsToRecords()
        {
            // Викликаємо менеджер промо-акцій для застосування налаштувань.
            PromotionManager.ApplyPromotions(
                VinylRecords,
                PromotionManager.SelectedGenre,
                PromotionManager.MinReleaseYear,
                PromotionManager.DiscountPercentage,
                PromotionManager.IsAutoPromotionEnabled
            );

            // Зберігаємо зміни для кожної платівки після застосування промо-акцій.
            foreach (var record in VinylRecords)
            {
                SaveRecordToDatabase(record);
            }

            OnPropertyChanged(nameof(VinylRecords)); // Оновлюємо UI.
        }

        // Метод для відкриття вікна з налаштуваннями промо-акцій.
        private void ShowPromotionSettings()
        {
            var promotionSettingsWindow = new PromotionSettingsWindow(new PromotionSettingsViewModel(this)); // Передаємо поточну ViewModel.
            promotionSettingsWindow.ShowDialog();

            // Оновлюємо промо-акції після закриття вікна.
            ApplyPromotionsToRecords();
        }

        // Метод для перевірки, чи можна редагувати або видалити вибрану платівку.
        private bool CanEditOrDelete()
        {
            return SelectedRecord != null;
        }

        // Метод для оновлення стану команд, пов'язаних з вибраною платівкою.
        private void UpdateCommandStates()
        {
            ((RelayCommand)EditCommand).RaiseCanExecuteChanged();
            ((RelayCommand)DeleteCommand).RaiseCanExecuteChanged();
            ((RelayCommand)SellCommand).RaiseCanExecuteChanged();
            ((RelayCommand)ReserveCommand).RaiseCanExecuteChanged();
            ((RelayCommand)ShowReservationsCommand).RaiseCanExecuteChanged();
            ((RelayCommand)ApplyPromotionsCommand).RaiseCanExecuteChanged();
        }
    }
}
