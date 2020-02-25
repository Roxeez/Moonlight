using System;
using System.Diagnostics;
using Moonlight.Core.Interop;

namespace Moonlight.Clients.Local
{
    public class Window
    {
        public IntPtr Handle { get; }

        public Window(IntPtr handle) => Handle = handle;

        public void Rename(string name)
        {
            User32.SetWindowText(Handle, name);
        }

        public void BringToFront()
        {
            User32.SetForegroundWindow(Handle);
        }
    }
}