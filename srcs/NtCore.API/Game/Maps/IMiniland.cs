using System.Collections.Generic;

namespace NtCore.API.Game.Maps
{
    public interface IMiniland
    {
        string Owner { get; }
        IEnumerable<IMinilandObject> MinilandObjects { get; }
    }
}