using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace NtCore.Import
{
    internal static class NtNative
    {
        public delegate bool PacketCallback(string packet);

        private const string LibraryName = "NtNative.dll";

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
    }
}