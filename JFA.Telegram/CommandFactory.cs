using JFA.DependencyContainer;
using JFA.DependencyInjection;
using System.Reflection;

namespace JFA.Telegram;

public class CommandFactory : ICommandFactory
{
    private readonly DependencyResolver _resolver;
    private readonly IReadOnlyList<Command> _commands;

    public CommandFactory()
    {
        _commands = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(s => s.GetTypes())
            .Where(t => t.GetCustomAttribute<CommandAttribute>() != null)
            .Select(Command.Create)
            .ToList();

        _resolver = new DependencyResolver();
        _resolver.AddServicesFromAttribute();
        _commands.ToList().ForEach(c => _resolver.Services.AddScoped(c.Type!));
    }

    public ICommandHandler<T>? CreateCommand<T>(T context) where T : MessageContextBase
    {
        var command = _commands.FirstOrDefault(c => Filter(c, context));

        if (command?.Type == null) return null;

        return (ICommandHandler<T>?)_resolver.GetService(command.Type);
    }

    public ICommandHandler<T>? CreateCommand<T>(Type type) where T : MessageContextBase
    {
        return (ICommandHandler<T>?)_resolver.GetService(type);
    }

    private static bool Filter(Command command, MessageContextBase context)
    {
        var isStepEqual = command.Attribute!.Step == context.Step;

        if (context.Step is not null)
        {
            if (string.IsNullOrEmpty(context.Message))
                return isStepEqual;

            if (string.IsNullOrEmpty(command.Attribute!.Command))
                return isStepEqual;

            return command.Attribute!.Command == context.Message && isStepEqual;
        }

        if (!string.IsNullOrEmpty(context.Message))
            return command.Attribute!.Command == context.Message;

        return true;
    }
}