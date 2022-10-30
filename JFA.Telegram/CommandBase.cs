namespace JFA.Telegram;

public abstract class CommandBase<T> : ICommand<T> where T : MessageContext
{
    public abstract Task Excute(T context);
}

public abstract class CommandBase : ICommand
{
    public abstract Task Excute(MessageContext context);
}

public interface ICommand
{
    Task Excute(MessageContext context);
}

public interface ICommand<in T> where T : MessageContext
{
    Task Excute(T context);
}