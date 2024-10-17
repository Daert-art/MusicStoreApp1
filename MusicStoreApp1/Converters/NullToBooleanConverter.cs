using System;
using System.Globalization;
using System.Windows.Data;

namespace MusicStoreApp1.Converters
{
    // Конвертер, який перетворює значення null в булеве значення.
    // Реалізує інтерфейс IValueConverter для прив'язки властивостей в XAML.

    public class NullToBooleanConverter : IValueConverter
    {
        // Метод Convert перетворює значення (value) на булеве. Повертає true, якщо значення не є null, і false, якщо це null.
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value != null;
        }

        // Метод ConvertBack не реалізований, оскільки це односторонній конвертер (від значення до bool).
        // Якщо потрібно зворотне перетворення, слід реалізувати цей метод.
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
