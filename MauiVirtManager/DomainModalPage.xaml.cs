// <copyright file="DomainModalPage.xaml.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiVirtManager.Tools.Utilities;
using MauiVirtManager.ViewModels;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
using Microsoft.Maui.Controls.Xaml;
using VirtServer.Common;

namespace MauiVirtManager
{
    /// <summary>
    /// Domain Modal Page.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DomainModalPage : BasePage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DomainModalPage"/> class.
        /// </summary>
        /// <param name="domain">see<see cref="Domain"/>.</param>
        /// <param name="services">see<see cref="IServiceProvider"/>.</param>
        public DomainModalPage(Domain domain, IServiceProvider services)
             : base(services)
        {
            this.On<iOS>().SetModalPresentationStyle(UIModalPresentationStyle.Automatic);
            this.InitializeComponent();
            this.BindingContext = this.ViewModel = services.ResolveWith<DomainModalViewModel>(domain);
        }
    }
}
