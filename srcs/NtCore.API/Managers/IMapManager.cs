using NtCore.API.Game.Maps;

namespace NtCore.API.Managers
{
    public interface IMapManager
    {
        IMap GetMapById(int id);
    }
}