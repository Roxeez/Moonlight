using System;
using NtCore.API.Client;
using NtCore.API.Game.Inventory;

namespace NtCore.API.Game.Entities
{
    public interface ICharacter : IPlayer, ICommunication
    {
        int SpPoints { get; }
        int AdditionalSpPoints { get; }
        int MaximumSpPoints { get; }
        int MaximumAdditionalSpPoints { get; }
        IEquipment Equipment { get; }
        int Gold { get; }
        DateTime LastMapChange { get; }
    }
}