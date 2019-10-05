// Copyright(c) Aaron Bockover.All rights reserved.
// Licensed under the Ms-PL license. See LICENSE.txt file in the project root for full license information.

using System.Runtime.InteropServices;
using System.Threading;

using AppKit;
using Foundation;

public static class Program
{
    public static void Main(string [] args)
    {
        NSApplication.Init();
        NSApplication.SharedApplication.Delegate = new AppDelegate();
        NSApplication.Main(args);
    }

    private sealed class AppDelegate : NSApplicationDelegate
    {
        [DllImport(ObjCRuntime.Constants.libcLibrary)]
        private static extern void _exit(int exitCode);

        public override void DidFinishLaunching(NSNotification notification)
            => ThreadPool.QueueUserWorkItem(
                o => _exit(
                    Xunit.ConsoleClient.Program.Main(new[]
                    {
                        typeof(Program).Assembly.Location,
                        "-appdomains",
                        "denied"
                    })));
    }
}