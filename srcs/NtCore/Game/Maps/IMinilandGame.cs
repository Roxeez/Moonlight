using System.Collections.Generic;
using NtCore.Core;

namespace NtCore.Game.Maps
{
    public interface IMinilandGame : IMinilandObject
    {
        IReadOnlyList<Range> Scores { get; }
    }
}