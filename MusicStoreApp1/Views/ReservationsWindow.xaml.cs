using MusicStoreApp1.Models;
using MusicStoreApp1.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MusicStoreApp1.Views
{
    /// <summary>
    /// Interaction logic for ReservationsWindow.xaml
    /// </summary>
    public partial class ReservationsWindow : Window
    {
        public ReservationsWindow(VinylRecord selectedRecord)  // Додаємо конструктор з параметром
        {
            InitializeComponent();
            DataContext = new ReservationsViewModel(selectedRecord);  // Передаємо VinylRecord у ViewModel
        }
    }
}
