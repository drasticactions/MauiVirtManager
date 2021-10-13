using System;
using System.Globalization;
using IDNT.AppBasics.Virtualization.Libvirt;
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
            if (value is VirDomainState domainState)
            {
                return domainState switch
                {
                    VirDomainState.VIR_DOMAIN_NOSTATE => string.Empty,
                    VirDomainState.VIR_DOMAIN_RUNNING => Translations.Common.DomainStateRunning,
                    VirDomainState.VIR_DOMAIN_BLOCKED or VirDomainState.VIR_DOMAIN_PAUSED => Translations.Common.DomainStatePaused,
                    VirDomainState.VIR_DOMAIN_SHUTDOWN or VirDomainState.VIR_DOMAIN_SHUTOFF or VirDomainState.VIR_DOMAIN_CRASHED => Translations.Common.DomainStateShutoff,
                    _ => string.Empty,
                };
            }

            return string.Empty;
        }

        /// <inheritdoc/>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
