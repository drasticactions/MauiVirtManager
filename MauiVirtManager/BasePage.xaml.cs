// <copyright file="BasePage.xaml.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiVirtManager.ViewModels;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;

namespace MauiVirtManager
{
    /// <summary>
    /// Base Page, used as the base class for all content pages.
    /// Calls "LoadAsync" for the <see cref="BaseViewModel"/> OnAppearing.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BasePage : ContentPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BasePage"/> class.
        /// </summary>
        /// <param name="services"><see cref="IServiceProvider"/>.</param>
        public BasePage(IServiceProvider services)
        {
            this.Services = services;
        }

        /// <summary>
        /// Gets or sets the View Model.
        /// </summary>
        internal BaseViewModel ViewModel { get; set; }

        /// <summary>
        /// Gets or sets the IServiceProvider.
        /// </summary>
        internal IServiceProvider Services { get; set; }

        /// <inheritdoc/>
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await this.ViewModel?.LoadAsync();
        }
    }
}
