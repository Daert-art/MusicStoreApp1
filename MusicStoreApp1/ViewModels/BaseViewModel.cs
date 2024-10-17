using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStoreApp1.ViewModels
{
    // Базовий клас для всіх ViewModel в додатку.
    // Реалізує інтерфейс INotifyPropertyChanged для відслідковування змін у властивостях і оновлення прив'язаного інтерфейсу користувача (UI).

    public class BaseViewModel : INotifyPropertyChanged
    {
        // Подія, яка спрацьовує при зміні властивості. Використовується для повідомлення UI про зміну.
        public event PropertyChangedEventHandler PropertyChanged;

        // Метод для виклику події PropertyChanged. Приймає назву властивості, яка змінилася.
        // Це забезпечує коректне оновлення UI, коли дані в ViewModel змінюються.
        protected void OnPropertyChanged(string propertyName)
        {
            // Якщо є підписники на подію PropertyChanged, вони будуть сповіщені про зміну властивості.
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
