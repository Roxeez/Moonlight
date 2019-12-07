namespace NtCore.API.Commands
{
    public interface ICommandManager
    {
        void Register(ICommandHandler handler);
    }
}