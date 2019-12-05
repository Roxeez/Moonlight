using System.Collections.Generic;

namespace NtCore.API.Game.Maps
{
    public interface IMiniland
    {
        string Owner { get; }
        int Visitor { get; }
        int TotalVisitor { get; }
        string Message { get; }
        IEnumerable<IMinilandObject> MinilandObjects { get; }

        IMinilandObject GetMinilandObject(int id);
    }
}