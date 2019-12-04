using System.Runtime.InteropServices;

namespace NtCore.Import
{
    public static class Kernel32
    {
        [DllImport("kernel32")]
        public static extern bool AllocConsole();
    }
}