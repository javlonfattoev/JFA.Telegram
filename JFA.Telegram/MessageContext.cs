namespace JFA.Telegram;

public abstract class MessageContext<T> : MessageContext where T : class
{
    public T? User { get; set; }
}

public abstract class MessageContext
{
    public long ChatId { get; set; }
    public int? Step { get; set; }
    public string? Message { get; set; }
}