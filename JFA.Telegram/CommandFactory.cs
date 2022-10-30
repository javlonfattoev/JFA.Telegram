using JFA.DependencyContainer;
using JFA.DependencyInjection;
using System.Reflection;

namespace JFA.Telegram;

public class CommandFactory
{
    private static readonly DependencyResolver Resolver = new();
    private static IReadOnlyList<Command> _commands = new List<Command>();
    private static bool _isCreated;

    private static void Create()
    {
        _commands = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(s => s.GetTypes())
            .Where(t => t.GetCustomAttribute<CommandAttribute>() != null)
            .Select(Command.Create)
            .ToList();
        
        Resolver.AddServicesFromAttribute();
        _commands.ToList().ForEach(c => Resolver.Services.AddScoped(c.Type!));
        _isCreated = true;
    }

    public static ICommand<T>? CreateCommand<T>(T context) where T : MessageContext
    {
        if (!_isCreated) Create();

        var command = _commands.FirstOrDefault(c => Filter(c, context));

        if (command?.Type == null) return null;

        return (ICommand<T>?)Resolver.GetService(command.Type);
    }

    private static bool Filter(Command command, MessageContext context)
    {
        if (context.Step is not null)
        {
            if (string.IsNullOrEmpty(context.Message))
                return command.Attribute!.Step == context.Step;

            return command.Attribute!.Command == context.Message
                   && command.Attribute!.Step == context.Step;
        }

        if (!string.IsNullOrEmpty(context.Message))
            return command.Attribute!.Command == context.Message;

        return true;
    }

    public class Command
    {
        public Type? Type { get; set; }
        public CommandAttribute? Attribute { get; set; }

        public static Command Create(Type type)
        {
            return new Command()
            {
                Type = type,
                Attribute = type.GetCustomAttribute<CommandAttribute>()
            };
        }
    }
}