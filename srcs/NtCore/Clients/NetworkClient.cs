using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using NtCore.Clients.Cryptography;

namespace NtCore.Clients
{
    public sealed class NetworkClient : IDisposable
    {
        private readonly IPEndPoint _ip;
        private readonly ICryptography _cryptography;
        private readonly Socket _socket;
        private Task _loop;

        private bool _dispose;

        public event Action<string> PacketReceived;

        public NetworkClient(IPEndPoint ip, ICryptography cryptography)
        {
            _ip = ip;
            _cryptography = cryptography;
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        private void Run()
        {
            _loop = Task.Run(Loop);
        }

        private async void Loop()
        {
            while (!_dispose)
            {
                if (!_socket.Connected)
                {
                    Thread.Sleep(100);
                    continue;
                }

                IEnumerable<string> packets = await ReceivePackets();
                foreach (string packet in packets)
                {
                    PacketReceived?.Invoke(packet);
                }
            }
        }
        
        public async Task SendPacket(string packet)
        {
            if (!_socket.Connected)
            {
                await _socket.ConnectAsync(_ip);
            }

            if (!_socket.Connected)
            {
                Dispose();
                return;
            }

            byte[] encrypted = _cryptography.Encrypt(packet);
            await _socket.SendAsync(new ArraySegment<byte>(encrypted), SocketFlags.None);
        }

        public async Task<IEnumerable<string>> ReceivePackets()
        {
            if (!_socket.Connected)
            {
                await _socket.ConnectAsync(_ip);
            }

            if (!_socket.Connected)
            {
                Dispose();
                return new [] { string.Empty };
            }

            var buffer = new byte[_socket.ReceiveBufferSize];
            int size = await _socket.ReceiveAsync(new ArraySegment<byte>(buffer), SocketFlags.None);

            return _cryptography.Decrypt(buffer, size);
        }

        public async void Dispose()
        {
            _dispose = true;
            if (_loop != null && _loop.IsCompleted)
            {
                await _loop;
            }
            _socket.Disconnect(false);
            _socket.Dispose();
        }
    }
}