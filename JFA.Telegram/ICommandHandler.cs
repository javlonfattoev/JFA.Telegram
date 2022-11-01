namespace JFA.Telegram;

public interface ICommandHandler
{
    Task Excute(MessageContextBase context);
}

public interface ICommandHandler<in T> where T : MessageContextBase
{
    Task Excute(T context);
}