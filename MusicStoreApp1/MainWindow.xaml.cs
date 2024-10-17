using MusicStoreApp1.ViewModels;
using System.Windows;

namespace MusicStoreApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// Це код-біхайнд для головного вікна застосунку.
    /// </summary>
    public partial class MainWindow : Window
    {
        // Конструктор для головного вікна.
        public MainWindow()
        {
            InitializeComponent(); // Ініціалізує компоненти XAML.

            // Встановлює DataContext для прив'язки даних.
            // VinylRecordsViewModel містить логіку та дані для взаємодії з інтерфейсом.
            this.DataContext = new VinylRecordsViewModel();
        }

        // Метод, який спрацьовує при зміні вибору в ListView.
        private void ListView_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            // Порожній обробник події. Цей метод може бути використаний для додаткової логіки,
            // яка має виконуватися, коли вибір у списку змінюється.
        }
    }
}
