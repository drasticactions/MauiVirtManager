using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace MauiVirtManager
{
    /// <summary>
    /// Convert a <see cref="VirtServer.Common.Domain.State"/> to a string representation.
    /// </summary>
    public class DomainStateConverter : IValueConverter
    {
        /// <inheritdoc/>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var target = (long)value;

            switch (target)
            {
                case 0:
                    return "Shutoff";
                case 1:
                    return "Saved";
                case 2:
                case 3:
                case 4:
                    return "Paused";
                case 5:
                    return "Running";
                default:
                    return string.Empty;
            }
        }

        /// <inheritdoc/>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
