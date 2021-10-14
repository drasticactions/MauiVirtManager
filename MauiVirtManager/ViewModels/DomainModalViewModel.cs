// <copyright file="DomainModalViewModel.cs" company="Drastic Actions">
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
    /// Domain Modal View Model.
    /// </summary>
    public class DomainModalViewModel : BaseViewModel
    {
        private Domain selectedDomain;

        /// <summary>
        /// Initializes a new instance of the <see cref="DomainModalViewModel"/> class.
        /// </summary>
        /// <param name="domain"><see cref="Domain"/>.</param>
        /// <param name="services">IServiceProvider.</param>
        public DomainModalViewModel(Domain domain, IServiceProvider services)
            : base(services)
        {
            this.selectedDomain = domain;
        }
    }
}
