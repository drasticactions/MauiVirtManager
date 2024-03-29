﻿// <copyright file="IConnectionService.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MauiVirtManager.Tools.Utilities;
using Microsoft.AspNetCore.SignalR.Client;
using VirtServer.Common;

namespace MauiVirtManager.Services
{
    /// <summary>
    /// Handles the Libvirt Service Connections.
    /// </summary>
    public interface IConnectionService
    {
        /// <summary>
        /// Event Handler for connection events.
        /// </summary>
        event EventHandler<ConnEventArgs> ConnectionEventHandler;

        /// <summary>
        /// Gets the current SignalR Connection State.
        /// </summary>
        HubConnectionState State { get; }

        /// <summary>
        /// Starts the connection to VirtManager server.
        /// </summary>
        /// <param name="connectionString">Connection String.</param>
        /// <returns><see cref="Task"/>.</returns>
        Task StartConnectionAsync(string connectionString);

        /// <summary>
        /// Gets the current Libvirt Connection.
        /// </summary>
        /// <returns><see cref="Connection"/>.</returns>
        Task<Connection> GetConnectionAsync();

        /// <summary>
        /// Gets the list of domains (VMs).
        /// </summary>
        /// <returns>List of<see cref="Domain"/>.</returns>
        Task<List<Domain>> GetDomainsAsync();

        /// <summary>
        /// Gets the domain given the GUID.
        /// </summary>
        /// <param name="domainId">GUID of the domain.</param>
        /// <returns><see cref="Domain"/>.</returns>
        Task<Domain> GetDomainAsync(Guid domainId);

        /// <summary>
        /// Gets the list of Storage Pools.
        /// </summary>
        /// <returns>List of <see cref="StoragePoolElement"/>.</returns>
        Task<List<StoragePoolElement>> GetStoragePoolsAsync();

        /// <summary>
        /// Gets the list of Storage Volumes.
        /// </summary>
        /// <returns>List of <see cref="StorageVolumeStoragePool"/>.</returns>
        Task<List<StorageVolumeStoragePool>> GetStorageVolumesAsync();

        /// <summary>
        /// Start SignalR connection to Server.
        /// </summary>
        /// <returns><see cref="Task"/>.</returns>
        Task StopConnectionAsync();

        /// <summary>
        /// Set the Domain State on a given Domain.
        /// </summary>
        /// <param name="update"><see cref="DomainStateUpdate"/>.</param>
        /// <returns><see cref="Domain"/>.</returns>
        Task<Domain> SetDomainStateAsync(DomainStateUpdate update);

        /// <summary>
        /// Gets the screenshot of the given domain.
        /// </summary>
        /// <param name="domainId">GUID of the domain.</param>
        /// <returns>Stream.</returns>
        Task<System.IO.Stream> GetDomainImageAsync(Guid domainId);
    }
}
