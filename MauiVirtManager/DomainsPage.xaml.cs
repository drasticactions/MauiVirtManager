// <copyright file="DomainsPage.xaml.cs" company="Drastic Actions">
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
using Microsoft.Maui.Controls.Xaml;

namespace MauiVirtManager
{
    /// <summary>
    /// Domains Page.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DomainsPage : BasePage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DomainsPage"/> class.
        /// </summary>
        /// <param name="services">see<see cref="IServiceProvider"/>.</param>
        public DomainsPage(IServiceProvider services)
             : base(services)
        {
            this.InitializeComponent();
            this.BindingContext = this.ViewModel = services.ResolveWith<DomainsViewModel>();
        }
    }
}
