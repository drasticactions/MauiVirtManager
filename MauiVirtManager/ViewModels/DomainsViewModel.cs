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
            this.Domains = await this.Connection.GetDomainsAsync();
        }
    }
}
