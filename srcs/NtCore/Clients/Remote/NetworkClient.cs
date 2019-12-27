using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using NtCore.Cryptography;

namespace NtCore.Clients.Remote
{
    public class NetworkClient : INetworkClient
    {
        public event Action<string> PacketReceived;
        
        private readonly Socket _socket;
        private readonly ICryptography _cryptography;
        
        public NetworkClient(ICryptography cryptography)
        {
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _cryptography = cryptography;
        }

        public async Task<IEnumerable<string>> ReceivePackets()
        {
            if (!_socket.Connected)
            {
                throw new InvalidOperationException("Can't receive if socket is not connected");
            }

            var buffer = new byte[_socket.ReceiveBufferSize];
            int size = await _socket.ReceiveAsync(new ArraySegment<byte>(buffer), SocketFlags.None);

            IEnumerable<string> packets = _cryptography.Decrypt(buffer, size);
            foreach (string packet in packets)
            {
                PacketReceived?.Invoke(packet);
            }

            return packets;
        }

        public async Task Connect(IPEndPoint ip)
        {
            await _socket.ConnectAsync(ip);
        }

        public void Disconnect()
        {
            _socket.Disconnect(true);
        }

        public async Task SendPacket(string packet, bool session = false)
        {
            if (!_socket.Connected)
            {
                throw new InvalidOperationException("Can't send if socket is not connected");
            }

            byte[] encrypted = _cryptography.Encrypt(packet, session);
            await _socket.SendAsync(new ArraySegment<byte>(encrypted), SocketFlags.None);
        }
        
        public void Dispose()
        {
            Disconnect();
            
            _socket.Dispose();
        }
    }
}