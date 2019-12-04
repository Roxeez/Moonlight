using System;
using NtCore.API.Client;
using NtCore.API.Game.Entities;
using NtCore.Game.Entities;

namespace NtCore
{
    public class RemoteClient : IClient
    {
        public event Action<string> PacketSend;
        public event Action<string> PacketReceived;
        
        public ICharacter Character { get; }

        public RemoteClient()
        {
            Character = new Character();
        }
        
        public void SendPacket(string packet)
        {
            throw new System.NotImplementedException();
        }

        public void ReceivePacket(string packet)
        {
            throw new System.NotImplementedException();
        }
    }
}