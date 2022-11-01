# JFA.Telegram
Telegram bot with commands
> Example project
- [Xarajat.Bot](https://github.com/javlonfattoev/Xarajat.Bot)
#
>Install package from [nuget.org](https://www.nuget.org/packages/JFA.Telegram)
```C#
NuGet\Install-Package JFA.Telegram -Version <VERSION>
```
#
>Create MessageContext
```C#
public class MessageContext : MessageContextBase<User>
{
    public string? Username { get; set; }
    public int MessageId { get; set; }
}
```
#
>Add lifetime attribute to service implementations, [JFA.DependencyInjection](https://github.com/javlonfattoev/JFA.DependencyInjection)
```C#
[Scoped]
public class TelegramBotService : ITelegramBotService
{...}
```
#
>Create command handler
```C#
[Command(EStep.InMainMenu)]
public class ProfileCommand : CommandHandlerBase<MessageContext>
{
    public ProfileCommand(XarajatDbContext context, TelegramBotService telegramBotService)
    {
    }

    [Method("/profile")]
    public async Task SendProfile(MessageContext context)
    {
        await TelegramBotService.SendMessage(context.User!.ChatId, context.User!.Name!);
    }

    [Method("/start")]
    public async Task SendMenu(MessageContext context)
    {
        await TelegramBotService.SendMessage(context.User!.ChatId, "Menu");
    }
}
```
