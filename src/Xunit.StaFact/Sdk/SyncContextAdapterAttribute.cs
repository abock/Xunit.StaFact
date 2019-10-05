// Copyright (c) Aaron Bockover. All rights reserved.
// Licensed under the Ms-PL license. See LICENSE.txt file in the project root for full license information.

namespace Xunit.Sdk
{
    using System;

    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
    internal sealed class SyncContextAdapterAttribute : Attribute
    {
        public UITestCase.SyncContextType SyncContextType { get; }
        public Type SyncContextAdapterType { get; }

        public SyncContextAdapterAttribute(
            UITestCase.SyncContextType syncContextType,
            Type syncContextAdapterType)
        {
            this.SyncContextType = syncContextType;

            this.SyncContextAdapterType = syncContextAdapterType
                ?? throw new ArgumentNullException(nameof(syncContextAdapterType));

            if (syncContextAdapterType.IsAssignableFrom(typeof(SyncContextAdapter)))
                throw new ArgumentException(
                    $"must derive from {nameof(SyncContextAdapter)}",
                    nameof(syncContextAdapterType));
        }
    }
}