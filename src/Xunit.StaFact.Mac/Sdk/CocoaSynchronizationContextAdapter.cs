// Copyright (c) Aaron Bockover. All rights reserved.
// Licensed under the Ms-PL license. See LICENSE.txt file in the project root for full license information.

[assembly: Xunit.Sdk.SyncContextAdapter(
    Xunit.Sdk.UITestCase.SyncContextType.Cocoa,
    typeof(Xunit.Sdk.CocoaSynchronizationContextAdapter))]

namespace Xunit.Sdk
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Foundation;

    internal class CocoaSynchronizationContextAdapter : SyncContextAdapter
    {
        internal static readonly SyncContextAdapter Default = new CocoaSynchronizationContextAdapter();

        private CocoaSynchronizationContextAdapter()
        {
        }

        internal override bool CanCompleteOperations => true;

        internal override SynchronizationContext Create() => new CocoaSynchronizationContext();

        internal override void CompleteOperations()
        {
        }

        internal override void PumpTill(Task task)
        {
        }

        internal override object Run(Func<object> work)
        {
            object result = null;
            Exception exception = null;

            NSRunLoop.Main.InvokeOnMainThread(() =>
            {
                try
                {
                    result = work();
                }
                catch (Exception ex)
                {
                    exception = ex;
                }
            });

            if (exception != null)
                throw exception;

            return result;
        }

        internal override void Run(Func<Task> work)
        {
            Exception exception = null;

            NSRunLoop.Main.InvokeOnMainThread(() =>
            {
                try
                {
                    work().GetAwaiter().GetResult();
                }
                catch (Exception ex)
                {
                    exception = ex;
                }
            });

            if (exception != null)
                throw exception;
        }
    }
}