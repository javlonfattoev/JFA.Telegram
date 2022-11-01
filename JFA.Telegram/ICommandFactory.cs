namespace JFA.Telegram;

public interface ICommandFactory
{
    ICommandHandler<T>? CreateCommand<T>(T context) where T : MessageContextBase;
    ICommandHandler<T>? CreateCommand<T>(Type type) where T : MessageContextBase;
}