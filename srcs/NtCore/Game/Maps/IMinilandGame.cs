using System.Collections.Generic;

namespace NtCore.Game.Maps
{
    public interface IMinilandGame : IMinilandObject
    {
        IReadOnlyList<Range> Scores { get; }
    }
}