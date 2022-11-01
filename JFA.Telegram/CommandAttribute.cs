using JFA.DependencyInjection;

namespace JFA.Telegram;

[AttributeUsage(AttributeTargets.Class)]
public class CommandAttribute : Attribute
{
    public string Command { get; set; }
    public int Step { get; set; }
    public int Role { get; set; }
    public ELifetime Lifetime { get; set; } = ELifetime.Scoped;

    public CommandAttribute(string command) => Command = command;
    public CommandAttribute(int step, string command) : this(command) => Step = step;
    public CommandAttribute(Enum step, string command) : this(Convert.ToInt32(step), command) { }
    public CommandAttribute(Enum step) : this(Convert.ToInt32(step), string.Empty) { }
    public CommandAttribute(int step) : this(step, string.Empty) { }
    public CommandAttribute(string command, int role) : this(command) => Role = role;
    public CommandAttribute(int step, string command, int role) : this(step, command) => Role = role;
    public CommandAttribute(Enum step, string command, int role) : this(Convert.ToInt32(step), command, role) { }
    public CommandAttribute(Enum step, Enum role) : this(Convert.ToInt32(step), string.Empty, Convert.ToInt32(role)) { }
    public CommandAttribute(int step, int role) : this(step, string.Empty, role) { }
}

[AttributeUsage(AttributeTargets.Method)]
public class MethodAttribute : Attribute
{
    public string Command { get; set; }
    public int Role { get; set; }
    public MethodAttribute(string command) => Command = command;
    public MethodAttribute(string command, int role) : this(command) => Role = role;
}