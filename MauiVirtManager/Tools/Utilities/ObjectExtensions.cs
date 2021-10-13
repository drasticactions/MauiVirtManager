// <copyright file="ObjectExtensions.cs" company="Drastic Actions">
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
    /// Object Extensions.
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// Copy properties from one object to another.
        /// </summary>
        /// <param name="self">This object.</param>
        /// <param name="target">The target object.</param>
        public static void CopyPropertiesFrom(this object self, object target)
        {
            var fromProperties = target.GetType().GetProperties();
            var toProperties = self.GetType().GetProperties();

            foreach (var fromProperty in fromProperties)
            {
                foreach (var toProperty in toProperties)
                {
                    if (fromProperty.Name == toProperty.Name && fromProperty.PropertyType == toProperty.PropertyType)
                    {
                        toProperty.SetValue(self, fromProperty.GetValue(target));
                        break;
                    }
                }
            }
        }
    }
}
