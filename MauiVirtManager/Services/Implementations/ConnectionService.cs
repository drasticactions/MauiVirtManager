﻿// <copyright file="ConnectionService.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using VirtServer.Common;

namespace MauiVirtManager.Services
{
    /// <summary>
    /// Libvirt Connection Service.
    /// </summary>
    public class ConnectionService : IConnectionService
    {
        private string baseEndpoint;
        private HttpClient client;

        private string connectionEndpoint = "{0}/connection";
        private string domainsEndpoint = "{0}/domains";
        private string domainEndpoint = "{0}/domain";
        private string storagePoolsEndpoint = "{0}/storagepools";
        private string storagePoolEndpoint = "{0}/storagepool";
        private string storageVolumesEndpoint = "{0}/storagevolumes";
        private string libvirtEndpoint = "{0}/libvirt";
        private HubConnection connection;


        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionService"/> class.
        /// </summary>
        /// <param name="baseUri">Host URI.</param>
        public ConnectionService(string baseUri = "http://drastic-nuc.local:5000")
        {
            // HACK: Hardcoded URI for computer. Needs to be taken from the user!
            this.baseEndpoint = baseUri;
            this.client = new HttpClient();
            this.connection = new HubConnectionBuilder()
                .WithUrl(string.Format(this.libvirtEndpoint, this.baseEndpoint))
                .Build();

            this.connection.On<string>("StoragePoolRefreshEventReceived", this.StoragePoolRefreshEventReceived);
            this.connection.On<string>("StoragePoolLifecycleEventReceived", this.StoragePoolLifecycleEventReceived);
            this.connection.On<string>("DomainEventReceived", this.DomainEventReceived);

            this.connection.Closed += this.Connection_Closed;
            this.connection.Reconnected += this.Connection_Reconnected;
            this.connection.Reconnecting += this.Connection_Reconnecting;
        }

        /// <inheritdoc/>
        public HubConnectionState State => this.connection?.State ?? HubConnectionState.Disconnected;

        /// <inheritdoc/>
        public Task<Connection> GetConnectionAsync()
        {
            return this.client.GetFromJsonAsync<Connection>(string.Format(this.connectionEndpoint, this.baseEndpoint));
        }

        /// <inheritdoc/>
        public Task<Domain> GetDomainAsync(Guid domainId)
        {
            return this.client.GetFromJsonAsync<Domain>($"{string.Format(this.domainEndpoint, this.baseEndpoint)}?id={domainId}");
        }

        /// <inheritdoc/>
        public Task<List<Domain>> GetDomainsAsync()
        {
            return this.client.GetFromJsonAsync<List<Domain>>(string.Format(this.domainsEndpoint, this.baseEndpoint));
        }

        /// <inheritdoc/>
        public Task<List<StoragePoolElement>> GetStoragePoolsAsync()
        {
            return this.client.GetFromJsonAsync<List<StoragePoolElement>>(string.Format(this.storagePoolsEndpoint, this.baseEndpoint));
        }

        /// <inheritdoc/>
        public Task<List<StorageVolumeStoragePool>> GetStorageVolumesAsync()
        {
            return this.client.GetFromJsonAsync<List<StorageVolumeStoragePool>>(string.Format(this.storageVolumesEndpoint, this.baseEndpoint));
        }

        /// <inheritdoc/>
        public Task StartConnectionAsync()
        {
            return this.connection.StartAsync();
        }

        /// <inheritdoc/>
        public Task StopConnectionAsync()
        {
            return this.connection.StopAsync();
        }

        private async Task Connection_Reconnecting(Exception arg)
        {
            System.Diagnostics.Debug.WriteLine(arg);
        }

        private async Task Connection_Reconnected(string arg)
        {
            System.Diagnostics.Debug.WriteLine(arg);
        }

        private async Task Connection_Closed(Exception arg)
        {
            System.Diagnostics.Debug.WriteLine(arg);
        }

        private void DomainEventReceived(string arg)
        {
            System.Diagnostics.Debug.WriteLine(arg);
        }

        private void StoragePoolLifecycleEventReceived(string arg)
        {
            System.Diagnostics.Debug.WriteLine(arg);
        }

        private void StoragePoolRefreshEventReceived(string arg)
        {
            System.Diagnostics.Debug.WriteLine(arg);
        }
    }
}
