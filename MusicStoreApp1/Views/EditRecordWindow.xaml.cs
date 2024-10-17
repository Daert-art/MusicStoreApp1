using MusicStoreApp1.Models;
using System.Windows;

namespace MusicStoreApp1.Views
{
    public partial class EditRecordWindow : Window
    {
        public VinylRecord Record { get; }

        public EditRecordWindow(VinylRecord record)
        {
            InitializeComponent();
            Record = record;
            DataContext = Record; // Прив'язуємо дані платівки до DataContext
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Перевіряємо, що всі поля заповнені правильно, за необхідності можна додати перевірку
            DialogResult = true; // Закриваємо вікно і зберігаємо зміни
            this.Close();
        }
    }
}
