using MusicStoreApp1.Models;
using System.Windows;

namespace MusicStoreApp1.Views
{
    // Це клас для вікна редагування резервування.
    public partial class EditReservationWindow : Window
    {
        // Властивість для зберігання переданого резервування.
        public Reservation Reservation { get; }

        // Конструктор вікна, який отримує екземпляр резервування для редагування.
        public EditReservationWindow(Reservation reservation)
        {
            InitializeComponent(); // Ініціалізує компоненти XAML.
            Reservation = reservation; // Присвоює передане резервування властивості класу.
            DataContext = Reservation; // Встановлює DataContext для прив'язки даних до інтерфейсу.
        }

        // Метод, який спрацьовує при натисканні кнопки "Зберегти".
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Встановлюємо результат діалогу як успішний (true).
            DialogResult = true;
            // Закриваємо вікно після збереження.
            this.Close();
        }
    }
}
