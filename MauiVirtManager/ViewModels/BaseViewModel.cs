// <copyright file="BaseViewModel.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using System.Threading.Tasks;
using MauiVirtManager.Services;
using MauiVirtManager.Tools;
using Microsoft.Extensions.DependencyInjection;

namespace MauiVirtManager.ViewModels
{
    /// <summary>
    /// Base View Model.
    /// </summary>
    public class BaseViewModel : ExtendedBindableObject
    {
        private bool isBusy;
        private bool isRefreshing;
        private string title;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseViewModel"/> class.
        /// </summary>
        /// <param name="services">IServiceProvider.</param>
        public BaseViewModel(IServiceProvider services)
        {
            this.Services = services;
            this.Navigation = services.GetService<INavigationService>();
            this.Error = services.GetService<IErrorHandlerService>();
            this.Connection = services.GetService<IConnectionService>();
            this.CloseDialogCommand = new AsyncCommand(
               async () => await this.ExecuteCloseDialogCommand(),
               null,
               this.Error);
        }

        /// <summary>
        /// Gets or Sets a value indicating whether the view is busy.
        /// </summary>
        public bool IsBusy
        {
            get
            {
                return this.isBusy;
            }

            set
            {
                this.isBusy = value;
                this.OnPropertyChanged("IsBusy");
            }
        }

        /// <summary>
        /// Gets or Sets a value indicating whether the view is refreshing.
        /// </summary>
        public bool IsRefreshing
        {
            get
            {
                return this.isRefreshing;
            }

            set
            {
                this.isRefreshing = value;
                this.OnPropertyChanged("IsRefreshing");
            }
        }

        /// <summary>
        /// Gets or sets the page title.
        /// </summary>
        public string Title
        {
            get
            {
                return this.title;
            }

            set
            {
                this.title = value;
                this.OnPropertyChanged("Title");
            }
        }

        /// <summary>
        /// Gets the Close Dialog Command.
        /// </summary>
        public AsyncCommand CloseDialogCommand { get; private set; }

        /// <summary>
        /// Gets the service provider collection.
        /// </summary>
        protected IServiceProvider Services { get; private set; }

        /// <summary>
        /// Gets the navigation service.
        /// </summary>
        protected INavigationService Navigation { get; private set; }

        /// <summary>
        /// Gets the connection service.
        /// </summary>
        protected IConnectionService Connection { get; private set; }

        /// <summary>
        /// Gets the error handler service.
        /// </summary>
        protected IErrorHandlerService Error { get; private set; }

        /// <summary>
        /// Load VM Async.
        /// </summary>
        /// <returns><see cref="Task"/>.</returns>
        public virtual Task LoadAsync()
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// Raise Can Execute Commands.
        /// </summary>
        public virtual void RaiseCanExecute()
        {
        }

        /// <summary>
        /// Sets title for page.
        /// </summary>
        /// <param name="title">The Title.</param>
        public virtual void SetTitle(string title = "")
        {
            this.Title = title;
        }

        /// <summary>
        /// Unload VM Async.
        /// </summary>
        /// <returns><see cref="Task"/>.</returns>
        public virtual Task UnloadAsync()
        {
            return Task.CompletedTask;
        }

        private async Task ExecuteCloseDialogCommand()
        {
            await this.Navigation.PopModalPageInMainWindowAsync();
        }
    }
}
