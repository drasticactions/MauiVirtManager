// <copyright file="DomainScreenshotImageConverter.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDNT.AppBasics.Virtualization.Libvirt;
using Microsoft.Maui.Controls;

namespace MauiVirtManager.Tools.Converters
{
    /// <summary>
    /// Domain Screenshot Image Converter.
    /// </summary>
    public class DomainScreenshotImageConverter : IValueConverter
    {
        /// <inheritdoc/>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Stream stream)
            {
                return ImageSource.FromStream(() => stream);
            }

            return new FileImageSource() { File = "dotnet_bot.png" };
        }

        /// <inheritdoc/>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
