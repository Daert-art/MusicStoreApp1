using MusicStoreApp1.Commands;
using MusicStoreApp1.Data;
using MusicStoreApp1.Models;
using MusicStoreApp1.Views;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace MusicStoreApp1.ViewModels
{
    // ViewModel для керування резервуваннями платівок. Включає можливість редагувати і видаляти резервування.
    public class ReservationsViewModel : BaseViewModel
    {
        // Вибрана платівка, до якої відносяться резервування.
        public VinylRecord SelectedRecord { get; set; }

        // Вибране резервування для редагування або видалення.
        private Reservation _selectedReservation;
        public Reservation SelectedReservation
        {
            get { return _selectedReservation; }
            set
            {
                _selectedReservation = value;
                OnPropertyChanged(nameof(SelectedReservation)); // Оповіщаємо UI про зміну вибраного резервування.

                // Оновлюємо стан команд редагування та видалення, коли змінюється вибір резервування.
                ((RelayCommand)EditReservationCommand).RaiseCanExecuteChanged();
                ((RelayCommand)DeleteReservationCommand).RaiseCanExecuteChanged();
            }
        }

        // Команди для редагування і видалення резервування.
        public ICommand EditReservationCommand { get; }
        public ICommand DeleteReservationCommand { get; }

        // Конструктор приймає вибрану платівку і ініціалізує команди.
        public ReservationsViewModel(VinylRecord selectedRecord)
        {
            SelectedRecord = selectedRecord;
            EditReservationCommand = new RelayCommand(EditReservation, CanEditOrDeleteReservation); // Команда для редагування резервування
            DeleteReservationCommand = new RelayCommand(DeleteReservation, CanEditOrDeleteReservation); // Команда для видалення резервування
        }

        // Метод для перевірки, чи можна редагувати або видалити вибране резервування.
        private bool CanEditOrDeleteReservation()
        {
            return SelectedReservation != null; // Можливо, якщо вибране резервування не null.
        }

        // Метод для редагування вибраного резервування.
        private void EditReservation()
        {
            if (SelectedReservation != null)
            {
                // Зберігаємо попередню кількість зарезервованих платівок для порівняння після редагування.
                int oldQuantityReserved = SelectedReservation.QuantityReserved;

                var editWindow = new EditReservationWindow(SelectedReservation); // Відкриваємо вікно для редагування
                if (editWindow.ShowDialog() == true)
                {
                    // Обчислюємо різницю між новою і старою кількістю зарезервованих платівок.
                    int difference = SelectedReservation.QuantityReserved - oldQuantityReserved;

                    // Оновлюємо кількість платівок в наявності, враховуючи різницю.
                    SelectedRecord.QuantityInStock -= difference;

                    // Оновлюємо резервування і платівку у базі даних.
                    using (var context = new AppDbContext())
                    {
                        context.Reservations.Update(SelectedReservation); // Оновлюємо резервування
                        context.VinylRecords.Update(SelectedRecord); // Оновлюємо платівку
                        context.SaveChanges(); // Зберігаємо зміни
                    }

                    // Оповіщаємо UI про зміну в резервуваннях і кількості платівок в наявності.
                    OnPropertyChanged(nameof(SelectedRecord.Reservations));
                    OnPropertyChanged(nameof(SelectedRecord.QuantityInStock));
                }
            }
        }

        // Метод для видалення вибраного резервування.
        private void DeleteReservation()
        {
            if (SelectedReservation != null)
            {
                var result = MessageBox.Show("Ви дійсно хочете видалити це резервування?", "Підтвердження", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes) // Якщо підтверджено видалення
                {
                    // Повертаємо кількість зарезервованих платівок до кількості в наявності.
                    SelectedRecord.QuantityInStock += SelectedReservation.QuantityReserved;

                    // Видаляємо резервування з колекції та бази даних.
                    SelectedRecord.Reservations.Remove(SelectedReservation);

                    using (var context = new AppDbContext())
                    {
                        context.Reservations.Remove(SelectedReservation); // Видаляємо резервування з бази
                        context.VinylRecords.Update(SelectedRecord); // Оновлюємо платівку
                        context.SaveChanges(); // Зберігаємо зміни
                    }

                    // Оповіщаємо UI про зміну в резервуваннях і кількості платівок в наявності.
                    OnPropertyChanged(nameof(SelectedRecord.Reservations));
                    OnPropertyChanged(nameof(SelectedRecord.QuantityInStock));  // Оновлюємо кількість в наявності
                }
            }
        }
    }
}
