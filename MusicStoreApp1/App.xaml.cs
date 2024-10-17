using MusicStoreApp1.Data;
using System.Configuration;
using System.Data;
using System.Windows;

namespace MusicStoreApp1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// Клас для керування життєвим циклом застосунку.
    /// </summary>
    public partial class App : Application
    {
        // Метод OnStartup виконується під час запуску застосунку.
        // Зараз закоментовано для запобігання автоматичного виконання логіки при запуску.

        // protected override void OnStartup(StartupEventArgs e)
        // {
        //     base.OnStartup(e);

        //     // Ініціалізація бази даних при запуску застосунку.
        //     using (var context = new AppDbContext())
        //     {
        //         // Заповнення початковими даними (насіння) таблиці платівок у базі даних.
        //         AppDbContext.SeedVinylRecords(context);
        //     }

        //     // Створюємо і показуємо головне вікно застосунку.
        //     var mainWindow = new MainWindow();
        //     mainWindow.Show();
        // }
    }

}
