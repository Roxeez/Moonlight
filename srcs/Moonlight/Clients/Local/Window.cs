using System;
using System.Windows.Forms;
using Moonlight.Core.Interop;

namespace Moonlight.Clients.Local
{
    public class Window
    {
        private const int WM_KEYDOWN = 0x0100;
        private const int WM_KEYUP = 0x0101;
        private const int WM_CHAR = 0x0102;
        
        public Window(IntPtr handle) => Handle = handle;
        public IntPtr Handle { get; }

        public void Rename(string name)
        {
            User32.SetWindowText(Handle, name);
        }

        public void BringToFront()
        {
            User32.SetForegroundWindow(Handle);
        }

        public void SendKey(Keys key)
        {
            User32.PostMessage(Handle, WM_KEYDOWN, Convert.ToUInt32(key), 0);
            User32.PostMessage(Handle, WM_CHAR, Convert.ToUInt32(key), 0);
            User32.PostMessage(Handle, WM_KEYUP, Convert.ToUInt16(key), 0);
        }
    }
}