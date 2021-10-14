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
using Microcharts;
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
        private LineChart chart;
        private Random random;
        private List<ChartEntry> chartEntries;
        /// <summary>
        /// Initializes a new instance of the <see cref="DomainModalViewModel"/> class.
        /// </summary>
        /// <param name="domain"><see cref="Domain"/>.</param>
        /// <param name="chart"><see cref="LineChart"/>.</param>
        /// <param name="services">IServiceProvider.</param>
        public DomainModalViewModel(Domain domain, LineChart chart, IServiceProvider services)
            : base(services)
        {
            this.random = new Random();
            this.selectedDomain = domain;
            this.chart = chart;
            this.chart.YAxisMaxTicks = 100;
            this.chartEntries = new List<ChartEntry>();
            for (var i = 0; i < 100; i++)
            {
                this.chartEntries.Add(new ChartEntry(this.random.Next(40, 60)));
            }

            this.chart.Entries = this.chartEntries;
            this.Connection.ConnectionEventHandler += Connection_ConnectionEventHandler;
        }

        private void Connection_ConnectionEventHandler(object sender, ConnEventArgs e)
        {
            if (e.ArgTypes is ConnectionEventArgTypes.DomainEventRecieved)
            {
                DomainEventCommandProxy deProxy = (DomainEventCommandProxy)e.Data;
                if (deProxy.Domain.UniqueId == this.selectedDomain.UniqueId)
                {
                    this.chartEntries.Add(new ChartEntry(this.random.Next(40, 60)));
                }
            }
        }
    }
}
