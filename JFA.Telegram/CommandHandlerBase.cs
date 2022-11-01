using System.Reflection;

namespace JFA.Telegram;

public abstract class CommandHandlerBase<T> : ICommandHandler<T> where T : MessageContextBase
{
    public async Task Excute(T context)
    {
        var method = GetType().GetMethods().FirstOrDefault(m => Filter(m, context.Message));
        if (method != null)
        {
            var task = (Task?)method.Invoke(this, new object[] { context });
            if (task is not null) await task;
        }
    }

    private static bool Filter(MemberInfo info, string? message)
    {
        if (string.IsNullOrEmpty(message))
            return true;

        var methodAttribute = info.GetCustomAttribute<MethodAttribute>();
        if (methodAttribute == null || string.IsNullOrEmpty(methodAttribute.Command))
            return true;

        return methodAttribute?.Command == message;
    }
}