using System.Reflection;

namespace JFA.Telegram;

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