using System.Runtime.InteropServices;

namespace Moonlight.Core.Interop
{
    internal static class Kernel32
    {
        [DllImport("kernel32")]
        public static extern bool AllocConsole();
    }
}