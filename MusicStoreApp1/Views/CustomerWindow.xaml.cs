using MusicStoreApp1.Models;
using System.Windows;

namespace MusicStoreApp1.Views
{
    public partial class CustomerWindow : Window
    {
        public Customer Customer { get; private set; }

        public CustomerWindow(Customer customer)
        {
            InitializeComponent();
            Customer = customer;
            DataContext = Customer; // Прив'язуємо клієнта до DataContext
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true; // Закриваємо вікно та вказуємо, що зміни підтверджені
        }
    }
}
