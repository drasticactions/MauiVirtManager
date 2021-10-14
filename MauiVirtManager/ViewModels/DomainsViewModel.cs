// <copyright file="DomainsViewModel.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IDNT.AppBasics.Virtualization.Libvirt;
using MauiVirtManager.Services;
using MauiVirtManager.Tools;
using MauiVirtManager.Tools.Utilities;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using VirtServer.Common;

namespace MauiVirtManager.ViewModels
{
    /// <summary>
    /// List of Domains (VMs) running on a given connection.
    /// </summary>
    public class DomainsViewModel : BaseViewModel
    {
        private ObservableCollection<Domain> domains = new ObservableCollection<Domain>();

        private Domain selectedDomain;

        // HACK HACK HACK
        // The CPU Virtualization value doesn't work in the Binded Library.
        // For "Hacking" reasons, we're gonna cheat and make it up.
        private Random random;

        /// <summary>
        /// Initializes a new instance of the <see cref="DomainsViewModel"/> class.
        /// </summary>
        /// <param name="services">IServiceProvider.</param>
        public DomainsViewModel(IServiceProvider services)
            : base(services)
        {
            this.random = new Random();
            this.Connection.ConnectionEventHandler += this.Connection_EventHandler;
            this.StartConnectionCommand = new AsyncCommand(
                async () => await this.StartConnectionAsync(),
                () => this.Connection.State == HubConnectionState.Disconnected,
                this.Error);
            this.RefreshDomainListCommand = new AsyncCommand(
                async () => await this.RefreshDomainListAsync(),
                () => this.Connection.State == HubConnectionState.Connected,
                this.Error);
            this.OpenDomainModalCommand = new AsyncCommand(
                async () => await this.OpenDomainModalAsync(this.SelectedDomain),
                () => { return this.SelectedDomain != null && this.Connection.State == HubConnectionState.Connected; },
                this.Error);
            this.DomainStateShutdownCommand = new AsyncCommand(
                async () => await this.UpdateDomainStateAsync(this.SelectedDomain, DomainState.Shutdown),
                () => { return this.SelectedDomain != null && (this.SelectedDomain.State != VirDomainState.VIR_DOMAIN_SHUTOFF || this.SelectedDomain.State != VirDomainState.VIR_DOMAIN_SHUTDOWN) && this.Connection.State == HubConnectionState.Connected; },
                this.Error);
            this.DomainStateSuspendCommand = new AsyncCommand(
                async () => await this.UpdateDomainStateAsync(this.SelectedDomain, DomainState.Suspend),
                () => { return this.SelectedDomain != null && this.SelectedDomain.State != VirDomainState.VIR_DOMAIN_PAUSED && this.Connection.State == HubConnectionState.Connected; },
                this.Error);
            this.DomainStateResetCommand = new AsyncCommand(
                async () => await this.UpdateDomainStateAsync(this.SelectedDomain, DomainState.Reset),
                () => { return this.SelectedDomain != null && this.Connection.State == HubConnectionState.Connected; },
                this.Error);
            this.DomainStateResumeCommand = new AsyncCommand(
                async () => await this.UpdateDomainStateAsync(this.SelectedDomain, DomainState.Resume),
                () => { return this.SelectedDomain != null && this.SelectedDomain.State != VirDomainState.VIR_DOMAIN_RUNNING && this.Connection.State == HubConnectionState.Connected; },
                this.Error);
        }

        /// <summary>
        /// Gets the RefreshDomainListCommand.
        /// </summary>
        public AsyncCommand RefreshDomainListCommand { get; private set; }

        /// <summary>
        /// Gets the DomainStateShutdownCommand.
        /// </summary>
        public AsyncCommand DomainStateShutdownCommand { get; private set; }

        /// <summary>
        /// Gets the DomainStateSuspendCommand.
        /// </summary>
        public AsyncCommand DomainStateSuspendCommand { get; private set; }

        /// <summary>
        /// Gets the DomainStateResetCommand.
        /// </summary>
        public AsyncCommand DomainStateResetCommand { get; private set; }

        /// <summary>
        /// Gets the DomainStateResumeCommand.
        /// </summary>
        public AsyncCommand DomainStateResumeCommand { get; private set; }

        /// <summary>
        /// Gets the OpenDomainModalCommand.
        /// </summary>
        public AsyncCommand OpenDomainModalCommand { get; private set; }

        /// <summary>
        /// Gets the StartConnectionCommand.
        /// </summary>
        public AsyncCommand StartConnectionCommand { get; private set; }

        /// <summary>
        /// Gets or sets the Domains.
        /// </summary>
        public ObservableCollection<Domain> Domains
        {
            get => this.domains;
            set => this.SetProperty(ref this.domains, value);
        }

        /// <summary>
        /// Gets or sets the Domains.
        /// </summary>
        public Domain SelectedDomain
        {
            get => this.selectedDomain;
            set
            {
                this.SetProperty(ref this.selectedDomain, value);
                this.RaiseCanExecute();
            }
        }

        /// <summary>
        /// Refreshes the current domains for a given connection.
        /// </summary>
        /// <returns>see<see cref="Task"/>.</returns>
        public async Task RefreshDomainListAsync()
        {
            // TODO: Cheap Hack ignoring Cancellation Token, Need to implement.
            var test = CancellationToken.None;
            var domains = await this.Connection.GetDomainsAsync();
            this.Domains.Clear();
            foreach (var domain in domains)
            {
                this.Domains.Add(domain);
            }

            Task.Run(() => Parallel.ForEachAsync<Domain>(this.Domains, CancellationToken.None, async (domain, test) => await this.UpdateDomainImageAsync(domain))).FireAndForgetSafeAsync(this.Error);
        }

        /// <summary>
        /// Updates the domain to a given state.
        /// </summary>
        /// <param name="domain"><see cref="Domain"/>.</param>
        /// <param name="domainState"><see cref="DomainState"/>.</param>
        /// <returns>see<see cref="Task"/>.</returns>
        public async Task UpdateDomainStateAsync(Domain domain, DomainState domainState)
        {
            this.IsBusy = true;
            var newDomain = await this.Connection.SetDomainStateAsync(new DomainStateUpdate() { DomainId = domain.UniqueId, State = domainState });
            domain.CopyPropertiesFrom(newDomain);
            this.RaiseCanExecute();
            this.IsBusy = false;
        }

        /// <summary>
        /// Asks for users input and starts the connection to the server.
        /// </summary>
        /// <returns>see<see cref="Task"/>.</returns>
        public async Task StartConnectionAsync()
        {
            // HACK: Adding default value so I don't have to type in my server ip all the time.
            // TODO: Store Connection addresses somewhere.
            var virtUrl = await this.Navigation.DisplayPromptAsync(Translations.Common.StartConnectionDialog, Translations.Common.StartConnectionButton, "http://192.168.1.39:5000");
            if (!string.IsNullOrEmpty(virtUrl))
            {
                await this.Connection.StartConnectionAsync(virtUrl);
                await this.RefreshDomainListAsync();
            }
        }

        /// <inheritdoc/>
        public override async Task LoadAsync()
        {
            await base.LoadAsync();
        }

        /// <inheritdoc/>
        public override void RaiseCanExecute()
        {
            this.RefreshDomainListCommand.RaiseCanExecuteChanged();
            this.DomainStateShutdownCommand.RaiseCanExecuteChanged();
            this.DomainStateSuspendCommand.RaiseCanExecuteChanged();
            this.DomainStateResetCommand.RaiseCanExecuteChanged();
            this.DomainStateResumeCommand.RaiseCanExecuteChanged();
            this.OpenDomainModalCommand.RaiseCanExecuteChanged();
        }

        private void Connection_EventHandler(object sender, Tools.Utilities.ConnEventArgs e)
        {
            // TODO: Change UI Based on Events handled here.
            switch (e.ArgTypes)
            {
                case Tools.Utilities.ConnectionEventArgTypes.Empty:
                    break;
                case Tools.Utilities.ConnectionEventArgTypes.SignalRReconnecting:
                    break;
                case Tools.Utilities.ConnectionEventArgTypes.SignalRReconnected:
                    break;
                case Tools.Utilities.ConnectionEventArgTypes.SignalRClosed:
                    break;
                case Tools.Utilities.ConnectionEventArgTypes.DomainEventRecieved:
                    DomainEventCommandProxy deProxy = (DomainEventCommandProxy)e.Data;
                    Task.Run(() => this.UpdateDomainAsync(deProxy.Domain)).FireAndForgetSafeAsync(this.Error);
                    break;
                case Tools.Utilities.ConnectionEventArgTypes.StoragePoolLifecycleEventReceived:
                    break;
                case Tools.Utilities.ConnectionEventArgTypes.StoragePoolRefreshEventReceived:
                    break;
                default:
                    break;
            }

            System.Diagnostics.Debug.WriteLine(e.ArgTypes);
        }

        private async Task UpdateDomainAsync(Domain domain, bool updateImage = false)
        {
            if (domain == null)
            {
                throw new ArgumentNullException(nameof(domain));
            }

            // If we have a new domain from LibVirt, add it to the domains list.
            // Else, update the existing one.
            var listDomain = this.Domains.FirstOrDefault(n => n.UniqueId == domain.UniqueId);
            if (listDomain == null)
            {
                listDomain = domain;
                this.Domains.Add(listDomain);
            }
            else
            {
                listDomain.CopyPropertiesFrom(domain);
            }

            // HACK HACK HACK
            // The CPU Virtualization value doesn't work in the Binded Libvirt Library.
            // For "Hacking" reasons, we're gonna cheat and make it up.
            listDomain.CpuUtilization.LastSecond = this.random.Next(40, 60);
            listDomain.CpuUtilization.PerSecondValues[listDomain.CpuUtilization.PerSecondValues.Count() - 1] = listDomain.CpuUtilization.LastSecond;

            this.RaiseCanExecute();
            if (updateImage)
            {
                await this.UpdateDomainImageAsync(listDomain);
            }

            listDomain.OnPropertyChanged(string.Empty);
        }

        private async Task UpdateDomainImageAsync(Domain domain)
        {
            if (domain.IsActive)
            {
                // HACK: If the machine is being created, we don't have an image and need to wait a bit.
                await Task.Delay(1000);
                domain.DomainImage = await this.Connection.GetDomainImageAsync(domain.UniqueId);
            }
            else
            {
                domain.DomainImage = null;
            }

            domain.OnPropertyChanged(nameof(domain.DomainImage));
        }

        private Task OpenDomainModalAsync(Domain domain)
        {
            if (domain == null)
            {
                return Task.CompletedTask;
            }

            return this.Navigation.PushModalPageInMainWindowAsync(this.Services.ResolveWith<DomainModalPage>(this.SelectedDomain));
        }
    }
}
