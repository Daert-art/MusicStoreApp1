using MusicStoreApp1.Models;
using System.Windows;

namespace MusicStoreApp1.Views
{
    /// <summary>
    /// Interaction logic for SellRecordWindow.xaml
    /// Клас для вікна продажу платівки.
    /// </summary>
    public partial class SellRecordWindow : Window
    {
        // Властивість для збереження кількості проданих платівок.
        public int Quantity { get; set; }

        // Конструктор, який приймає об'єкт платівки для продажу.
        public SellRecordWindow(VinylRecord record)
        {
            InitializeComponent(); // Ініціалізує компоненти інтерфейсу.
            DataContext = this; // Встановлює DataContext для прив'язки властивостей до інтерфейсу.
        }

        // Обробник події для кнопки "Підтвердити продаж".
        private void SellButton_Click(object sender, RoutedEventArgs e)
        {
            // Перевіряємо, чи кількість є валідним числом більше за 0.
            if (int.TryParse(QuantityTextBox.Text, out int quantity) && quantity > 0)
            {
                // Присвоюємо введену кількість властивості Quantity.
                Quantity = quantity;

                // Встановлюємо результат діалогу на успішний (true) і закриваємо вікно.
                DialogResult = true;
                this.Close();
            }
            else
            {
                // Виводимо повідомлення про помилку, якщо кількість введена некоректно.
                MessageBox.Show("Будь ласка, введіть валідну кількість.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
