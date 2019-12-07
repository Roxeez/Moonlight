using System;
using System.Collections;
using System.Collections.Generic;
using NtCore.API.Game.Entities;

namespace NtCore.API.Game.Battle
{
    public interface ITarget
    {
        ILivingEntity Entity { get; }

        int Hp { get; }
        int Mp { get; }
        
        
    }
}