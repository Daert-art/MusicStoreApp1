using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MusicStoreApp1.Commands
{
    // Клас RelayCommand дозволяє прив'язати команду до елементів UI (наприклад, кнопок) в MVVM-паттерні.
    // Реалізує інтерфейс ICommand, який вимагає методи CanExecute та Execute.

    public class RelayCommand : ICommand
    {
        // Делегат для виконання команди
        private readonly Action _execute;

        // Делегат для визначення, чи може команда виконатися
        private readonly Func<bool> _canExecute;

        // Конструктор. Приймає дію для виконання та необов'язкову функцію для перевірки можливості виконання.
        public RelayCommand(Action execute, Func<bool> canExecute = null)
        {
            // Якщо execute дорівнює null, кидає виключення ArgumentNullException.
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            // Присвоює canExecute або залишає його null, якщо не передано.
            _canExecute = canExecute;
        }

        // Метод, який визначає, чи може команда виконатися. Повертає true, якщо canExecute == null або виконується умова.
        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute();
        }

        // Метод для виконання команди. Просто викликає переданий Action.
        public void Execute(object parameter)
        {
            _execute();
        }

        // Подія, яка сигналізує, що стан виконання команди змінився (для перерисовки UI, коли кнопка може або не може бути активована).
        public event EventHandler CanExecuteChanged;

        // Метод для вручну викликати подію CanExecuteChanged, коли необхідно повідомити систему, що умови виконання змінилися.
        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        // Закоментований код з автоматичною прив'язкою до CommandManager для частішого виклику CanExecuteChanged.
        // Може використовуватись для оновлення стану команд при зміні умов без явного виклику RaiseCanExecuteChanged.
        //public event EventHandler CanExecuteChanged
        //{
        //    add { CommandManager.RequerySuggested += value; }
        //    remove { CommandManager.RequerySuggested -= value; }
        //}
    }
}
