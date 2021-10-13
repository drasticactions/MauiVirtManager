// <copyright file="DomainStateImageConverter.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDNT.AppBasics.Virtualization.Libvirt;
using Microsoft.Maui.Controls;

namespace MauiVirtManager.Tools.Converters
{
    /// <summary>
    /// Domain State Image Converter.
    /// </summary>
    public class DomainStateImageConverter : IValueConverter
    {
        /// <inheritdoc/>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is VirDomainState domainState)
            {
                return domainState switch
                {
                    VirDomainState.VIR_DOMAIN_NOSTATE => new FileImageSource() { File = "state_shutoff.png" },
                    VirDomainState.VIR_DOMAIN_RUNNING => new FileImageSource() { File = "state_running.png" },
                    VirDomainState.VIR_DOMAIN_BLOCKED or VirDomainState.VIR_DOMAIN_PAUSED => new FileImageSource() { File = "state_paused.png" },
                    VirDomainState.VIR_DOMAIN_SHUTDOWN or VirDomainState.VIR_DOMAIN_SHUTOFF or VirDomainState.VIR_DOMAIN_CRASHED => new FileImageSource() { File = "state_shutoff.png" },
                    _ => new FileImageSource() { File = "state_shutoff.png" },
                };
            }

            return new FileImageSource() { File = "state_shutoff.png" };
        }

        /// <inheritdoc/>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}