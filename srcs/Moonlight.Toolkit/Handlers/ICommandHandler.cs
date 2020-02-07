using Moonlight.Toolkit.Commands;

namespace Moonlight.Toolkit.Handlers
{
    public interface ICommandHandler
    {
        bool Handle(ICommand command);
    }

    public abstract class CommandHandler<T> : ICommandHandler where T : ICommand
    {
        public bool Handle(ICommand command) => Handle((T)command);
        protected abstract bool Handle(T command);
    }
}