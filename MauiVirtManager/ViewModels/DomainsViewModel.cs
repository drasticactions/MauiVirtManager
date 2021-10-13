// <copyright file="DomainsViewModel.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IDNT.AppBasics.Virtualization.Libvirt;
using MauiVirtManager.Services;
using MauiVirtManager.Tools;
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
            this.Connection.ConnectionEventHandler += Connection_EventHandler;
            this.RefreshDomainListCommand = new AsyncCommand(
                async () => await this.RefreshDomainListAsync(),
                null,
                this.Error);
            this.DomainStateShutdownCommand = new AsyncCommand<Domain>(
                async (domain) => await this.UpdateDomainStateAsync(domain, DomainState.Shutdown),
                null,
                this.Error);
            this.DomainStateSuspendCommand = new AsyncCommand<Domain>(
                async (domain) => await this.UpdateDomainStateAsync(domain, DomainState.Suspend),
                null,
                this.Error);
            this.DomainStateResetCommand = new AsyncCommand<Domain>(
                async (domain) => await this.UpdateDomainStateAsync(domain, DomainState.Reset),
                null,
                this.Error);
            this.DomainStateResumeCommand = new AsyncCommand<Domain>(
                async (domain) => await this.UpdateDomainStateAsync(domain, DomainState.Resume),
                null,
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
            this.Domains = await this.Connection.GetDomainsAsync();
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

        /// <inheritdoc/>
        public override async Task LoadAsync()
        {
            await base.LoadAsync();
            // TODO: Implement loading screen.
            this.IsBusy = true;
            await this.Connection.StartConnectionAsync();
            await this.RefreshDomainListAsync();
            this.IsBusy = false;
        }

        private void Connection_EventHandler(object sender, Tools.Utilities.ConnEventArgs e)
        {
            // TODO: Change UI Based on Events handled here.
            System.Diagnostics.Debug.WriteLine(e.ArgTypes);
        }
    }
}
