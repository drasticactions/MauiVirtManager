// <copyright file="ConnectionEventArgTypes.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiVirtManager.Tools.Utilities
{
    /// <summary>
    /// Connection Event Arg Types.
    /// </summary>
    public enum ConnectionEventArgTypes
    {
        /// <summary>
        /// No Event.
        /// </summary>
        Empty,

        /// <summary>
        /// SignalR Reconnecting.
        /// </summary>
        SignalRReconnecting,

        /// <summary>
        /// SignalR Reconnected.
        /// </summary>
        SignalRReconnected,

        /// <summary>
        /// SignalR Closed.
        /// </summary>
        SignalRClosed,

        /// <summary>
        /// Domain Event Recieved.
        /// </summary>
        DomainEventRecieved,

        /// <summary>
        /// Storage Pool Lifecycle Event Recieved.
        /// </summary>
        StoragePoolLifecycleEventReceived,

        /// <summary>
        /// Storage Pool Refresh Event Recieved.
        /// </summary>
        StoragePoolRefreshEventReceived,
    }
}
