﻿using System;
using System.Runtime.InteropServices;

namespace NtCore.Import
{
    internal static class User32
    {
        [DllImport("user32.dll", EntryPoint = "FindWindowEx")]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow = null);
        
        [DllImport("user32.dll")]
        public static extern int PostMessage(IntPtr hWnd, uint msg, int wParam, int lParam);
        
        [DllImport("user32.dll", EntryPoint = "mouse_event")]
        public static extern void MouseEvent(uint dwFlags, int dx, int dy, uint dwData, int dwExtraInfo);
    }
}