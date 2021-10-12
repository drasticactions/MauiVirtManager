// <copyright file="ConnectionService.cs" company="Drastic Actions">
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

namespace MauiVirtManager.Services.Implementations
{
    /// <summary>
    /// Libvirt Connection Service.
    /// </summary>
    public class ConnectionService : IConnectionService
    {
        private static string baseEndpoint;
        private HttpClient client;

        private string connectionEndpoint = $"{baseEndpoint}/connection";
        private string domainsEndpoint = $"{baseEndpoint}/domains";
        private string domainEndpoint = $"{baseEndpoint}/domain";
        private string storagePoolsEndpoint = $"{baseEndpoint}/storagepools";
        private string storagePoolEndpoint = $"{baseEndpoint}/storagepool";
        private string storageVolumesEndpoint = $"{baseEndpoint}/storagevolumes";
        private string libvirtEndpoint = $"{baseEndpoint}/libvirt";
        private HubConnection connection;


        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionService"/> class.
        /// </summary>
        /// <param name="baseUri">Host URI.</param>
        public ConnectionService(string baseUri = "http://drastic-nuc.local:5000")
        {
            // HACK: Hardcoded URI for computer. Needs to be taken from the user!
            baseEndpoint = baseUri;
            this.client = new HttpClient();
            this.connection = new HubConnectionBuilder()
                .WithUrl(this.libvirtEndpoint)
                .Build();

            this.connection.On<StoragePoolRefreshEventCommand>("StoragePoolRefreshEventReceived", this.StoragePoolRefreshEventReceived);
            this.connection.On<StoragePoolLifecycleEventCommand>("StoragePoolLifecycleEventReceived", this.StoragePoolLifecycleEventReceived);
            this.connection.On<DomainEventCommand>("DomainEventReceived", this.DomainEventReceived);

            this.connection.Closed += this.Connection_Closed;
            this.connection.Reconnected += this.Connection_Reconnected;
            this.connection.Reconnecting += this.Connection_Reconnecting;
        }

        /// <inheritdoc/>
        public HubConnectionState State => this.connection?.State ?? HubConnectionState.Disconnected;

        /// <inheritdoc/>
        public Task<Connection> GetConnectionAsync()
        {
            return this.client.GetFromJsonAsync<Connection>(this.connectionEndpoint);
        }

        /// <inheritdoc/>
        public Task<Domain> GetDomainAsync(Guid domainId)
        {
            return this.client.GetFromJsonAsync<Domain>($"{this.domainEndpoint}?id={domainId}");
        }

        /// <inheritdoc/>
        public Task<List<Domain>> GetDomainsAsync()
        {
            return this.client.GetFromJsonAsync<List<Domain>>(this.domainsEndpoint);
        }

        /// <inheritdoc/>
        public Task<List<StoragePoolElement>> GetStoragePoolsAsync()
        {
            return this.client.GetFromJsonAsync<List<StoragePoolElement>>(this.storagePoolsEndpoint);
        }

        /// <inheritdoc/>
        public Task<List<StorageVolumeStoragePool>> GetStorageVolumesAsync()
        {
            return this.client.GetFromJsonAsync<List<StorageVolumeStoragePool>>(this.storageVolumesEndpoint);
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

        private void DomainEventReceived(DomainEventCommand arg)
        {
            System.Diagnostics.Debug.WriteLine(arg);
        }

        private void StoragePoolLifecycleEventReceived(StoragePoolLifecycleEventCommand arg)
        {
            System.Diagnostics.Debug.WriteLine(arg);
        }

        private void StoragePoolRefreshEventReceived(StoragePoolRefreshEventCommand arg)
        {
            System.Diagnostics.Debug.WriteLine(arg);
        }
    }
}
