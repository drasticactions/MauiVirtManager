﻿// <copyright file="MauiProgram.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using MauiVirtManager.Services;
using MauiVirtManager.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Controls.Xaml;
using Microsoft.Maui.Hosting;
using SkiaSharp.Views.Maui.Controls.Hosting;

namespace MauiVirtManager
{
    /// <summary>
    /// Main entry point.
    /// </summary>
    public static class MauiProgram
    {
        /// <summary>
        /// Creates the base MauiApp.
        /// </summary>
        /// <returns><see cref="MauiApp"/>.</returns>
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder.Services.AddSingleton<INavigationService, NavigationService>();
            builder.Services.AddSingleton<IErrorHandlerService, ErrorHandlerService>();

            // Debug, used for Mock Data.
            // TODO: Could be used for Unit Testing?
            //builder.Services.AddSingleton<IConnectionService, MockConnectionService>();
            builder.Services.AddSingleton<IConnectionService, ConnectionService>();

            builder.Services.AddTransient<DomainsViewModel>();
            builder.Services.AddTransient<DomainModalViewModel>();
            builder.Services.AddTransient<DomainModalPage>();
            builder.Services.AddTransient<DomainsPage>();
            builder
                .UseMauiApp<App>()
                .UseSkiaSharp()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("fa-solid-900.ttf", "FontAwesomeSolid");
                    fonts.AddFont("fa-regular-400.ttf", "FontAwesomeRegular");
                    fonts.AddFont("fa-brands-400", "FontAwesomeBrands");
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            return builder.Build();
        }
    }
}