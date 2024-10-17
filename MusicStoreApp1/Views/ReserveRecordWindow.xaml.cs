using MusicStoreApp1.Models;
using System.Windows;

namespace MusicStoreApp1.Views
{
    // Клас для вікна резервування платівки.
    public partial class ReserveRecordWindow : Window
    {
        // Властивості для збереження імені клієнта і кількості резервованих платівок.
        public string CustomerName { get; set; }
        public int QuantityReserved { get; set; }

        // Конструктор, який приймає об'єкт платівки для резервування.
        public ReserveRecordWindow(VinylRecord record)
        {
            InitializeComponent(); // Ініціалізує компоненти інтерфейсу.
            DataContext = this; // Встановлює DataContext для прив'язки властивостей до інтерфейсу.
        }

        // Обробник події для кнопки "Резервувати".
        private void ReserveButton_Click(object sender, RoutedEventArgs e)
        {
            // Перевіряємо, чи введене ім'я клієнта не є порожнім і чи кількість є коректним числом більше за 0.
            if (!string.IsNullOrEmpty(CustomerNameTextBox.Text) && int.TryParse(QuantityReservedTextBox.Text, out int quantity) && quantity > 0)
            {
                // Присвоюємо значення введених даних властивостям CustomerName і QuantityReserved.
                CustomerName = CustomerNameTextBox.Text;
                QuantityReserved = quantity;

                // Встановлюємо результат діалогу на успішний (true).
                DialogResult = true;
                this.Close(); // Закриваємо вікно.
            }
            else
            {
                // Виводимо повідомлення про помилку, якщо дані введені некоректно.
                MessageBox.Show("Будь ласка, введіть ім'я клієнта та кількість платівок.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
