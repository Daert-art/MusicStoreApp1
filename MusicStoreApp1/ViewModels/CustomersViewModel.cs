using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
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
    // ViewModel для роботи зі списком клієнтів в додатку.
    // Наслідує BaseViewModel, що дозволяє відслідковувати зміни у властивостях і відповідно оновлювати UI.

    public class CustomersViewModel : BaseViewModel
    {
        // Колекція клієнтів для відображення в інтерфейсі.
        public ObservableCollection<Customer> Customers { get; set; }

        // Команди для додавання, редагування, видалення та оновлення клієнтів.
        public ICommand AddCustomerCommand { get; }
        public ICommand EditCustomerCommand { get; }
        public ICommand DeleteCustomerCommand { get; }
        public ICommand RefreshCustomersCommand { get; }

        // Властивість для збереження вибраного клієнта.
        private Customer _selectedCustomer;
        public Customer SelectedCustomer
        {
            get => _selectedCustomer;
            set
            {
                _selectedCustomer = value;
                OnPropertyChanged(nameof(SelectedCustomer)); // Оповіщаємо UI про зміну вибраного клієнта.
                // Оновлюємо стан команд для редагування та видалення клієнта.
                ((RelayCommand)EditCustomerCommand).RaiseCanExecuteChanged();
                ((RelayCommand)DeleteCustomerCommand).RaiseCanExecuteChanged();
            }
        }

        // Конструктор ініціалізує команди та завантажує клієнтів з бази даних.
        public CustomersViewModel()
        {
            Customers = new ObservableCollection<Customer>();
            RefreshCustomersCommand = new RelayCommand(RefreshCustomers); // Команда для оновлення списку
            AddCustomerCommand = new RelayCommand(AddCustomer); // Команда для додавання клієнта
            EditCustomerCommand = new RelayCommand(EditCustomer, () => SelectedCustomer != null); // Команда для редагування клієнта
            DeleteCustomerCommand = new RelayCommand(DeleteCustomer, () => SelectedCustomer != null); // Команда для видалення клієнта

            LoadCustomers(); // Завантажуємо клієнтів при створенні ViewModel
        }

        // Метод для завантаження клієнтів з бази даних і оновлення колекції Customers.
        private void LoadCustomers()
        {
            using (var context = new AppDbContext())
            {
                var customers = context.Customers.ToList(); // Отримуємо всіх клієнтів з бази
                Customers.Clear(); // Очищаємо поточний список клієнтів
                foreach (var customer in customers)
                {
                    Customers.Add(customer); // Додаємо кожного клієнта в ObservableCollection для прив'язки до UI
                }
                OnPropertyChanged(nameof(Customers)); // Оповіщаємо UI про зміни
            }
        }

        // Метод для оновлення списку клієнтів через повторне завантаження.
        private void RefreshCustomers()
        {
            LoadCustomers(); // Перезавантажуємо список клієнтів
        }

        // Метод для перевірки унікальності клієнта за email, щоб уникнути дублювання.
        private bool IsCustomerUnique(Customer customer)
        {
            using (var context = new AppDbContext())
            {
                // Перевіряємо, чи є в базі клієнт з таким же email, але іншим Id.
                return !context.Customers.Any(c => c.Email == customer.Email && c.Id != customer.Id);
            }
        }

        // Метод для додавання нового клієнта. Відкривається вікно CustomerWindow для введення даних.
        private void AddCustomer()
        {
            var newCustomer = new Customer(); // Створюємо новий об'єкт клієнта
            var customerWindow = new CustomerWindow(newCustomer); // Відкриваємо вікно для введення даних

            if (customerWindow.ShowDialog() == true) // Якщо користувач зберіг дані
            {
                if (IsCustomerUnique(newCustomer)) // Перевіряємо унікальність клієнта за email
                {
                    using (var context = new AppDbContext())
                    {
                        context.Customers.Add(newCustomer); // Додаємо нового клієнта в базу
                        context.SaveChanges(); // Зберігаємо зміни
                    }
                    LoadCustomers(); // Оновлюємо список клієнтів після додавання
                }
                else
                {
                    MessageBox.Show("Клієнт з таким email вже існує.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error); // Виводимо помилку, якщо клієнт з таким email вже є
                }
            }
        }

        // Метод для редагування обраного клієнта. Відкривається вікно для редагування даних.
        private void EditCustomer()
        {
            if (SelectedCustomer != null) // Перевіряємо, чи є вибраний клієнт
            {
                var customerWindow = new CustomerWindow(SelectedCustomer); // Відкриваємо вікно для редагування
                if (customerWindow.ShowDialog() == true) // Якщо користувач зберіг зміни
                {
                    using (var context = new AppDbContext())
                    {
                        context.Customers.Update(customerWindow.Customer); // Оновлюємо дані клієнта в базі
                        context.SaveChanges(); // Зберігаємо зміни
                    }
                    RefreshCustomers(); // Оновлюємо список після редагування
                }
            }
        }

        // Метод для видалення вибраного клієнта з підтвердженням.
        private void DeleteCustomer()
        {
            if (SelectedCustomer != null) // Перевіряємо, чи є вибраний клієнт
            {
                var result = MessageBox.Show($"Ви дійсно хочете видалити {SelectedCustomer.FirstName} {SelectedCustomer.LastName}?", "Підтвердження", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes) // Якщо користувач підтвердив видалення
                {
                    using (var context = new AppDbContext())
                    {
                        context.Customers.Remove(SelectedCustomer); // Видаляємо клієнта з бази
                        context.SaveChanges(); // Зберігаємо зміни
                    }
                    RefreshCustomers(); // Оновлюємо список після видалення
                }
            }
        }
    }
}
