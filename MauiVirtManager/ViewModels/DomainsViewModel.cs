// <copyright file="DomainsViewModel.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IDNT.AppBasics.Virtualization.Libvirt;
using MauiVirtManager.Services;
using MauiVirtManager.Tools;
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
        private List<Domain> domains;

        /// <summary>
        /// Initializes a new instance of the <see cref="DomainsViewModel"/> class.
        /// </summary>
        /// <param name="services">IServiceProvider.</param>
        public DomainsViewModel(IServiceProvider services)
            : base(services)
        {
            this.Connection.ConnectionEventHandler += this.Connection_EventHandler;
            this.StartConnectionCommand = new AsyncCommand(
                async () => await this.StartConnectionAsync(),
                () => this.Connection.State == HubConnectionState.Disconnected,
                this.Error);
            this.RefreshDomainListCommand = new AsyncCommand(
                async () => await this.RefreshDomainListAsync(),
                () => this.Connection.State == HubConnectionState.Connected,
                this.Error);
            this.DomainStateShutdownCommand = new AsyncCommand<Domain>(
                async (domain) => await this.UpdateDomainStateAsync(domain, DomainState.Shutdown),
                (domain) => { return this.Connection.State == HubConnectionState.Connected; },
                this.Error);
            this.DomainStateSuspendCommand = new AsyncCommand<Domain>(
                async (domain) => await this.UpdateDomainStateAsync(domain, DomainState.Suspend),
                (domain) => { return this.Connection.State == HubConnectionState.Connected; },
                this.Error);
            this.DomainStateResetCommand = new AsyncCommand<Domain>(
                async (domain) => await this.UpdateDomainStateAsync(domain, DomainState.Reset),
                (domain) => { return this.Connection.State == HubConnectionState.Connected; },
                this.Error);
            this.DomainStateResumeCommand = new AsyncCommand<Domain>(
                async (domain) => await this.UpdateDomainStateAsync(domain, DomainState.Resume),
                (domain) => { return this.Connection.State == HubConnectionState.Connected; },
                this.Error);
        }

        /// <summary>
        /// Gets the RefreshDomainListCommand.
        /// </summary>
        public AsyncCommand RefreshDomainListCommand { get; private set; }

        /// <summary>
        /// Gets the DomainStateShutdownCommand.
        /// </summary>
        public AsyncCommand<Domain> DomainStateShutdownCommand { get; private set; }

        /// <summary>
        /// Gets the DomainStateSuspendCommand.
        /// </summary>
        public AsyncCommand<Domain> DomainStateSuspendCommand { get; private set; }

        /// <summary>
        /// Gets the DomainStateResetCommand.
        /// </summary>
        public AsyncCommand<Domain> DomainStateResetCommand { get; private set; }

        /// <summary>
        /// Gets the DomainStateResumeCommand.
        /// </summary>
        public AsyncCommand<Domain> DomainStateResumeCommand { get; private set; }

        /// <summary>
        /// Gets the StartConnectionCommand.
        /// </summary>
        public AsyncCommand StartConnectionCommand { get; private set; }

        /// <summary>
        /// Gets or sets the Domains.
        /// </summary>
        public List<Domain> Domains
        {
            get => this.domains;
            set => this.SetProperty(ref this.domains, value);
        }

        /// <summary>
        /// Refreshes the current domains for a given connection.
        /// </summary>
        /// <returns>see<see cref="Task"/>.</returns>
        public async Task RefreshDomainListAsync()
        {
            // TODO: Cheap Hack ignoring Cancellation Token, Need to implement.
            var test = CancellationToken.None;
            this.Domains = await this.Connection.GetDomainsAsync();
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
            domain = await this.Connection.SetDomainStateAsync(new DomainStateUpdate() { DomainId = domain.UniqueId, State = DomainState.Shutdown });
            this.OnPropertyChanged(nameof(this.Domains));

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

        private void Connection_EventHandler(object sender, Tools.Utilities.ConnEventArgs e)
        {
            // TODO: Change UI Based on Events handled here.
            System.Diagnostics.Debug.WriteLine(e.ArgTypes);
        }

        private async Task UpdateDomainImageAsync(Domain domain)
        {
            if (domain.IsActive)
            {
                domain.DomainImage = await this.Connection.GetDomainImageAsync(domain.UniqueId);
                domain.OnPropertyChanged(nameof(domain.DomainImage));
            }
        }
    }
}
