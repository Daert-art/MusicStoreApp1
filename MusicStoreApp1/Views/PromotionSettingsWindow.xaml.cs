using MusicStoreApp1.ViewModels;
using System.Windows;

namespace MusicStoreApp1.Views
{
    public partial class PromotionSettingsWindow : Window
    {
        public PromotionSettingsWindow(PromotionSettingsViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;  // Передача ViewModel у вікно
        }
    }
}
