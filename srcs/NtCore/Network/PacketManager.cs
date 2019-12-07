using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using NtCore.API.Clients;
using NtCore.API.Extensions;
using NtCore.API.Logger;

namespace NtCore.Network
{
    public interface IPacketManager
    {
        void Handle(IClient client, string packet, PacketType packetType);

        void Register(IPacketHandler packetHandler);
    }

    public sealed class PacketManager : IPacketManager
    {
        private readonly ILogger _logger;

        private readonly Dictionary<(string, PacketType), PacketCreator> _packetCreators =
            new Dictionary<(string, PacketType), PacketCreator>();

        private readonly Dictionary<(string, PacketType), IPacketHandler> _packetHandlers =
            new Dictionary<(string, PacketType), IPacketHandler>();

        public PacketManager(ILogger logger)
        {
            _logger = logger;
        }

        public void Handle(IClient client, string packet, PacketType packetType)
        {
            var arguments = packet.Trim().Split(' ');
            var header = arguments.Length > 0 ? arguments[0] : packet;

            var packetHandler = _packetHandlers.GetValueOrDefault((header, packetType));
            if (packetHandler == null) return;

            var packetCreator = _packetCreators.GetValueOrDefault((header, packetType));
            if (packetCreator == null)
            {
                _logger.Debug($"No packet creator found for packet {header}");
                return;
            }

            var p = packetCreator();
            var deserialized = p.Deserialize(arguments);

            if (!deserialized)
            {
                _logger.Debug($"Failed to deserialize packet {header}");
                return;
            }

            packetHandler.Handle(client, p);
        }

        public void Register(IPacketHandler packetHandler)
        {
            var type = packetHandler.GetType();
            if (type.BaseType == null) return;

            var packetType =
                type.BaseType.GenericTypeArguments.FirstOrDefault(x => typeof(IPacket).IsAssignableFrom(x));
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

            var packetConstructor = packetType.GetConstructor(Type.EmptyTypes);
            if (packetConstructor == null)
            {
                _logger.Debug($"No constructor found for {packetType.Name}");
                return;
            }

            _logger.Information($"Registered packet handler {type.Name} for packet {packetType.Name}");

            _packetHandlers[(info.Header, info.Type)] = packetHandler;
            _packetCreators[(info.Header, info.Type)] =
                Expression.Lambda<PacketCreator>(Expression.New(packetConstructor)).Compile();
        }

        private delegate IPacket PacketCreator();
    }
}