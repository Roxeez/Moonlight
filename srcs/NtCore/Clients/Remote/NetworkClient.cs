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

        private readonly IPEndPoint _ip;
        private readonly Socket _socket;
        private readonly ICryptography _cryptography;
        
        public NetworkClient(IPEndPoint ip, ICryptography cryptography)
        {
            _ip = ip;
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

            return _cryptography.Decrypt(buffer, size);
        }

        public async Task SendPacket(string packet, bool session = false)
        {
            if (!_socket.Connected)
            {
                await _socket.ConnectAsync(_ip);
                if (!_socket.Connected)
                {
                    throw new InvalidOperationException("Can't send if socket is not connected");
                }
            }

            byte[] encrypted = _cryptography.Encrypt(packet, session);
            await _socket.SendAsync(new ArraySegment<byte>(encrypted), SocketFlags.None);
        }
        
        public void Dispose()
        {
            _socket.Dispose();
        }
    }
}