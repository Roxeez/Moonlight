using System.Runtime.InteropServices;

namespace Moonlight.Core.Import
{
    internal static class MoonlightInterop
    {
        public delegate bool PacketCallback(string packet);

        private const string LibraryName = "Moonlight/MoonlightInterop.dll";

        [DllImport(LibraryName, EntryPoint = "setRecvCallback", CallingConvention = CallingConvention.StdCall)]
        public static extern void SetRecvCallback(PacketCallback callback);

        [DllImport(LibraryName, EntryPoint = "setSendCallback", CallingConvention = CallingConvention.StdCall)]
        public static extern void SetSendCallback(PacketCallback callback);

        [DllImport(LibraryName, EntryPoint = "initialize", CallingConvention = CallingConvention.StdCall)]
        public static extern void Initialize();

        [DllImport(LibraryName, EntryPoint = "clean", CallingConvention = CallingConvention.StdCall)]
        public static extern void Clean();

        [DllImport(LibraryName, EntryPoint = "sendPacket", CallingConvention = CallingConvention.StdCall)]
        public static extern void SendPacket(string packet);

        [DllImport(LibraryName, EntryPoint = "recvPacket", CallingConvention = CallingConvention.StdCall)]
        public static extern void RecvPacket(string packet);

        [DllImport(LibraryName, EntryPoint = "walk", CallingConvention = CallingConvention.StdCall)]
        public static extern void Walk(short x, short y);
    }
}