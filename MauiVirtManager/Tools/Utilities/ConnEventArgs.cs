// <copyright file="ConnectionEventArgs.cs" company="Drastic Actions">
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
    /// Connection Event Arguments.
    /// </summary>
    public class ConnEventArgs : EventArgs
    {
        /// <summary>
        /// Gets or sets the <see cref="ConnectionEventArgTypes"/>.
        /// </summary>
        public ConnectionEventArgTypes ArgTypes { get; set; }

        /// <summary>
        /// Gets or sets the generic object on the events.
        /// This could be cast based on <see cref="ArgTypes"/>. 
        /// </summary>
        public object Data { get; set; }
    }
}
