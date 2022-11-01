namespace JFA.Telegram;

public abstract class MessageContextBase
{
    public long ChatId { get; set; }
    public int? Step { get; set; }
    public string? Message { get; set; }
}

public abstract class MessageContextBase<T> : MessageContextBase where T : class
{
    public T? User { get; set; }
}