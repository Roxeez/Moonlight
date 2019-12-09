namespace NtCore.Game.Maps
{
    /// <summary>
    ///     Class used for managing all running map (mostly useful for multi client plugins)
    /// </summary>
    public interface IMapManager
    {
        IMap GetMapById(int id);
    }
}