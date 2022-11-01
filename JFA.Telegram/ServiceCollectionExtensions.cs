using Microsoft.Extensions.DependencyInjection;

namespace JFA.Telegram;

public static class ServiceCollectionExtensions
{
    public static void AddTelegramCommands(this IServiceCollection services)
    {
        services.AddSingleton<ICommandFactory, CommandFactory>();
    }
}