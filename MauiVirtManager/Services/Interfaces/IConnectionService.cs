// <copyright file="IConnectionService.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDNT.AppBasics.Virtualization.Libvirt;

namespace MauiVirtManager.Services.Interfaces
{
    /// <summary>
    /// Handles the Libvirt Service Connections.
    /// </summary>
    public interface IConnectionService
    {
        /// <summary>
        /// Gets the current Libvirt Connection.
        /// </summary>
        /// <returns><see cref="LibvirtConnection"/>.</returns>
        Task<LibvirtConnection> GetConnectionAsync();

        /// <summary>
        /// Gets the list of domains (VMs).
        /// </summary>
        /// <returns>List of<see cref="LibvirtDomain"/>.</returns>
        Task<List<LibvirtDomain>> GetDomainsAsync();

        /// <summary>
        /// Gets the domain given the GUID.
        /// </summary>
        /// <param name="domainId">GUID of the domain.</param>
        /// <returns><see cref="LibvirtDomain"/>.</returns>
        Task<LibvirtDomain> GetDomainAsync(Guid domainId);

        /// <summary>
        /// Gets the list of Storage Pools.
        /// </summary>
        /// <returns>List of <see cref="LibvirtStoragePool"/>.</returns>
        Task<List<LibvirtStoragePool>> GetStoragePoolsAsync();

        /// <summary>
        /// Gets the list of Storage Volumes.
        /// </summary>
        /// <returns>List of <see cref="LibvirtStorageVolume"/>.</returns>
        Task<List<LibvirtStorageVolume>> GetStorageVolumesAsync();
    }
}
