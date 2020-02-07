using System;
using System.Runtime.InteropServices;

namespace Moonlight.Core.Import
{
    internal static class User32
    {
        [DllImport("user32.dll")]
        public static extern int PostMessage(IntPtr hWnd, int uMsg, uint wParam, uint lParam);

        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);
    }
}