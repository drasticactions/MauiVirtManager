// <copyright file="DomainsViewModel.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
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
        }

        /// <summary>
        /// Gets or sets the Domains.
        /// </summary>
        public List<Domain> Domains
        {
            get => this.domains;
            set => this.SetProperty(ref this.domains, value);
        }

        /// <inheritdoc/>
        public override async Task LoadAsync()
        {
            await base.LoadAsync();
            // TODO: Implement loading screen.
            this.IsBusy = true;
            await this.Connection.StartConnectionAsync();
            this.Domains = await this.Connection.GetDomainsAsync();
            this.IsBusy = false;
        }

        private void Connection_EventHandler(object sender, Tools.Utilities.ConnEventArgs e)
        {
            // TODO: Change UI Based on Events handled here.
            System.Diagnostics.Debug.WriteLine(e.ArgTypes);
        }
    }
}
