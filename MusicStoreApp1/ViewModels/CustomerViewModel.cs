using MusicStoreApp1.Models;
using System.Windows;
using System.Windows.Input;
using MusicStoreApp1.Commands;
using MusicStoreApp1.Data;

namespace MusicStoreApp1.ViewModels
{
    // ViewModel для роботи з клієнтом. Інкапсулює логіку для створення, редагування і збереження клієнта.
    public class CustomerViewModel : BaseViewModel
    {
        // Об'єкт клієнта, з яким працює ViewModel.
        public Customer Customer { get; set; }

        // Властивості для відображення та редагування імені клієнта.
        // Оновлює відповідні властивості клієнта і повідомляє UI про зміни через OnPropertyChanged.
        public string FirstName
        {
            get => Customer.FirstName;
            set
            {
                Customer.FirstName = value;
                OnPropertyChanged(nameof(FirstName)); // Оповіщує UI про зміну
            }
        }

        // Властивість для прізвища клієнта.
        public string LastName
        {
            get => Customer.LastName;
            set
            {
                Customer.LastName = value;
                OnPropertyChanged(nameof(LastName)); // Оповіщує UI про зміну
            }
        }

        // Властивість для email клієнта.
        public string Email
        {
            get => Customer.Email;
            set
            {
                Customer.Email = value;
                OnPropertyChanged(nameof(Email)); // Оповіщує UI про зміну
            }
        }

        // Властивість для номера телефону клієнта.
        public string PhoneNumber
        {
            get => Customer.PhoneNumber;
            set
            {
                Customer.PhoneNumber = value;
                OnPropertyChanged(nameof(PhoneNumber)); // Оповіщує UI про зміну
            }
        }

        // Команда для збереження клієнта.
        public ICommand SaveCommand { get; }

        // Конструктор, який приймає об'єкт клієнта. Ініціалізує команду для збереження.
        public CustomerViewModel(Customer customer)
        {
            Customer = customer;
            SaveCommand = new RelayCommand(SaveCustomer); // Прив'язуємо команду для збереження клієнта
        }

        // Метод для збереження клієнта в базу даних.
        private void SaveCustomer()
        {
            using (var context = new AppDbContext())
            {
                if (Customer.Id == 0) // Якщо Id клієнта 0, додаємо нового клієнта в базу
                {
                    context.Customers.Add(Customer);
                }
                else // Якщо Id не 0, оновлюємо дані існуючого клієнта
                {
                    context.Customers.Update(Customer);
                }
                context.SaveChanges(); // Зберігаємо зміни в базу
            }

            // Показуємо повідомлення про успішне збереження клієнта.
            MessageBox.Show("Клієнт успішно збережений!");
        }
    }
}
