using MusicStoreApp1.ViewModels;
using System.Windows;

namespace MusicStoreApp1.Views
{
    /// <summary>
    /// Interaction logic for SalesStatisticsWindow.xaml
    /// </summary>
    public partial class SalesStatisticsWindow : Window
    {
        public SalesStatisticsWindow()
        {
            InitializeComponent();
            DataContext = new SalesStatisticsViewModel(); // Встановлюємо DataContext для вікна
        }
    }
}
