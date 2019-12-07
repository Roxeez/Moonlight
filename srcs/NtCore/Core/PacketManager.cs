using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using NtCore.API.Client;
using NtCore.API.Extensions;
using NtCore.API.Logger;
using NtCore.Network;

namespace NtCore.Core
{
    public interface IPacketManager
    {
        void Handle(IClient client, string packet, PacketType packetType);

        void Register(IPacketHandler packetHandler);
    }
    
    public sealed class PacketManager : IPacketManager
    {
        private delegate IPacket PacketCreator();
        
        private readonly Dictionary<(string, PacketType), IPacketHandler> _packetHandlers = new Dictionary<(string, PacketType), IPacketHandler>();
        private readonly Dictionary<(string, PacketType), PacketCreator> _packetCreators = new Dictionary<(string, PacketType), PacketCreator>();
        
        private readonly ILogger _logger;
        
        public PacketManager(ILogger logger)
        {
            _logger = logger;
        }

        public void Handle(IClient client, string packet, PacketType packetType)
        {
            string[] arguments = packet.Trim().Split(' ');
            string header = arguments.Length > 0 ? arguments[0] : packet;

            IPacketHandler packetHandler = _packetHandlers.GetValueOrDefault((header, packetType));
            if (packetHandler == null)
            {
                return;
            }
            
            PacketCreator packetCreator = _packetCreators.GetValueOrDefault((header, packetType));
            if (packetCreator == null)
            {
                _logger.Debug($"No packet creator found for packet {header}");
                return;
            }
            
            IPacket p = packetCreator();
            bool deserialized = p.Deserialize(arguments);
            
            if (!deserialized)
            {
                _logger.Debug($"Failed to deserialize packet {header}");
                return;
            }
            
            packetHandler.Handle(client, p);
        }

        public void Register(IPacketHandler packetHandler)
        {
            Type type = packetHandler.GetType();
            if (type.BaseType == null)
            {
                return;
            }

            Type packetType = type.BaseType.GenericTypeArguments.FirstOrDefault(x => typeof(IPacket).IsAssignableFrom(x));
            if (packetType == null)
            {
                _logger.Debug($"{type.Name} don't have any GenericTypeArguments of type IPacket");
                return;
            }
                
            var info = packetType.GetCustomAttribute<PacketInfo>();
            if (info == null)
            {
                _logger.Debug($"Missing header annotation on packet {packetType.Name}");
                return;
            }

            ConstructorInfo packetConstructor = packetType.GetConstructor(Type.EmptyTypes);
            if (packetConstructor == null)
            {
                _logger.Debug($"No constructor found for {packetType.Name}");
                return;
            }

            _logger.Debug($"Registering handler {type.Name} with packet {packetType.Name}");

            _packetHandlers[(info.Header, info.Type)] = packetHandler;
            _packetCreators[(info.Header, info.Type)] = Expression.Lambda<PacketCreator>(Expression.New(packetConstructor)).Compile();
        }
    }
}