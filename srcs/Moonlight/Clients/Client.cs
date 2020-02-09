using System;
using System.ComponentModel;
using Moonlight.Game.Entities;
using PropertyChanged;

namespace Moonlight.Clients
{
    [AddINotifyPropertyChangedInterface]
    public abstract class Client : IDisposable
    {
        public Character Character { get; internal set; }

        public virtual void Dispose()
        {
            // Do nothing
        }
        
        public event Func<string, bool> PacketSend;
        public event Func<string, bool> PacketReceived;

        protected bool OnPacketReceived(string packet) => PacketReceived == null || PacketReceived.Invoke(packet);
        protected bool OnPacketSend(string packet) => PacketSend == null || PacketSend.Invoke(packet);

        public abstract void SendPacket(string packet);
        public abstract void ReceivePacket(string packet);
    }
}